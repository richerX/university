#pragma once

#include "includes.h"


class Animal {
public:
    static Random random;
    static Animal* StaticIn(ifstream& ifdt);
    static Animal* StaticInRnd();
    double Value();

    virtual ~Animal() {};
    virtual void In(ifstream& ifdt) = 0;
    virtual void InRnd() = 0;
    virtual void Out(ofstream& ofst) = 0;

protected:
    string name;
    int weight;
};

