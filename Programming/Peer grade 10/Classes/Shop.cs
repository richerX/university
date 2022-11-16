using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class Shop
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public List<Client> clients;
        public List<Order> orders;
        public List<Product> products;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Shop() { }
    }
}
