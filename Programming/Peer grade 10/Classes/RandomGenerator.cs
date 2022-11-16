using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    /// <summary>
    /// Данный класс не относится к проверке работы
    /// и оставлен в ознакомительных целях.
    /// </summary>
    class RandomGenerator
    {
        private int amountProducts = 20;
        private int amountClients = 20;
        private int amountOrders = 50;

        private Random god = new Random();
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", 
            "k", "l", "m", "l", "n", "p", "q", "r", "s", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "y" };
        string[] cities = File.ReadAllLines("cities.txt");

        private List<Product> products = new List<Product>();
        private List<Client> clients = new List<Client>();
        private List<Order> orders = new List<Order>();

        public void Generation()
        {
            AddProducts();
            AddClients();
            AddOrders();

            Welcome.shop.products = this.products;
            Welcome.shop.clients = this.clients;
            Welcome.shop.orders = this.orders;
        }

        #region Основные функции

        private void AddProducts()
        {
            string name;
            int price;
            for (int i = 0; i < amountProducts; i++)
            {
                name = $"Product {i + 1}";
                price = god.Next(10, 5000);
                products.Add(new Product(i + 1, name, price));
            }
        }

        private void AddClients()
        {
            string fullName, phoneNumber, address, login, password;
            for (int i = 0; i < amountClients; i++)
            {
                fullName = $"{GenerateName()} {GenerateName()}";
                phoneNumber = $"{GeneratePhone()}";
                address = $"{GenerateAddress()}";
                login = $"{GenerateLogin()}@gmail.ru";
                password = $"{GenerateLogin()}";
                clients.Add(new Client(fullName, phoneNumber, address, login, password));
            }
            clients[0].login = "client@gmail.ru";
            clients[0].password = "password";
        }

        private void AddOrders()
        {
            List<(Product, int, int)> productList;
            Client client;
            Order order;
            DateTime time;
            for (int i = 0; i < amountOrders; i++)
            {
                productList = GenerateProductList();
                client = clients[god.Next(clients.Count)];
                time = GenerateTime();
                order = new Order(i + 1, productList, time, client);
                СonfigureStatus(ref order);
                orders.Add(order);
            }
        }

        #endregion

        #region Генераторы

        private string GenerateName(bool isShort = false)
        {
            string name = "";
            int length = god.Next(4, 8);
            if (isShort)
                length = god.Next(2, 5);
            name += consonants[god.Next(consonants.Length)].ToUpper();
            name += vowels[god.Next(vowels.Length)];
            while (name.Length < length)
            {
                name += consonants[god.Next(consonants.Length)];
                name += vowels[god.Next(vowels.Length)];
                if (god.Next(2) == 1)
                    name += consonants[god.Next(consonants.Length)];
                if (god.Next(2) == 1)
                    name += consonants[god.Next(consonants.Length)];
            }
            return name;
        }

        private string GeneratePhone()
        {
            // +xx (xxx) xxx-xx-xx
            int digit = god.Next(5, 100);
            int code = god.Next(100, 1000);
            int phone1 = god.Next(100, 1000);
            int phone2 = god.Next(10, 100);
            int phone3 = god.Next(10, 100);
            return $"+{digit} ({code}) {phone1}-{phone2}-{phone3}";
        }

        private string GenerateAddress()
        {
            // USA, New York, Main Street, 123
            string city = cities[god.Next(cities.Length)];
            string street = $"{GenerateName()} Street";
            string number = god.Next(1, 150).ToString();
            return $"{city}, {street}, {number}";
        }

        private string GenerateLogin()
        {
            string login = "";
            int length = god.Next(6, 15);
            while (login.Length < length)
            {
                login += consonants[god.Next(consonants.Length)];
                login += vowels[god.Next(vowels.Length)];
                if (god.Next(2) == 1)
                    login += consonants[god.Next(consonants.Length)];
                if (god.Next(2) == 1)
                    login += consonants[god.Next(consonants.Length)];
                if (god.Next(2) == 1)
                    login += vowels[god.Next(vowels.Length)];
            }
            return login;
        }

        private List<(Product, int, int)> GenerateProductList()
        {
            int amount = god.Next(1, 10);
            var answer = new List<(Product, int, int)>();
            for (int i = 0; i < amount; i++)
            {
                var currentProduct = products[god.Next(products.Count)];

                int index = -1;
                for (int j = 0; j < answer.Count; j++)
                {
                    if (answer[j].Item1.name == currentProduct.name)
                        index = j;
                }

                if (index == -1)
                    answer.Add((currentProduct, amount, currentProduct.price));
                else
                    answer[index] = (currentProduct, answer[index].Item2 + amount, currentProduct.price);
            }
            return answer;
        }

        private DateTime GenerateTime()
        {
            DateTime from = DateTime.Now.AddYears(-2);
            DateTime to = DateTime.Now;

            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(god.NextDouble() * range.Ticks));
            return from + randTimeSpan;
        }


        private void СonfigureStatus(ref Order order)
        {
            if (god.Next(1, 101) <= 80)
            {
                order.status |= Status.Processed;
                if (god.Next(1, 101) <= 60)
                    order.status |= Status.Paid;
                if (god.Next(1, 101) <= 60)
                    order.status |= Status.Shipped;
                if (order.status.HasFlag(Status.Paid) &&
                    order.status.HasFlag(Status.Shipped) &&
                    god.Next(1, 101) <= 90)
                    order.status |= Status.Done;
            }
        }

        #endregion
    }
}
