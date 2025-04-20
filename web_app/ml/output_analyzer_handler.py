from typing import Dict, Any
import re

def parse_test_results(test_result: Dict[str, Any]) -> Dict[str, Any]:
    stdout = test_result.get("stdout", "")
    stderr = test_result.get("stderr", "")
    returncode = test_result.get("returncode", 1)

    # Ищем количество тестов
    test_summary = re.search(r"Ran (\d+) test", stdout)
    total_tests = int(test_summary.group(1)) if test_summary else 0

    failed_tests = []
    passed_tests = []

    # Парсим вывод тестов
    test_lines = stdout.splitlines()
    current_test = None
    current_traceback = []

    for line in test_lines + stderr.splitlines():
        test_match = re.match(r"^(test_\w+)", line)
        if test_match:
            if current_test:
                failed_tests.append({
                    "test_name": current_test,
                    "traceback": "\n".join(current_traceback)
                })
            current_test = test_match.group(1)
            current_traceback = [line]
        elif current_test:
            current_traceback.append(line)
        elif "OK" in line:
            passed_tests = [{"test_name": f"test_passed_{i+1}"} for i in range(total_tests)]

    if current_test:
        failed_tests.append({
            "test_name": current_test,
            "traceback": "\n".join(current_traceback)
        })

    success = returncode == 0 and len(failed_tests) == 0 and total_tests > 0

    return {
        "success": success,
        "total_tests": total_tests,
        "passed_tests": passed_tests,
        "failed_tests": failed_tests,
        "error": stderr if stderr else None
    }