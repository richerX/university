using System;
//using System.Numerics;

public class Rational
{
    int numerator;
    int denomenator;
    
    public Rational(int numer, int denom)
    {
        //Console.WriteLine($"NEW RATIONAL {numer} / {denom}");
        if (denom < 0)
        {
            numer *= -1;
            denom *= -1;
        }
        var gcd = GCD(Math.Abs(numer), Math.Abs(denom));
        numer /= gcd;
        denom /= gcd;

        this.numerator = numer;
        this.denomenator = denom;
    }

    public static int GCD(int a, int b)
    {
        //return (int)System.Numerics.BigInteger.GreatestCommonDivisor(a, b);

        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }
        return a | b;
    }

    public static Rational Parse(string input)
    {
        if (input.Contains('/'))
        {
            var numbers = input.Split('/');
            var num1 = int.Parse(numbers[0]);
            var num2 = int.Parse(numbers[1]);
            return new Rational(num1, num2);
        }
        return new Rational(int.Parse(input), 1);
    }

    public override string ToString()
    {
        if (denomenator == 1)
            return numerator.ToString();
        return numerator + "/" + denomenator;
    }

    public static Rational operator +(Rational a, Rational b)
    {
        var num1 = a.numerator * b.denomenator + b.numerator * a.denomenator;
        var num2 = a.denomenator * b.denomenator;
        return new Rational(num1, num2);
    }

    public static Rational operator -(Rational a, Rational b)
    {
        var num1 = a.numerator * b.denomenator - b.numerator * a.denomenator;
        var num2 = a.denomenator * b.denomenator;
        return new Rational(num1, num2);
    }

    public static Rational operator *(Rational a, Rational b)
    {
        var num1 = a.numerator * b.numerator;
        var num2 = a.denomenator * b.denomenator;
        return new Rational(num1, num2);
    }

    public static Rational operator /(Rational a, Rational b)
    {
        var num1 = a.numerator * b.denomenator;
        var num2 = a.denomenator * b.numerator;
        return new Rational(num1, num2);
    }
}