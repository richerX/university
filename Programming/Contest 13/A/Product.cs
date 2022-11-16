using System;

public abstract partial class Product
{
    public long id;

    protected Product(long id)
    {
        this.id = id;
    }

    public override string ToString()
    {
        return $"PRODUCT ToString() бшонкмхкняэ - рюй ме мюдн";
    }
}