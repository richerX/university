#pragma once

#include "fish.h"
#include "bird.h"
#include "beast.h"

struct animal {
    enum class key {FISH, BIRD, BEAST};
    key type;
    union {
        fish fishInst;
        bird birdInst;
        beast beastInst;
    };
};

// Ввод
animal* In(char** string);

// Случайный ввод
animal* InRnd();

// Вывод
void Out(animal& s, FILE* file);

// Вычисление параметра
double Value(animal& s);
