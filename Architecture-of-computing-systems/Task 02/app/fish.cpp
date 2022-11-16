#include "fish.h"


void Fish::In(ifstream& ifst) {
    int key;
    ifst >> name >> weight >> key;
    key %= 4;
    if (key == 0) {
        place = fishPlace::LAKE;
    } else if (key == 1) {
        place = fishPlace::SEA;
    } else if (key == 2) {
        place = fishPlace::OCEAN;
    } else if (key == 3) {
        place = fishPlace::RIVER;
    }
}

void Fish::InRnd() {
    int key = random.GetNumber() % 4;
    name = random.GetName();
    weight = random.GetNumber();
    if (key == 0) {
        place = fishPlace::LAKE;
    } else if (key == 1) {
        place = fishPlace::SEA;
    } else if (key == 2) {
        place = fishPlace::OCEAN;
    } else if (key == 3) {
        place = fishPlace::RIVER;
    }
}

string ParseType(fishPlace key) {
    switch (key) {
        case fishPlace::LAKE:
            return string("LAKE");
        case fishPlace::OCEAN:
            return string("OCEAN");
        case fishPlace::RIVER:
            return string("RIVER");
        case fishPlace::SEA:
            return string("SEA");
        default:
            return "";
    }
}

void Fish::Out(ofstream& ofst) {
    cout << "It is Fish : name = " << name << ", weight = " << weight << ", value = " << Value() << ", place = " << ParseType(place) << "\n";
    ofst << "It is Fish : name = " << name << ", weight = " << weight << ", value = " << Value() << ", place = " << ParseType(place) << "\n";
}
