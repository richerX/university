#pragma once

#include "animal.h"
#include "includes.h"


enum class beastType { PREDATOR, HERBIVORES, INSECTIVORES };

class Beast : public Animal {
public:
    virtual ~Beast() {};
    virtual void In(ifstream& ifst);
    virtual void InRnd();
    virtual void Out(ofstream& ofst);

private:
    beastType type;
};

