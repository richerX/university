using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    /*
     * Контейнер является хранилищем ящиков. 
     * Контейнер ограничивает максимальную суммарную массу хранимых ящиков, 
     * ограничение - случайное целое число в диапазоне [50, 1000].  
     */
    public class Container
    {
        // Ввод переменных.
        private double maxWeight;
        private double currentWeight;
        private List<Box> boxes = new List<Box>();

        // Конструктор класса.
        public Container(List<Box> boxes)
        {
            Random random = new Random();
            this.maxWeight = random.Next(50, 1001);
            currentWeight = 0;
            foreach (var box in boxes)
            {
                if (currentWeight + box.GetWeight() <= maxWeight)
                {
                    currentWeight += box.GetWeight();
                    this.boxes.Add(box);
                }
            }
        }

        // Get методы класса.
        public double GetMaxWeight() => maxWeight;
        public double GetCurrentWeight() => currentWeight;
        public List<Box> GetBoxes() => boxes;
        public double GetTotalCost()
        {
            double totalCost = 0;
            foreach (var box in this.boxes)
                totalCost += box.GetPrice();
            return totalCost;
        }

        // Переопределение строки объекта. 
        public override string ToString() => $"Контейнер | " +
            $"Максимальный вес - {maxWeight,0:f2} | Текущий вес - {currentWeight,0:f2} | Кол-во ящиков - {boxes.Count}";

        // Вывод информации об объекте.
        public void GetInfo()
        {
            Console.WriteLine(this);
            for (int i = 0; i < this.boxes.Count; i++)
            {
                Console.WriteLine($"         |");
                Console.WriteLine($"         |---------> №{i + 1} {boxes[i]}");
            }
            Console.WriteLine();
        }
    }
}
