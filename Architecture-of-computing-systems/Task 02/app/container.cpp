#include "container.h"


Container::Container() {
    length = 0;
}

Container::~Container() {
    for (int i = 0; i < length; i++) {
        delete storage[i];
    }
    length = 0;
}

void Container::In(ifstream& ifst) {
    //ofstream ofstDebug("files/debug.txt");
    while (!ifst.eof() && length < 10000) {
        if ((storage[length] = Animal::StaticIn(ifst)) != 0) {
            if (storage[length]->Value() >= 0) {
                length++;
            }
            //cout << "DEBUG ";// << (storage[length]-> == -nan) << " ";
            //storage[length]->Out(ofstDebug);
            //length++;
        }
    }
}

void Container::InRnd(int size) {
    while (length < size) {
        if ((storage[length] = Animal::StaticInRnd()) != 0) {
            length++;
        }
    }
}

void Container::Out(ofstream& ofst) {
    cout << "Container contains " << length << " elements.\n";
    ofst << "Container contains " << length << " elements.\n";
    for (int i = 0; i < length; i++) {
        cout << i + 1 << ": ";
        ofst << i + 1 << ": ";
        storage[i]->Out(ofst);
    }
}

double Container::Value() {
    double sum = 0;
    for (int i = 0; i < length; i++) {
        sum += (*storage[i]).Value();
    }
    return sum;
}

