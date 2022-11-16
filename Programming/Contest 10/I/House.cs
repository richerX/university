public class House
{
    public string Street;
    public string HouseNumber;
    public Student[] Students;

    public override string ToString()
    {
        string answer = $"HOUSE {Street} {HouseNumber} \n";
        foreach (var student in Students)
        {
            answer += $"| \n";
            answer += $"|--------> {student} \n";
        }
        return answer;
    }
}