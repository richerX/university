using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


[DataContract]
public class TextTask : Task
{
    [XmlAttribute]
    public AnswerType type;
    [XmlAttribute]
    public string question;
    [XmlAttribute, DataMember]
    public string answer;

    public TextTask()
    {
        
    }

    [JsonIgnore]
    public AnswerType Type => type;

    [JsonIgnore]
    public string Question => question;

    [JsonIgnore]
    public string Answer
    {
        get { return answer; }
        set { answer = value; }
    }

    public enum AnswerType : int
    {
        Text = 0,
        Multiple = 1,
        Single = 2
    }

    public override string ToString()
    {
        return $"TextTask: {groupNumber} {hardLvl} {type} {question} {answer}";
    }
}