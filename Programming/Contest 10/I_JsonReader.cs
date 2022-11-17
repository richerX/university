using System;
using System.Collections.Generic;
using System.IO;

public class JsonReader
{
    string streetName;

    public JsonReader(string path)
    {
        using (StreamReader reader = new StreamReader(File.Open("input.txt", FileMode.Open, FileAccess.Read)))
        {
            List<string> streets = OuterParse(reader.ReadLine());
            

            List<House> houses = new List<House>();
            foreach (var street in streets)
            {
                House tikTok = new House();
                var parsedStreet = DoubleDelimParse(street);
                tikTok.Street = parsedStreet[0].Split()[1].Trim('"');
                tikTok.HouseNumber = parsedStreet[1].Split()[1];
                List<Student> students = new List<Student>();
                foreach (var student in OuterParse(parsedStreet[2]))
                {
                    Student newStudent = new Student();
                    var parsedStudent = DoubleDelimParse(student);
                    newStudent.Name = parsedStudent[0].Split()[1].Trim('"');
                    newStudent.Temperature = double.Parse(parsedStudent[1].Split()[1].Trim().Replace('.', ','));
                    //newStudent.Temperature = double.Parse(parsedStudent[1].Split()[1].Trim());
                    newStudent.IsMale = parsedStudent[2].Split()[1].Trim() == "true";
                    students.Add(newStudent);
                }
                tikTok.Students = students.ToArray();
                houses.Add(tikTok);
            }

            double maxTemperature = double.Parse(reader.ReadLine().Replace('.', ','));
            //double maxTemperature = double.Parse(reader.ReadLine());
            Dictionary<string, int> patients = new Dictionary<string, int>();
            foreach (House tikTok in houses)
            {
                if (!patients.ContainsKey(tikTok.Street))
                    patients[tikTok.Street] = 0;
                patients[tikTok.Street] += GetNumberOfPatients(tikTok, maxTemperature);
            }

            int maximum = int.MinValue;
            foreach (var pair in patients)
            {
                if (pair.Value > maximum)
                {
                    maximum = pair.Value;
                    streetName = pair.Key;
                }
            }

            /*
            foreach (var house in houses)
                Console.WriteLine(house);
            */
        }
    }
    
    public string TheSickestStreet
    {
        get
        {
            return streetName;
        }
    }

    public List<string> OuterParse(string str)
    {
        List<int> indexes = new List<int>();
        int enclosure = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '{')
            {
                if (enclosure == 0)
                    indexes.Add(i);
                enclosure += 1;
            }
            else if (str[i] == '}')
            {
                enclosure -= 1;
                if (enclosure == 0)
                    indexes.Add(i);
            }
        }

        List<string> answer = new List<string>();
        for (int i = 0; i < indexes.Count; i += 2)
            answer.Add(str.Substring(indexes[i] + 1, indexes[i + 1] - indexes[i]).Trim());
        return answer;
    }

    public List<string> DoubleDelimParse(string str)
    {
        List<int> indexes = new List<int>();
        int found = 0;

        indexes.Add(0);
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == ',')
            {
                indexes.Add(i);
                found += 1;
            }
            if (found == 2)
                break;
        }
        indexes.Add(str.Length - 1);

        List<string> answer = new List<string>();
        for (int i = 0; i < indexes.Count - 1; i += 1)
            answer.Add(str.Substring(indexes[i] + 1, indexes[i + 1] - indexes[i] - 1).Trim());
        return answer;
    }

    public void PrintList(List<string> array)
    {
        Console.WriteLine("--------- ARRAY -----------");
        foreach (var elem in array)
            Console.WriteLine($"ELEMENT - {elem}" + Environment.NewLine);
        Console.WriteLine();
    }

    public int GetNumberOfPatients(House tikTok, double maxTemperature)
    {
        int answer = 0;
        foreach (var student in tikTok.Students)
        {
            if (student.Temperature > maxTemperature && student.IsMale)
            {
                answer += 1;
            }
                
        }
        return answer;
    }
}