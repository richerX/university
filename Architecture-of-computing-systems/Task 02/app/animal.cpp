#include "animal.h"
#include "bird.h"
#include "beast.h"
#include "fish.h"


Random Animal::random = Random();

Animal* Animal::StaticIn(ifstream& ifst) {
    int animalNumber;
    ifst >> animalNumber;
    Animal* animal = nullptr;
    if (animalNumber == 0) {
        animal = new Fish();
    } else if (animalNumber == 1) {
        animal = new Bird();
    } else if (animalNumber == 2) {
        animal = new Beast();
    }
    if (animalNumber >= 0 && animalNumber <= 2) {
        animal->In(ifst);
    }
    return animal;
}

Animal* Animal::StaticInRnd() {
    int animalNumber = random.GetNumber() % 3;
    Animal* animal = nullptr;
    if (animalNumber == 0) {
        animal = new Fish();
    } else if (animalNumber == 1) {
        animal = new Bird();
    } else if (animalNumber == 2) {
        animal = new Beast();
    }
    animal->InRnd();
    return animal;
}

double Animal::Value() {
    if (weight == 0) {
    	return -1;
    }
    int sum = 0;
    for (auto elem : name) {
        sum += static_cast<int>(elem);
    }
    return static_cast<double>(sum) / weight;
}
