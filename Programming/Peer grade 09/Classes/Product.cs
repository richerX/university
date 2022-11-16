using System;
using System.Collections.Generic;
using System.Text;

namespace Slider_App
{
    public class Product
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        public string name;
        public string article;
        public int amount;
        public int price;

        /// <summary>
        /// Конструктор по умочланию.
        /// </summary>
        public Product()
        {
            name = "";
            article = "";
            amount = 0;
            price = 0;
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="article"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public Product(string name, string article, int amount, int price)
        {
            this.name = name;
            this.article = article;
            this.amount = amount;
            this.price = price;
        }

        /// <summary>
        /// Упаковка полей в массив.
        /// </summary>
        /// <returns></returns>
        public object[] GetValues()
        {
            return new object[] { name, article, amount, price };
        }
    }
}
