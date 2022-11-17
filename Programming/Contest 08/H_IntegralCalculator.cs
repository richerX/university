using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class IntegralCalculator
{
    static double step = Program.EPSYLON;

    public static void InsertParameter(int param)
    {
        if (param == 0)
            Program.func = SolveIntegralSin;
        if (param == 1)
            Program.func = SolveIntegralCos;
        if (param == 2)
            Program.func = SolveIntegralTan;
    }

    public static double SolveIntegralSin(double left, double right)
    {
        double answer = 0;
        for (double i = left; i < right; i += step)
            answer += (Math.Sin(i) + Math.Sin(i + step)) * step / 2;
        return answer;
    }

    public static double SolveIntegralCos(double left, double right)
    {
        double answer = 0;
        for (double i = left; i < right; i += step)
            answer += (Math.Cos(i) + Math.Cos(i + step)) * step / 2;
        return answer;
    }

    public static double SolveIntegralTan(double left, double right)
    {
        double answer = 0;
        for (double i = left; i < right; i += step)
            answer += (Math.Tan(i) + Math.Tan(i + step)) * step / 2;
        return answer;
    }
}

