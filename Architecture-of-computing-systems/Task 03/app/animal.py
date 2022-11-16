from application.constants import *


class Animal:
    animal_type = None
    name = None
    weight = None
    animal_subtype = None

    # animal_type name weight animal_subtype
    def __init__(self, string):
        array = string.split()
        self.animal_type = int(array[0])
        self.name = array[1]
        self.weight = int(array[2])
        self.animal_subtype = int(array[3])

    def __str__(self):
        return f"{self.name.center(name_max_length)} |  " \
               f"{self.animal_type_name.ljust(type_max_length)} ({self.animal_subtype_name.center(subtype_max_length)})  " \
               f"{(str(self.weight) + 'gr.').ljust(weight_max_length)} " \
               f"(value = {'{:.5f}'.format(self.value)})"

    @property
    def animal_type_name(self):
        key = self.animal_type
        if animal_types.get(key) is not None:
            return animal_types[key]
        return "NOT DEFINED ANIMAL TYPE NAME"

    @property
    def animal_subtype_name(self):
        key = (self.animal_type, self.animal_subtype)
        if animal_subtypes.get(key) is not None:
            return animal_subtypes[key]
        return "NOT DEFINED SPECIAL TYPE NAME"

    @property
    def value(self):
        if self.weight == 0:
            return -1
        return sum([ord(char) for char in self.name]) / self.weight
