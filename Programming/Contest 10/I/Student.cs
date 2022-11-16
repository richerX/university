public class Student
{
    public string Name;
    public double Temperature;
    public bool IsMale;

    public override string ToString()
    {
        return $"STUDENT {Name} {Temperature} {IsMale}";
    }
}