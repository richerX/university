using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Slider_App
{
    public class Category
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        public int id;
        public string name;
        public int parentId;
        public List<Category> children;
        public List<Product> products;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Category()
        {
            id = 0;
            name = "";
            parentId = 0;
            children = new List<Category>();
            products = new List<Product>();
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        public Category(int id, string name, int parentId = 0)
        {
            this.id = id;
            this.name = name;
            this.parentId = parentId;
            children = new List<Category>();
            products = new List<Product>();
        }

        /// <summary>
        /// Переопределение ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Категория {name}";
        }
    }
}
