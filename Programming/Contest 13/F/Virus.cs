using System;

public class Virus : IComparable
{
    private int infectedCount;
    private double dangerIndex;
    private int typeNumber;

    public Virus(int infectedCount, double dangerIndex, int typeNumber)
    {
        this.infectedCount = infectedCount;
        this.dangerIndex = dangerIndex;
        this.typeNumber = typeNumber;
    }

    private Virus(Virus virus)
    {
        this.infectedCount = virus.infectedCount;
        this.dangerIndex = virus.dangerIndex;
        this.typeNumber = virus.typeNumber;
    }

    public static Virus GetSum(Virus[] arr, int firstN)
    {
        if (firstN > arr.Length || firstN < 1)
            throw new ArgumentException("Incorrect value");
        
        var answer = arr[0];
        for (int i = 1; i < firstN; i++)
            answer += arr[i];
        return answer;
    }
    
    public static Virus GetDifference(Virus[] arr, int lastN)
    {
        if (lastN > arr.Length || lastN < 1)
            throw new ArgumentException("Incorrect value");

        var answer = arr[arr.Length - 1];
        for (int i = arr.Length - 2; i >= arr.Length - lastN; i--)
            answer -= arr[i];
        return answer;
    }

    public override string ToString()
    {
        return $"{infectedCount} {dangerIndex:F2} {typeNumber}";
    }

    public int CompareTo(object obj2)
    {
        var obj = (Virus)obj2;
        if (dangerIndex > obj.dangerIndex)
            return 1;
        else if (dangerIndex < obj.dangerIndex)
            return -1;

        if (infectedCount > obj.infectedCount)
            return 1;
        else if (infectedCount < obj.infectedCount)
            return -1;

        if (typeNumber > obj.typeNumber)
            return 1;
        else if (typeNumber < obj.typeNumber)
            return -1;

        return 0;
    }

    public static Virus operator +(Virus a, Virus b)
    {
        int newCount = a.infectedCount + b.infectedCount;
        int newType = a.typeNumber | b.typeNumber;
        double newDanger = ((double)newCount) / (a.typeNumber & b.typeNumber);
        Console.WriteLine($"{a.typeNumber} + {b.typeNumber} = {newType} ({a.typeNumber & b.typeNumber})");
        return new Virus(newCount, newDanger, newType);
    }

    public static Virus operator -(Virus a, Virus b)
    {
        int newCount = Math.Abs(a.infectedCount - b.infectedCount);
        int newType = a.typeNumber & b.typeNumber;
        double newDanger = ((double)newCount) / ((double)newType);
        Console.WriteLine($"{a.typeNumber} - {b.typeNumber} = {newType}");
        return new Virus(newCount, newDanger, newType);
    }
}