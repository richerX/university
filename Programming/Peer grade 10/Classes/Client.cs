using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class Client
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public string fullName;
        public string phoneNumber;
        public string address;
        public string login;
        public string password;

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        public Client() { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public Client(string fullName, string phoneNumber, string address, string login, string password)
        {
            this.fullName = fullName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.login = login;
            this.password = password;
        }

        /// <summary>
        /// Упаковка значений для DataGrid
        /// </summary>
        /// <returns></returns>
        public object[] GetValues()
        {
            return new object[] { fullName, phoneNumber, address, login, password };
        }
    }
}
