from ai_agent_handler import fix_function_with_ai
from pprint import pprint

if __name__ == "__main__":
    func = '''
def add(a, b):
    return a - b
    '''

    # Инициализация для генерации тестов
    output_analyzer = {"success": False, "failed_tests": []}

    # Запуск процесса исправления
    result = fix_function_with_ai(output_analyzer, func)
    pprint(result)