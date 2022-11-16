#include "includes.h"


static Random smallLetter = Random('a', 'z');
static Random bigLetter = Random('A', 'Z');

Random::Random(int firstParam, int lastParam) {
    first = firstParam;
    last = lastParam;
    srand(static_cast<unsigned int>(time(0)));
}

int Random::GetNumber() {
    return rand() % (last - first + 1) + first;
}

string Random::GetName() {
    string answer = string();
    answer += static_cast<char>(bigLetter.GetNumber());
    for (int i = 0; i < rand() % 7 + 3; i++) {
        answer += static_cast<char>(smallLetter.GetNumber());
    }
    return answer;
}
