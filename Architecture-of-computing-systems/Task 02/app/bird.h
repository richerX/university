#pragma once

#include "animal.h"
#include "includes.h"


class Bird : public Animal {
public:
    virtual ~Bird() {};
    virtual void In(ifstream& ifst);
    virtual void InRnd();
    virtual void Out(ofstream& ofst);

private:
    bool flyAway;
};

