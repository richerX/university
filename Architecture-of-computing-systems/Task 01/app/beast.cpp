#include "beast.h"

// Ввод параметров
void In(beast& object, char** array) {
    for (int i = 0; i < 5; i++)
        object.name[i] = array[1][i];
    object.weight = atoi(array[2]);
    int k = atoi(array[3]) % 3;
    if (k == 0) {
        object.type = beast::key::PREDATOR;
    } else if (k == 1) {
        object.type = beast::key::HERBIVORES;
    } else if (k == 2) {
        object.type = beast::key::INSECTIVORES;
    }
}

// Ввод параметров
void InRnd(beast& object) {
    NameRandom(object.name);
    object.weight = Random();
    int k = Random() % 3;
    if (k == 0) {
        object.type = beast::key::PREDATOR;
    } else if (k == 1) {
        object.type = beast::key::HERBIVORES;
    } else if (k == 2) {
        object.type = beast::key::INSECTIVORES;
    }
}

// Парсинг
const char* ParseType(beast::key type) {
    switch (type) {
    case beast::key::HERBIVORES:
        return "HERBIVORES";
    case beast::key::INSECTIVORES:
        return "INSECTIVORES";
    case beast::key::PREDATOR:
        return "PREDATOR";
    default:
        return "";
    }
}

// Вычисление значения
double Value(beast& object) {
    int sum = 0;
    for (auto elem : object.name) {
        sum += static_cast<int>(elem);
    }
    return static_cast<double>(sum) / object.weight;
}

// Вывод параметров
void Out(beast& object, FILE* file) {
    printf("It is Beast: name = ");
    printf("%c", object.name[0]);
    printf("%c", object.name[1]);
    printf("%c", object.name[2]);
    printf("%c", object.name[3]);
    printf("%c", object.name[4]);
    printf(", weight = ");
    printf("%d", object.weight);
    printf(", value = ");
    printf("%f", Value(object));
    printf(", type = ");
    printf(ParseType(object.type));
    printf("\n");
    
    fprintf(file, "It is Beast: name = ");
    fprintf(file, "%c", object.name[0]);
    fprintf(file, "%c", object.name[1]);
    fprintf(file, "%c", object.name[2]);
    fprintf(file, "%c", object.name[3]);
    fprintf(file, "%c", object.name[4]);
    fprintf(file, ", weight = ");
    fprintf(file, "%d", object.weight);
    fprintf(file, ", value = ");
    fprintf(file, "%f", Value(object));
    fprintf(file, ", type = ");
    fprintf(file, ParseType(object.type));
    fprintf(file, "\n");
}
