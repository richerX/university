from random import randint, choice


name_max_length = 12
lengths = [3, 20, 20, 40, 100, 500, 500, 500, 10000, 10000]
animal_numbers = [0, 1, 2]
for i in range(len(lengths)):
    length = lengths[i]
    filename = f"input{str(i + 1).rjust(2, '0')}.txt"
    with open(filename, 'w') as file:
        for i in range(length):
            name = "".join([chr(randint(ord('a'), ord('z'))) for _ in range(randint(3, name_max_length))])
            string = f"{choice(animal_numbers)} {name.capitalize()} {randint(100, 9999)} {randint(0, 1)}"
            file.write(string + "\n")
