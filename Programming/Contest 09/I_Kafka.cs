using System;
using System.Collections.Generic;

public class Kafka
{
    private MessageQueue queue;
    private HashSet<User> users = new HashSet<User>();
    private bool isActive = false;

    public Kafka(int queueSize)
    {
        queue = new MessageQueue(queueSize);
    }

    public void Subscribe(User user)
    {
        if (!isActive)
            throw new KafkaException("Kafka is not active");
        if (users.Contains(user))
            throw new KafkaException("User is already subscribed");
        users.Add(user);
    }

    public void Unsubscribe(User user)
    {
        if (!isActive)
            throw new KafkaException("Kafka is not active");
        if (!users.Contains(user))
            throw new KafkaException("User is already unsubscribed");
        users.Remove(user);
    }

    public void Push(Message message)
    {
        if (!isActive)
            throw new KafkaException("Kafka is not active");
        queue.Push(message);
    }

    public List<Message> PopMessages(User user, int count)
    {
        if (!isActive)
            throw new KafkaException("Kafka is not active");
        if (user.Index + count > queue.Size)
            throw new KafkaException("Not enough messages");
        if (!users.Contains(user))
            throw new KafkaException("User is not subscribed");
        user.IncreaseIndex(count);
        return queue[user.Index - count, user.Index];
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive)
            throw new KafkaException("Kafka is not active");
        isActive = false;
    }
}