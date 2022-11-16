#include "includes.h"

int Random() {
    return rand() % 5000 + 1000;
}

void NameRandom(char mas[]) {
    mas[0] = rand() % 26 + 65;
    for (int i = 1; i < 5; i++) {
        mas[i] = rand() % 26 + 97;
    }
}
