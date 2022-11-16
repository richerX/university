using System;

internal class MyGiveawayHelper
{
    int count = 0;
    int number = 1579;
    string[] logins;
    string[] prizes;

    public MyGiveawayHelper(string[] logins, string[] prizes)
    {
        this.logins = logins;
        this.prizes = prizes;
    }

    public bool HasPrizes => count < prizes.Length;

    public (string prize,string login) GetPrizeLogin()
    {
        number = NextNumber(number);
        int index = number % logins.Length;
        count += 1;
        return (prizes[count - 1], logins[index]);
    }

    public int NextNumber(int number)
    {
        return (number * number) / 100 % 10000;
    }
}   