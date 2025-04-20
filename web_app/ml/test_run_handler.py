import tempfile
import subprocess
import os
from typing import Dict, Any
import unittest

def run_function_tests(func_code: str, test_code: str, language: str = "python") -> Dict[str, Any]:
    with tempfile.TemporaryDirectory() as temp_dir:
        # Создаём файл с функцией и тестами
        test_file_path = os.path.join(temp_dir, "test_file.py")
        with open(test_file_path, "w", encoding="utf-8") as f:
            # Убираем if __name__ == '__main__' из test_code
            cleaned_test_code = test_code.replace("if __name__ == '__main__':\n    unittest.main()", "")
            f.write(func_code.strip() + "\n\n" + cleaned_test_code.strip())

        # Исправляем путь для Windows
        test_file_path = test_file_path.replace("\\", "/")
        # Запускаем тесты через unittest
        try:
            result = subprocess.run(
                ["python", "-m", "unittest", "-v", test_file_path],
                capture_output=True,
                text=True,
                timeout=10
            )
            return {
                "stdout": result.stdout,
                "stderr": result.stderr,
                "returncode": result.returncode
            }
        except subprocess.TimeoutExpired:
            return {
                "stdout": "",
                "stderr": "Tests timed out",
                "returncode": 1
            }