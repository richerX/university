using System.Collections.Generic;

public class Shop
{
    private readonly List<Product> products;

    public Product this[int index]
    {
        get
        {
            if (products[index] is Notebook)
                return (Notebook) products[index];
            return (Book) products[index];
        }
    }

    public Shop(List<Product> products)
    {
        this.products = products;
    }
}