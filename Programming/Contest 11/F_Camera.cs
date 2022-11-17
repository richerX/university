using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


[DataContract]
public class Camera
{
    [DataMember]
    public int id;
    private int maxSpeed;
    [DataMember]
    public List<Penalty> penalties = new List<Penalty>();

    public Camera(int id, int maxSpeed)
    {
        this.id = id;
        this.maxSpeed = maxSpeed;
    }

    public void AddPenalty(int carNumber, int speed)
    {
        if (speed > maxSpeed)
            penalties.Add(new Penalty(carNumber, (speed - maxSpeed) * 100));
    }
}