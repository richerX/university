using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    /*
     * Ящик овощей
     * Ящик овощей описывается двумя параметрами - массой в килограммах и ценой за килограмм.
     * Оба параметра указываются при создании, при этом масса не может быть изменена позднее.
     */
    public class Box
    {
        // Ввод переменных.
        private double weight;
        private double pricePerKilo;

        // Конструктор класса.
        public Box(double weight, double pricePerKilo)
        {
            this.weight = weight;
            this.pricePerKilo = pricePerKilo;
        }

        // Get методы класса.
        public double GetWeight() => weight;
        public double GetPricePerKilo() => pricePerKilo;
        public double GetPrice() => weight * pricePerKilo;

        // Переопределение строки объекта.
        public override string ToString() => $"Ящик | " +
            $"Вес - {weight, 0:f2} | Цена за килограмм - {pricePerKilo,0:f2} | Стоимость - {GetPrice(),0:f2}";
    }
}
