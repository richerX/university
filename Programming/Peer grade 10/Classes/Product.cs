using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class Product
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public string name;
        public int id;
        public int price;

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        public Product() { }

        /// <summary>
        /// Констурктор класса
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Product(string name, int price)
        {
            this.name = name;
            this.id = GetNewId();
            this.price = price;
        }

        /// <summary>
        /// И еще констурктор класса, больше - не меньше
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Product(int id, string name, int price)
        {
            this.name = name;
            this.id = id;
            this.price = price;
        }

        /// <summary>
        /// Упаковка значений для DataGrid
        /// </summary>
        /// <returns></returns>
        public object[] GetValues()
        {
            return new object[] { id, name, price };
        }

        /// <summary>
        /// Создание нового ID
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
            int maximum = 0;
            foreach (var elem in Welcome.shop.products)
            {
                if (elem.id > maximum)
                    maximum = elem.id;
            }
            return maximum + 1;
        }
    }
}
