using System;
using System.Collections.Generic;

class Kikstarter
{
    // Данный тип необходимо обязательно использовать
    public delegate int GetMoneyDelegate();

    List<GetMoneyDelegate> getMoney = new List<GetMoneyDelegate>();
    public int m;

    public Kikstarter(int m, Hipster[] hipsters)
    {
        if (hipsters.Length == 0)
            throw new ArgumentException("Not enough hipsters");
        GetMoneyDelegate del;
        foreach (var hipster in hipsters)
        {
            del = hipster.GetMoney;
            getMoney.Add(del);
        }
        this.m = m;
    }

    public int Run()
    {
        int months = 0;
        int totalSum = 0;
        int curSum;
        while (totalSum < m)
        {
            curSum = SumForTheMonth();
            if (curSum == 0)
                throw new InvalidOperationException("Not enough money");
            totalSum += curSum;
            months += 1;
        }
        return months;
    }

    public int SumForTheMonth()
    {
        int answer = 0;
        foreach (var del in getMoney)
        {
            answer += del();
        }
        return answer;
    }
}