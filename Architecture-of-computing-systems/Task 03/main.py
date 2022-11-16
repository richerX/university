import sys
import time
from random import randint, choice
from application.animal import *


# Print in console and log in file
def my_print(text, filepath = None):
    print(text)
    if filepath is not None:
        with open(f"{filepath}.txt", "a") as file:
            file.write(str(text) + '\n')


# Print default error message
def error_message():
    my_print("Incorrect command line! Waited:")
    my_print("\tcommand -f infile outfile")
    my_print("\tcommand -n number outfile")


# Print error message
def error(index, text = ""):
    error_message()
    my_print(text)
    return index


# Item binary search in storage
def binary_search(storage, item, left, right):
    if right <= left:
        return left + 1 if item > storage[left].value else left
    middle = (left + right) // 2
    if item == storage[middle].value:
        return middle + 1
    if item > storage[middle].value:
        return binary_search(storage, item, middle + 1, right)
    return binary_search(storage, item, left, middle - 1)


# Binary insertion sort of the storage
def binary_insertion_sort(storage):
    for i in range(1, len(storage)):
        selected = storage[i]
        new_location = binary_search(storage, selected.value, 0, i - 1)
        for j in range(i - 1, new_location - 1, -1):
            storage[j + 1] = storage[j]
        storage[new_location] = selected


# Read animals from file
def read_animals_from_file(filepath):
    with open(f"{filepath}.txt", "r") as file:
        return [Animal(line) for line in file]


# Create random animals
def create_random_animals(length):
    answer = []
    animal_numbers = [fish_id, bird_id, beast_id]
    for i in range(length):
        name = "".join([chr(randint(ord('a'), ord('z'))) for _ in range(randint(3, name_max_length))])
        string = f"{choice(animal_numbers)} {name.capitalize()} {randint(100, 9999)} {randint(0, 2)}"
        answer.append(Animal(string))
    return answer


# Main method
def main(argv):
    start_time = time.time()

    if len(argv) < 4:
        return error(1, "Incorrect number of arguments. Waited for at least 4 arguments")
    if argv[1] not in ["-f", "-n"]:
        return error(2, "Incorrect second argument. Use -f or -n.")

    if argv[1] == "-f":
        try:
            animals = read_animals_from_file(argv[2])
        except Exception as exception:
            return error(3, f"Incorrect input file. {exception}")
    else:
        try:
            size = int(argv[2])
        except ValueError:
            return error(4, "Incorrect third argument. Waited for number.")
        if size < 1 or 10000 < size:
            return error(5, f"Incorrect number of figures = {size}. Set 1 <= number <= 10000.")
        animals = create_random_animals(size)
    binary_insertion_sort(animals)

    try:
        output_filepath = argv[3]
        file = open(f"{output_filepath}.txt", "w")
        file.close()
    except Exception as exception:
        return error(6, f"Incorrect output file. {exception}")

    my_print(f"Filled container with {len(animals)} animals.", output_filepath)
    my_print(f"Value sum = {'{:.5f}'.format(sum([animal.value for animal in animals]))}.", output_filepath)
    for animal in animals:
        my_print(animal, output_filepath)
    my_print(f"Total time = {'{:.5f}'.format(time.time() - start_time)} seconds.", output_filepath)
    return 0


if __name__ == "__main__":
    main(sys.argv)

    # main("main.py -n 10000 files/output".split())
    # main(f"main.py -f files/input files/output".split())

    # for file_number_index in range(1, 11):
    #     file_number = str(file_number_index).rjust(2, '0')
    #     main(f"main.py -f files/input{file_number} files/output{file_number}".split())
