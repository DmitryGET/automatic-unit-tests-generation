from smolagents import CodeAgent, tool
from test_run_handler import run_function_tests
from output_analyzer_handler import parse_test_results
from typing import Dict, Any
from openai import OpenAI
import yaml
import re
import logging

# Отключаем подробные логи smolagents
logging.getLogger("smolagents").setLevel(logging.ERROR)

# Инструмент для генерации юнит-тестов
@tool
def generate_unit_tests_from_code(code: str, errors: str = "") -> str:
    """Генерирует юнит-тесты для кода на Python.

        Args:
            code: Исходный код функции.
            errors: Ошибки предыдущих тестов (если есть).

        Returns:
            Строка с кодом тестов в формате `Code: ```py ... <end_code>`.
    """
    if errors:
        return f"""\
Исходный код функции:
```python
{code}
```
Ошибки предыдущих тестов:
{errors}

Сгенерируйте исправленные юнит-тесты, используя фреймворк unittest. Верните только код тестов в формате:
Code:
```py
import unittest
...
```
<end_code>
"""
    return f"""\
Исходный код функции:
```python
{code}
```
Сгенерируйте полные, чистые и читаемые юнит-тесты, используя фреймворк unittest.
Включите тесты для типичных сценариев и граничных случаев. Верните только код тестов в формате:
Code:
```py
import unittest
...
```
<end_code>
"""

# Инструмент для финального ответа
@tool
def final_answer(short_outcome: str, detailed_outcome: str, context: str = "") -> str:
    """Форматирует финальный ответ для задачи.

        Args:
            short_outcome: Краткое описание результата.
            detailed_outcome: Подробное описание результата.
            context: Дополнительный контекст (если есть).

        Returns:
            Строка с отформатированным финальным ответом.
    """
    return f"""\
### 1. Task outcome (short version):
{short_outcome}
### 2. Task outcome (extremely detailed version):
{detailed_outcome}
### 3. Additional context (if relevant):
{context}
"""

class ChatMessage:
    def __init__(self, content: str):
        self.content = content

class CustomOllamaModel:
    def __init__(self, base_url: str = "http://localhost:11434/v1", model: str = "llama3.1"):
        self.client = OpenAI(base_url=base_url, api_key="ollama")
        self.model = model

    def __call__(self, messages, **kwargs):
        supported_kwargs = {k: v for k, v in kwargs.items() if k in ["temperature", "max_tokens", "top_p"]}
        supported_kwargs["max_tokens"] = 2048  # Увеличиваем для длинных ответов
        response = self.client.chat.completions.create(
            model=self.model,
            messages=messages,
            **supported_kwargs
        )
        return ChatMessage(content=response.choices[0].message.content)

def extract_code_from_response(response: str) -> str:
    match = re.search(r'Code:\s*```py\n(.*?)\n```<end_code>', response, re.DOTALL)
    if match:
        return match.group(1).strip()
    # Если формат неверный, возвращаем пустую строку, чтобы не накапливать мусор
    return ""

def print_test_results(parsed_result: Dict[str, Any]):
    print(f"\nРезультаты тестов:")
    print(f"Всего тестов: {parsed_result['total_tests']}")
    print(f"Пройдено: {len(parsed_result['passed_tests'])}")
    print(f"Провалено: {len(parsed_result['failed_tests'])}")
    if parsed_result['failed_tests']:
        print("\nПроваленные тесты:")
        for test in parsed_result['failed_tests']:
            print(f"- {test['test_name']}:\n{test['traceback']}\n")
    if parsed_result['success']:
        print("✅ Все тесты пройдены!")
    else:
        print("❌ Некоторые тесты провалены.")

