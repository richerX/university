#pragma once

#include "animal.h"

struct container {
    //const int max_len = 10000; // максимальная длина
    int len; // текущая длина
    animal* cont[10000];
};

// Инициализация контейнера
void Init(container& c);

// Очистка контейнера от элементов (освобождение памяти)
void Clear(container& c);

// Парсинг входной строки
char** InStringParse(const char* string);

// Ввод содержимого контейнера из указанного потока
void In(container& c, FILE* file);

// Случайный ввод содержимого контейнера
void InRnd(container& c, int size);

// Вывод содержимого контейнера в указанный поток
void Out(container& c, FILE* file);

// Вычисление суммы периметров всех фигур в контейнере
double TotalValue(container& c);
