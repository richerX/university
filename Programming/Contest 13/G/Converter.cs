using System;

public class Converter : IConverter<MessageNetwork, MessageDb>
{
    public MessageDb Convert(MessageNetwork obj)
    {
        long id = obj.Id;
        string content = DeleteSpaces(obj.Content);
        string imageurl = DeleteSpaces(obj.ImageNetwork.Url);
        return new MessageDb(id, content, imageurl);
    }

    public string DeleteSpaces(string text)
    {
        var array = text.Split();
        var answer = "";
        foreach (var elem in array)
            answer += elem;
        return answer;
    }
}