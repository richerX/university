using System;

class Boat
{
    public int value;
    public bool IsAtThePort;

    public Boat(int value, bool isAtThePort)
    {
        this.value = value;
        this.IsAtThePort = isAtThePort;
    }

    public int CountCost(int weight)
    {
        return value * weight;
    }
}


