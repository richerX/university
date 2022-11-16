using System;

public class Pupil
{
    public static int count = 1;
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }
    public int Year { get; }
    public string SchoolNumber { get; set; }

    public Pupil(string firstName, string middleName, string lastName, int year, string schoolNumber)
    {
        this.Id = count;
        count += 1;
        this.FirstName = firstName;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.Year = year;
        this.SchoolNumber = schoolNumber;
    }
}