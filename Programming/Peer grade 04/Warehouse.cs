using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    /* Склад является хранилищем контейнеров. 
     * Содержимое склада может меняться - туда могут поступать новые контейнеры и удаляться старые. 
     * 1) Cклад может содержать ограниченное число контейнеров     (это число определяется пользователем).
     * 2) Склад взимает фиксированную плату за хранение контейнера (это число определяется пользователем).
     * 3) Если происходит добавление нового контейнера в заполненный склад, то он заменяет самый старый контейнер.
     */
    public class Warehouse
    {
        // Ввод переменных.
        private int capacity;
        private double pricePerContainer;
        private List<Container> containers = new List<Container>();

        // Конструктор класса.
        public Warehouse(int capacity, double pricePerContainer)
        {
            this.capacity = capacity;
            this.pricePerContainer = pricePerContainer;
        }

        // Get методы класса.
        public int GetCapacity() => capacity;
        public double GetPricePerContainer() => pricePerContainer;
        public List<Container> GetContainers() => containers;

        // Добавление контейнера.
        public bool AddContainer(Container container)
        {
            Random random = new Random();
            double damageDegree = random.NextDouble() / 2;
            // Проверка рентабельности хранения контейнера.
            if (container.GetTotalCost() * damageDegree > pricePerContainer)
            {
                if (containers.Count == capacity)
                    containers.RemoveAt(0);
                containers.Add(container);
                return true;
            }
            return false;
        }

        // Удаление контейнера.
        public void DeleteContainer(int index)
        {
            this.containers.RemoveAt(index);
        }

        // Переопределение строки объекта. 
        public override string ToString() => $"Cклад | Вместимость - {capacity} | " +
            $"Стоимость хранения одного контейнера - {pricePerContainer,0:f2} | Кол-во контейнеров - {containers.Count}";

        // Вывод информации об объекте.
        public void GetInfo()
        {
            Console.WriteLine(this);
            for (int i = 0; i < this.containers.Count; i++)
            {
                Console.WriteLine($"      |");
                Console.WriteLine($"      |---------> №{i + 1} {this.containers[i]}");
            }
            Console.WriteLine();
        }
    }
}
