using System;

public class KafkaException : Exception
{
    private string message;

    public KafkaException(string message)
    {
        this.message = message;
    }

    public override string Message
    {
        get { return message; }
    }
}