def fix_function_with_ai(parsed_results: Dict[str, Any], original_code: str, test_code: str = None, max_attempts: int = 3) -> Dict[str, Any]:
    current_code = original_code.strip()
    generated_test_code = test_code

    # Загрузка шаблонов промптов
    with open("prompts.yaml", 'r') as stream:
        prompt_templates = yaml.safe_load(stream)

    # Инициализация модели
    model = CustomOllamaModel(base_url="http://localhost:11434/v1", model="llama3.1")

    # Инициализация агента
    agent = CodeAgent(
        tools=[generate_unit_tests_from_code, final_answer],
        model=model,
        max_steps=6,
        prompt_templates=prompt_templates,
        additional_authorized_imports=['unittest', 'sys']
    )

    # Генерация тестов, если не предоставлены
    if not generated_test_code:
        print("🤖 Генерация юнит-тестов...")
        test_prompt = generate_unit_tests_from_code(current_code)
        generated_test_code = extract_code_from_response(agent(test_prompt))
        if not generated_test_code:
            print("⚠️ Не удалось сгенерировать тесты: модель вернула неверный формат.")
            return {
                "fixed_code": current_code,
                "generated_tests": "",
                "iterations": 0,
                "final_result": {"success": False, "error": "Invalid test code format"},
                "agent_response": "Failed to generate tests"
            }
        print(f"Сгенерированные тесты:\n{generated_test_code}")

    for attempt in range(max_attempts):
        print(f"\n🤖 Попытка {attempt + 1}/{max_attempts}...")

        # Запуск тестов
        test_result = run_function_tests(current_code, generated_test_code, language="python")
        parsed_result = parse_test_results(test_result)

        # Вывод результатов тестов
        print_test_results(parsed_result)

        if parsed_result["success"]:
            print("✅ Успех!")
            final_result = agent(final_answer(
                short_outcome="Все тесты пройдены.",
                detailed_outcome=f"Функция исправлена за {attempt + 1} попыток.\nКод:\n```python\n{current_code}\n```\nТесты:\n```python\n{generated_test_code}\n```",
                context="Тесты покрывают типичные и граничные случаи."
            ))
            return {
                "fixed_code": current_code,
                "generated_tests": generated_test_code,
                "iterations": attempt + 1,
                "final_result": parsed_result,
                "agent_response": final_result
            }

        # Формирование информации об ошибках
        failed_tests_info = "\n".join(
            f"Тест: {t['test_name']}\nТрассировка:\n{t['traceback']}\n"
            for t in parsed_result.get("failed_tests", [])
        ) or "Нет подробных ошибок."

        # Попытка исправить код
        fix_prompt = f"""\
Исходный код функции:
```python
{current_code}
```
Код тестов:
```python
{generated_test_code}
```
Ошибки тестов:
{failed_tests_info}

Верните только исправленный код функции в формате:
Code:
```py
def add(a, b):
    ...
```
<end_code>
"""
        new_code = extract_code_from_response(agent(fix_prompt))
        if not new_code:
            print("⚠️ Не удалось исправить код: модель вернула неверный формат.")
            continue
        current_code = new_code
        print(f"Исправленный код:\n{current_code}")

        # Перегенерация тестов
        print("🤖 Перегенерация тестов...")
        test_prompt = generate_unit_tests_from_code(current_code, failed_tests_info)
        new_test_code = extract_code_from_response(agent(test_prompt))
        if not new_test_code:
            print("⚠️ Не удалось сгенерировать новые тесты: модель вернула неверный формат.")
            continue
        generated_test_code = new_test_code
        print(f"Новые тесты:\n{generated_test_code}")

    print("❌ Не удалось пройти тесты за указанное количество попыток.")
    final_result = agent(final_answer(
        short_outcome="Не удалось пройти тесты.",
        detailed_outcome=f"После {max_attempts} попыток тесты не пройдены.\nПоследний код:\n```python\n{current_code}\n```\nТесты:\n```python\n{generated_test_code}\n```",
        context="Модель не смогла корректно интерпретировать ошибки."
    ))
    return {
        "fixed_code": current_code,
        "generated_tests": generated_test_code,
        "iterations": max_attempts,
        "final_result": parsed_result,
        "agent_response": final_result
    }