#pragma once

#include "includes.h"

struct beast {
    char name[5];
    int weight;
    enum class key { PREDATOR, HERBIVORES, INSECTIVORES };
    key type;
};

// Ввод параметров из файла
void In(beast& t, char** string);

// Случайный ввод параметров
void InRnd(beast& b);

// Вывод параметров в форматируемый поток
void Out(beast& t, FILE* file);

// Вычисление периметра
double Value(beast& t);
