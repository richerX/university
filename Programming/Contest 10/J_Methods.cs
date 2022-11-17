using System;
using System.Collections.Generic;
using System.Xml;

public class Methods
{
    public static XmlDocument GetDocument(string company, List<string> persons)
    {
        List<Person> employee = new List<Person>();
        foreach (var elem in persons)
        {
            var array = elem.Split(',');
            var newEmployee = new Person(array[0], array[3], array[2]);
            if (array[0] != array[1])
            {

            }
            employee.Add(new )
        }
        return new XmlDocument();
    }
}

public class Person
{
    public int id;
    public string name;
    public string position;
    public List<int> subs = new List<int>();

    public Person(string id, string name, string position)
    {
        this.id = int.Parse(id);
        this.name = name;
        this.position = position;
    }
}