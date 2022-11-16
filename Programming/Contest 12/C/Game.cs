using System;
using System.Collections;
using System.Collections.Generic;

public class Game : IEnumerable
{
    private readonly LinkedList<int> first;
    private readonly LinkedList<int> second;
    public List<string> answer = new List<string>();

    public Game(LinkedList<int> first, LinkedList<int> second)
    {
        this.first = first;
        this.second = second;
        InitiateGame();
    }
    
    public void InitiateGame()
    {
        int turn = 1;
        bool totalBreak = false;
        while (true)
        {
            if (turn == 1)
            {
                int count = first.Count;
                for (int i = 0; i < count; i++)
                {
                    if (first.First.Value >= second.First.Value)
                    {
                        answer.Add($"First: {first.First.Value}");
                        if (count == 1)
                        {
                            answer.Add("First win!");
                            totalBreak = true;
                        }
                        turn = 2;
                        first.RemoveFirst();
                        break;
                    }

                    else if (first.First.Value < second.First.Value)
                    {
                        first.AddLast(new LinkedListNode<int>(first.First.Value));
                        first.RemoveFirst();
                    }

                    if (i == count - 1)
                    {
                        answer.Add("Second win!");
                        totalBreak = true;
                        break;
                    }
                }
            }

            else if (turn == 2)
            {
                int count = second.Count;
                for (int i = 0; i < count; i++)
                {
                    if (second.First.Value >= first.First.Value)
                    {
                        answer.Add($"Second: {second.First.Value}");
                        if (count == 1)
                        {
                            answer.Add("Second win!");
                            totalBreak = true;
                        }
                        turn = 1;
                        second.RemoveFirst();
                        break;
                    }

                    else if (second.First.Value < first.First.Value)
                    {
                        second.AddLast(new LinkedListNode<int>(second.First.Value));
                        second.RemoveFirst();
                    }

                    if (i == count - 1)
                    {
                        answer.Add("First win!");
                        totalBreak = true;
                        break;
                    }
                }
            }

            if (totalBreak)
                break;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return answer.GetEnumerator();
    }
}