#pragma once

#include <iostream>
#include <fstream>
#include <cstring>
using namespace std;


class Random {
public:
    Random(int firstParam = 1300, int lastParam = 10000);
    int GetNumber();
    string GetName();

private:
    int first;
    int last;
};
