using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

partial class Program
{
    private static bool ValidateQuery(string query, out string[] queryParameters)
    {
        string[] parameters = query.Split(' ');
        parameters[0] = parameters[0].ToLower();
        queryParameters = parameters;
        if (parameters.Length != 3)
        {
            return false;
        }
        switch (parameters[0])
        {
            case "first_name":
            case "last_name":
            case "group":
                if (parameters[1] != "==" && parameters[1] != "<>")
                {
                    return false;
                }
                break;
            case "rating":
            case "gpa":
                if (parameters[1] != ">=" && parameters[1] != "<=")
                {
                    return false;
                }
                if (parameters[0] == "rating")
                {
                    if (!int.TryParse(parameters[2], out int result))
                    {
                        return false;
                    }
                }
                if (parameters[0] == "gpa")
                {
                    if (!double.TryParse(parameters[2], out double result))
                    {
                        return false;
                    }
                }
                break;
            default:
                return false;
        }
        return true;
    }

    private static List<string> ProcessQuery(string[] queryParameters, string pathToDatabase)
    {
        List<string> answer = new List<string>();
        string[] allStrings = File.ReadAllLines(pathToDatabase, Encoding.UTF8);
        for (int i = 1; i < allStrings.Length; i++)
        {
            if (StringValidate(allStrings[i].Split(';'), queryParameters))
            {
                answer.Add(allStrings[i]);
            }
        }
        return answer;
    }

    public static bool StringValidate(string[] currentString, string[] queryParameters)
    {
        // currentString    =  [Kelis, Mckenna, BSE209, 37, 3.53]
        // queryParameters  =  [last_name, ==, Mckenna]
        string column = queryParameters[0];
        string operation = queryParameters[1];
        string value = queryParameters[2];

        int columnIndex = 0;
        switch (column)
        {
            case "first_name":
                columnIndex = 0;
                break;
            case "last_name":
                columnIndex = 1;
                break;
            case "group":
                columnIndex = 2;
                break;
            case "rating":
                columnIndex = 3;
                break;
            case "gpa":
                columnIndex = 4;
                break;
        }

        if (operation == "==")
        {
            return currentString[columnIndex].ToLower() == value.ToLower();
        }

        if (operation == "<>")
        {
            return currentString[columnIndex].ToLower() != value.ToLower();
        }

        if (operation == ">=")
        {
            return double.Parse(currentString[columnIndex]) >= double.Parse(value);
        }

        if (operation == "<=")
        {
            return double.Parse(currentString[columnIndex]) <= double.Parse(value);
        }

        return false;
    }

}