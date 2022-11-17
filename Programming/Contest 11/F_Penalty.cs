using System;
using System.Runtime.Serialization;


[DataContract]
public class Penalty
{
    [DataMember]
    public int car_number;
    [DataMember]
    public int cost;

    public Penalty(int carNumber, int cost)
    {
        this.car_number = carNumber;
        this.cost = cost;
    }
}