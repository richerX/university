#pragma once

#include "includes.h"

struct bird {
    char name[5];
    int weight;
    bool flyAway;
};

// Ввод параметров из файла
void In(bird& t, char** string);

// Случайный ввод параметров
void InRnd(bird& e);

// Вывод параметров в форматируемый поток
void Out(bird& t, FILE* file);

// Вычисление периметра
double Value(bird& t);
