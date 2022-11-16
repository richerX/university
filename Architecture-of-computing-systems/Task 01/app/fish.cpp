#include "fish.h"

// Ввод параметров
void In(fish& object, char** array) {
    for (int i = 0; i < 5; i++)
        object.name[i] = array[1][i];
    object.weight = atoi(array[2]);
    int k = atoi(array[3]) % 4;
    if (k == 0) {
        object.place = fish::key::LAKE;
    } else if (k == 1) {
        object.place = fish::key::SEA;
    } else if (k == 2) {
        object.place = fish::key::OCEAN;
    } else if (k == 3) {
        object.place = fish::key::RIVER;
    }
}

// Случайный ввод
void InRnd(fish& object) {
    NameRandom(object.name);
    object.weight = Random();
    int k = Random() % 4;
    if (k == 0) {
        object.place = fish::key::LAKE;
    }
    else if (k == 1) {
        object.place = fish::key::SEA;
    }
    else if (k == 2) {
        object.place = fish::key::OCEAN;
    }
    else if (k == 3) {
        object.place = fish::key::RIVER;
    }
}

// Привод перечисления к строке
const char* ParseType(fish::key type) {
    switch (type) {
        case fish::key::LAKE:
            return "LAKE";
        case fish::key::OCEAN:
            return "OCEAN";
        case fish::key::RIVER:
            return "RIVER";
        case fish::key::SEA:
            return "SEA";
        default:
            return "";
    }
}

// Вычисление параметра
double Value(fish& object) {
    int sum = 0;
    for (auto elem : object.name) {
        sum += static_cast<int>(elem);
    }
    return static_cast<double>(sum) / object.weight;
}

// Вывод параметров
void Out(fish& object, FILE* file) {
    printf("It is Fish : name = ");
    printf("%c", object.name[0]);
    printf("%c", object.name[1]);
    printf("%c", object.name[2]);
    printf("%c", object.name[3]);
    printf("%c", object.name[4]);
    printf(", weight = ");
    printf("%d", object.weight);
    printf(", value = ");
    printf("%f", Value(object));
    printf(", place = ");
    printf(ParseType(object.place));
    printf("\n");
    
    fprintf(file, "It is Fish : name = ");
    fprintf(file, "%c", object.name[0]);
    fprintf(file, "%c", object.name[1]);
    fprintf(file, "%c", object.name[2]);
    fprintf(file, "%c", object.name[3]);
    fprintf(file, "%c", object.name[4]);
    fprintf(file, ", weight = ");
    fprintf(file, "%d", object.weight);
    fprintf(file, ", value = ");
    fprintf(file, "%f", Value(object));
    fprintf(file, ", place = ");
    fprintf(file, ParseType(object.place));
    fprintf(file, "\n");
}
