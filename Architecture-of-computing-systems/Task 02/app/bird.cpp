#include "bird.h"


void Bird::In(ifstream& ifst) {
    int key;
    ifst >> name >> weight >> key;
    key %= 2;
    if (key == 0) {
        flyAway = false;
    } else if (key == 1) {
        flyAway = true;
    }
}

void Bird::InRnd() {
    int key = random.GetNumber() % 2;
    name = random.GetName();
    weight = random.GetNumber();
    if (key == 0) {
        flyAway = false;
    } else if (key == 1) {
        flyAway = true;
    }
}

string ParseType(bool key) {
    if (key) {
        return string("TRUE");
    }
    return string("FALSE");
}

void Bird::Out(ofstream& ofst) {
    cout << "It is Bird : name = " << name << ", weight = " << weight << ", value = " << Value() << ", fly away = " << ParseType(flyAway) << "\n";
    ofst << "It is Bird : name = " << name << ", weight = " << weight << ", value = " << Value() << ", fly away = " << ParseType(flyAway) << "\n";
}
