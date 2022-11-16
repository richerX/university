import random



filename = "input10.txt"
length = 10**4

with open(filename, 'w') as file:
    for i in range(length):
        a = random.randint(0, 2)
        name = ""
        for i in range(5):
            name += chr(random.randint(ord('a'), ord('z')))
        b = random.randint(100, 10**6)
        if a == 0:
            c = random.randint(0, 3)
        if a == 1:
            c = random.randint(0, 1)
        if a == 2:
            c = random.randint(0, 2)
        file.write(str(a) + " " + name.capitalize() + " " + str(b) + " " + str(c) + "\n")
        