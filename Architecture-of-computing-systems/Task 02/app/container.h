#pragma once

#include "animal.h"


class Container {
public:
    Container();
    ~Container();

    void In(ifstream& ifst);
    void InRnd(int size);
    void Out(ofstream& ofst);
    double Value();

    int length;
    Animal* storage[10000];
};

