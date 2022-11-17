using System;
using System.Collections;
using System.Collections.Generic;

public class Army : IEnumerable
{
    public int[] soldiers;
    public int n;
    public List<int> answer = new List<int>();
    public bool[] answerBool;

    public Army(int[] soldiers, int n)
    {
        if (n < 1 || n > soldiers.Length)
            throw new ArgumentException("N is not valid");
        this.soldiers = soldiers;
        this.n = n;
        answerBool = new bool[soldiers.Length]; 
        Initiate();
    }

    public void Initiate()
    {
        int m = soldiers.Length;
        int current = -1;

        while (answer.Count < m)
        {
            current += n;
            current %= m;

            //Console.WriteLine($"current -> {soldiers[current]}");

            if (!answerBool[current])
            {
                answer.Add(soldiers[current]);
                answerBool[current] = true;
            }
                
            else
            {
                for (int i = 1; i < m; i++)
                {
                    if (!answerBool[(current + i) % m])
                    {
                        current = (current + i) % m;
                        answer.Add(soldiers[current]);
                        answerBool[current] = true;
                        break;
                    }
                }
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return answer.GetEnumerator();
    }
}