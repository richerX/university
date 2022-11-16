#pragma once

#include "animal.h"
#include "includes.h"


enum class fishPlace { LAKE, SEA, OCEAN, RIVER };

class Fish : public Animal {
public:
    virtual ~Fish() {};
    virtual void In(ifstream& ifst);
    virtual void InRnd();
    virtual void Out(ofstream& ofst);

private:
    fishPlace place;
};

