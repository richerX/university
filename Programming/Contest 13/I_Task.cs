using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;


[XmlInclude(typeof(TextTask))]
[XmlInclude(typeof(ProgramingTask))]
[DataContract]
[KnownType(typeof(TextTask))]
[KnownType(typeof(ProgramingTask))]
public class Task
{
    [XmlAttribute]
    public int groupNumber;
    [XmlAttribute, DataMember]
    public int hardLvl;
    [XmlAttribute]
    public string creator;

    public Task()
    {

    }

    [JsonIgnore]
    public int GroupNumber => groupNumber;

    [JsonIgnore]
    public int HardLvl
    {
        get { return hardLvl; }
        set { hardLvl = value; }
    }

    [JsonIgnore]
    public string Creator => creator;

    public static Task[] DeserializeFromXml(string xmlFile)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Task[]));
        using (FileStream stream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
            return (Task[])serializer.Deserialize(stream);
    }

    public static void SerializeToJson(string fileName, Task[] tasks)
    {
        var formatter = new DataContractJsonSerializer(typeof(Task[]));
        using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            formatter.WriteObject(stream, tasks);
    }

    public static void FindAnswers(Task[] tasks)
    {
        //var generator = new AnswerGenerator();
        foreach (var task in tasks)
            AnswerGenerator.GenerateAnswer(task);
    }
}