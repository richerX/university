using System;

public delegate void Calc(ref double x);

class StackCalculator
{
    public static void CreateRules(int[] args)
    {
        Program.func = Skip;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == 0)
                Program.func += Sin;
            if (args[i] == 1)
                Program.func += Cos;
            if (args[i] == 2)
                Program.func += Tan;
        }
    }

    public static void Skip(ref double x)
    {
        return;
    }

    public static void Sin(ref double x)
    {
        x = Math.Sin(x);
    }

    public static void Cos(ref double x)
    {
        x = Math.Cos(x);
    }

    public static void Tan(ref double x)
    {
        x = Math.Tan(x);
    }
}