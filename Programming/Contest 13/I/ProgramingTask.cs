using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


[DataContract]
public class ProgramingTask : Task
{
    [XmlAttribute]
    public string language;
    [XmlAttribute]
    public int maxLinesOfCode;
    [XmlAttribute, DataMember]
    public string answer;

    public ProgramingTask()
    {
        
    }

    [JsonIgnore]
    public string Language => language;

    [JsonIgnore]
    public int MaxLinesOfCode => maxLinesOfCode;

    [JsonIgnore]
    public string Answer
    {
        get { return answer; }
        set { answer = value; }
    }

    public override string ToString()
    {
        return $"ProgramingTask: {groupNumber} {hardLvl} {language} {maxLinesOfCode} {answer}";
    }
}