using System;

public class Programmer
{
    public int id;
    public int linesOfCode;
    public int adminId;

    private int GetAlmostRandomNumber()
    {
        return (int) Math.Abs(Math.Sin(GetIdDigitsSum() % 11 + 1) * 12345);
    }

    public Programmer(int id)
    {
        this.id = id;
        this.linesOfCode = GetAlmostRandomNumber();

    }

    private int GetIdDigitsSum()
    {
        var sum = 0;
        var idCopy = id;
        while (idCopy != 0)
        {
            sum += idCopy % 10;
            idCopy /= 10;
        }
        return sum;
    }

    public override string ToString()
    {
        return String.Format("Id: {0}\nLines of code: {1}", id, linesOfCode);
    }
}