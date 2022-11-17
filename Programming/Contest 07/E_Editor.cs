using System;

abstract class Editor
{
    public string name;
    int salary;

    protected Editor(string name, int salary)
    {
        this.name = name;
        this.salary = salary;
    }

    protected string EditHeader(string header)
    {
        return header + " " + name;
    }

    public int CountSalary(string oldStr, string newStr)
    {
        return Math.Abs(newStr.Length - oldStr.Length) * salary;
    }
}