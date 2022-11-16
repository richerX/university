using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


public class Message
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public Message()
    {

    }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="receiver"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    public Message(int id, User sender, User receiver, string subject, string message)
    {
        Id = id;
        Subject = subject;
        Text = message;

        SenderId = sender.Id;
        SenderName = sender.Name;
        SenderEmail = sender.Email;

        ReceiverId = receiver.Id;
        ReceiverName = receiver.Name;
        ReceiverEmail = receiver.Email;
    }

    /// <summary>
    /// Свойства класса.
    /// </summary>
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }

    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }

    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverEmail { get; set; }

    /// <summary>
    /// Override метода ToString.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => JsonSerializer.Serialize<Message>(this);
}
