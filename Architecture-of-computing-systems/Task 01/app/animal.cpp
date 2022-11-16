#include "animal.h"

// Ввод параметров
animal* In(char** array) {
    animal *object;
    int k = atoi(array[0]) % 3;
    switch (k) {
        case 0:
            object = new animal;
            object->type = animal::key::FISH;
            In(object->fishInst, array);
            return object;
        case 1:
            object = new animal;
            object->type = animal::key::BIRD;
            In(object->birdInst, array);
            return object;
        case 2:
            object = new animal;
            object->type = animal::key::BEAST;
            In(object->beastInst, array);
            return object;
        default:
            return 0;
    }
}

// Случайный ввод
animal* InRnd() {
    animal *object;
    int k = rand() % 3;
    switch (k) {
        case 0: {
            object = new animal;
            object->type = animal::key::FISH;
            InRnd(object->fishInst);
            return object;
            }
        case 1: {
            object = new animal;
            object->type = animal::key::BIRD;
            InRnd(object->birdInst);
            return object;
        }
        case 2: {
            object = new animal;
            object->type = animal::key::BEAST;
            InRnd(object->beastInst);
            return object;
        }
        default:
            return 0;
    }
}

// Вывод параметров
void Out(animal& object, FILE* file) {
    switch (object.type) {
    case animal::key::FISH:
        Out(object.fishInst, file);
        break;
    case animal::key::BIRD:
        Out(object.birdInst, file);
        break;
    case animal::key::BEAST:
        Out(object.beastInst, file);
        break;
    default:
        fprintf(file, "Incorrect animal!\n");
    }
}

// Вычисление параметрa
double Value(animal& object) {
    switch (object.type) {
        case animal::key::FISH:
            return Value(object.fishInst);
            break;
        case animal::key::BIRD:
            return Value(object.birdInst);
            break;
        case animal::key::BEAST:
            return Value(object.beastInst);
            break;
        default:
            return 0.0;
    }
}
