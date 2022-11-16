using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    /// <summary>
    /// Флаговое перечесление
    /// </summary>
    [Flags]
    public enum Status
    {
        New = 0,
        Processed = 1,
        Paid = 2,
        Shipped = 4,
        Done = 8
    }

    public class Order
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public int id;
        // Product, price, amount
        public List<(Product, int, int)> products;
        public DateTime time;
        public Status status;
        public Client client;

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        public Order() { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="products"></param>
        /// <param name="client"></param>
        public Order(List<(Product, int, int)> products, Client client)
        {
            this.id = GetNewId();
            this.products = products;
            this.time = DateTime.Now;
            this.client = client;
        }

        /// <summary>
        /// Снова конструктор класса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <param name="time"></param>
        /// <param name="client"></param>
        public Order(int id, List<(Product, int, int)> products, DateTime time, Client client)
        {
            this.id = id;
            this.products = products;
            this.time = time;
            this.client = client;
        }

        /// <summary>
        /// Упаковка значений для DataGrid
        /// </summary>
        /// <returns></returns>
        public object[] GetValues()
        {
            string productsString = "";
            foreach (var product in products)
                productsString += product.Item1.name + ", ";
            return new object[] { id, productsString.Substring(0, productsString.Length - 2), time, status, client.fullName };
        }

        /// <summary>
        /// Упаковка номер 2 значений для DataGrid
        /// </summary>
        /// <returns></returns>
        public object[] GetValuesWithoutClient()
        {
            string productsString = "";
            foreach (var product in products)
                productsString += product.Item1.name + ", ";
            return new object[] { id, productsString.Substring(0, productsString.Length - 2), time, status, GetTotalSum() };
        }

        /// <summary>
        /// Общая сумма всего заказа
        /// </summary>
        /// <returns></returns>
        public int GetTotalSum()
        {
            int totalSum = 0;
            foreach (var product in products)
            {
                totalSum += product.Item2 * product.Item3;
            }
            return totalSum;
        }

        /// <summary>
        /// Создается новый ID
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
            int maximum = 0;
            foreach (var elem in Welcome.shop.orders)
            {
                if (elem.id > maximum)
                    maximum = elem.id;
            }
            return maximum + 1;
        }
    }
}
