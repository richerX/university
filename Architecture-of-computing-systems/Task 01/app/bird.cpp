#include "bird.h"

// Ввод параметров
void In(bird& object, char** array) {
    for (int i = 0; i < 5; i++)
        object.name[i] = array[1][i];
    object.weight = atoi(array[2]);
    object.flyAway = (atoi(array[3]) % 2) == 1;
}

// Случайный ввод
void InRnd(bird& object) {
    NameRandom(object.name);
    object.weight = Random();
    int k = Random() % 2;
    if (k == 0) {
        object.flyAway = false;
    } else if (k == 1) {
        object.flyAway = true;
    }
}

// Вычисление параметра
double Value(bird& object) {
    int sum = 0;
    for (auto elem : object.name) {
        sum += static_cast<int>(elem);
    }
    return static_cast<double>(sum) / object.weight;
}

// Привод перечисления к строке
const char* ParseFlyAway(bool flyAway) {
    if (flyAway)
        return "TRUE";
    return "FALSE";
}

// Вывод параметров
void Out(bird& object, FILE* file) {
    printf("It is Bird : name = ");
    printf("%c", object.name[0]);
    printf("%c", object.name[1]);
    printf("%c", object.name[2]);
    printf("%c", object.name[3]);
    printf("%c", object.name[4]);
    printf(", weight = ");
    printf("%d", object.weight);
    printf(", value = ");
    printf("%f", Value(object));
    printf(", fly away = ");
    printf(ParseFlyAway(object.flyAway));
    printf("\n");
    
    fprintf(file, "It is Bird : name = ");
    fprintf(file, "%c", object.name[0]);
    fprintf(file, "%c", object.name[1]);
    fprintf(file, "%c", object.name[2]);
    fprintf(file, "%c", object.name[3]);
    fprintf(file, "%c", object.name[4]);
    fprintf(file, ", weight = ");
    fprintf(file, "%d", object.weight);
    fprintf(file, ", value = ");
    fprintf(file, "%f", Value(object));
    fprintf(file, ", fly away = ");
    fprintf(file, ParseFlyAway(object.flyAway));
    fprintf(file, "\n");
}
