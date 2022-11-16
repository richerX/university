#pragma once

#include "includes.h"

struct fish {
    char name[5];
    int weight;
    enum class key { LAKE, SEA, OCEAN, RIVER };
    key place;
};

// Ввод параметров из файла
void In(fish& t, char** string);

// Случайный ввод параметров
void InRnd(fish& e);

// Вывод параметров в форматируемый поток
void Out(fish& t, FILE* file);

// Вычисление периметра
double Value(fish& t);
