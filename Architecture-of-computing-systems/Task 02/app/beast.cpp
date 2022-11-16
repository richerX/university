#include "beast.h"


void Beast::In(ifstream& ifst) {
    int key;
    ifst >> name >> weight >> key;
    key %= 3;
    if (key == 0) {
        type = beastType::PREDATOR;
    } else if (key == 1) {
        type = beastType::HERBIVORES;
    } else if (key == 2) {
        type = beastType::INSECTIVORES;
    }
}

void Beast::InRnd() {
    int key = random.GetNumber() % 3;
    name = random.GetName();
    weight = random.GetNumber();
    if (key == 0) {
        type = beastType::PREDATOR;
    } else if (key == 1) {
        type = beastType::HERBIVORES;
    } else if (key == 2) {
        type = beastType::INSECTIVORES;
    }
}

string ParseType(beastType key) {
    switch (key) {
        case beastType::PREDATOR:
            return string("PREDATOR");
        case beastType::HERBIVORES:
            return string("HERBIVORES");
        case beastType::INSECTIVORES:
            return string("INSECTIVORES");
        default:
            return "";
    }
}

void Beast::Out(ofstream& ofst) {
    cout << "It is Beast : name = " << name << ", weight = " << weight << ", value = " << Value() << ", type = " << ParseType(type) << "\n";
    ofst << "It is Beast : name = " << name << ", weight = " << weight << ", value = " << Value() << ", type = " << ParseType(type) << "\n";
}
