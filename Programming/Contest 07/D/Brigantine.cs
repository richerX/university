using System;

class Brigantine : Boat
{
    public Brigantine(int value, bool isAtThePort) : base(value, isAtThePort)
    {
        this.value = value;
        this.IsAtThePort = isAtThePort;
    }

    public new int CountCost(int weight)
    {
        if (weight <= 500)
            return weight * value * value;
        return weight * value;
    }
}