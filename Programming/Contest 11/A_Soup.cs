using System;
using System.Runtime.Serialization;


[DataContract]
public class Soup
{
    [DataMember]
    Ingredient[] ingredients;

    public Soup(Ingredient[] ingredients)
    {
        this.ingredients = ingredients;
    }

    public Soup()
    {
    }

    [DataMember]
    public bool WillEat => SecondWillEat();

    public bool SecondWillEat()
    {
        foreach (var ingr in ingredients)
        {
            if (ingr is Meat && !((Meat)ingr).IsTasty)
                return false;
            if (ingr is Vegetable && ((Vegetable)ingr).IsAllergicTo)
                return false;
        }
        return true;
    }
}