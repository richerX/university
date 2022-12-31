import string    
import random
import time
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.common.alert import Alert

INCORRECT_LOGIN = "AqZ"
SLEEP_TIME = 1.5
MOVE_SLEEP_TIME = 7


def get_random_string(length = 20):
    return ''.join(random.choices(string.ascii_letters + string.digits, k = length))  


def logout(driver):
    driver.find_element(By.XPATH, "//input[1]").click()
    time.sleep(SLEEP_TIME)


def login(driver, login_id):
    driver.get("http://ruswizard.ddns.net:8091")
    login_box = driver.find_element(By.ID, "sessionId")
    login_box.clear()
    login_box.send_keys(login_id)
    driver.find_element(By.XPATH, "//input[2]").click()
    time.sleep(SLEEP_TIME)


# Parameterized decorator
def test(test_name, should_succeed):
    def decorator(function):
        def wrapper(*args):
            try:
                function(*args)
                print(f"[{'+' if should_succeed else '-'}] {test_name} test succeeded!")
            except:
                print(f"[{'-' if should_succeed else '+'}] {test_name} test failed!")
        return wrapper
    return decorator


@test("Incorrect login", False)
def incorrect_login(driver):
    driver.get("http://ruswizard.ddns.net:8091")
    login_box = driver.find_element(By.ID, "sessionId")
    login_box.clear()
    login_box.send_keys(INCORRECT_LOGIN)
    driver.find_element(By.XPATH, "//input[2]").click()
    time.sleep(SLEEP_TIME)
    # Проверка, что мы на странице логина
    driver.find_element(By.ID, "sessionId")


@test("Correct login", True)
def correct_login(driver, login_id):
    login(driver, login_id)
    # Проверка, что мы на странице игры
    driver.find_element(By.XPATH, "//img[1]")


@test("Correct logout", True)
def correct_logout(driver):
    logout(driver)
    # Проверка, что мы на странице логина
    driver.find_element(By.ID, "sessionId")


@test("Trade", True)
def trade_test(driver):
    # Переход в док
    found = driver.find_element(By.XPATH, "//a[1]").click()
    time.sleep(SLEEP_TIME)
    
    # Получение цен товаров
    prices = []
    for index in range(1, 9):
        value = driver.find_element(By.ID, f"item{index}buy").get_attribute("value")
        price = float("".join([symbol for symbol in value if symbol.isdigit() or symbol == "."]))
        prices.append(price)
    
    # Покупака самого дешевого товара
    index = prices.index(min(prices)) + 1
    count_field = driver.find_element(By.ID, f"item{index}cnt")
    count_field.clear()
    count_field.send_keys(1)
    driver.find_element(By.ID, f"item{index}buy").click()
    time.sleep(SLEEP_TIME)
    
    # Продажа купленного товара
    count_field = driver.find_element(By.ID, f"item{index}cnt")
    count_field.clear()
    count_field.send_keys(1)
    driver.find_element(By.ID, f"item{index}sell").click()
    time.sleep(SLEEP_TIME)
    
    # Проверка, что сумма игрока меньше изначальной
    assert float(driver.find_element(By.ID, "money").text) < 500


@test("Right edge of the world", True)
def world_right_edge_test(driver):
    for i in range(19):
        driver.find_element(By.ID, "arrowRight").click()
        time.sleep(MOVE_SLEEP_TIME)


@test("Move right JS at the edge of the world", True)
def move_right_at_edge(driver):
    driver.execute_script("moveRight()")
    time.sleep(MOVE_SLEEP_TIME)
    Alert(driver).accept()
    title = driver.find_element(By.XPATH, "//span[1]").text
    assert "19" in title


@test("Left edge of the world", True)
def world_left_edge_test(driver):
    for i in range(19):
        driver.find_element(By.ID, "arrowLeft").click()
        time.sleep(MOVE_SLEEP_TIME)


@test("Move left JS at the edge of the world", False)
def move_left_at_edge(driver):
    driver.execute_script("moveLeft()")
    time.sleep(MOVE_SLEEP_TIME)
    title = driver.find_element(By.XPATH, "//span[1]").text
    assert "19" in title


def main():
    driver = webdriver.Chrome()
    login_id = get_random_string()
    
    # Incorrect login tests
    incorrect_login(driver)
    logout(driver)
    
    # Login and logouts tests
    correct_login(driver, login_id)
    correct_logout(driver)
    
    # Trade tests
    login(driver, login_id)
    trade_test(driver)
    logout(driver)
    
    # Move right tests
    login(driver, login_id)
    world_right_edge_test(driver)
    move_right_at_edge(driver)
    logout(driver)
    
    # Move left tests
    login_id = get_random_string()
    login(driver, login_id)
    world_left_edge_test(driver)
    move_left_at_edge(driver)
    logout(driver)
    
    driver.close()


if __name__ == "__main__":
    main()
