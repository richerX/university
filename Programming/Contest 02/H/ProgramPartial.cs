using System;

partial class Program
{
    // В случае, если введённый день недели не соответствует формату входных данных
    // метод должен вернуть int.MinValue.
    // Гарантируется, что int.MinValue не может быть получен как верный ответ.
    static int MorningWorkout(String dayOfWeek, int firstNumber, int secondNumber)
    {
        switch (dayOfWeek)
        {
            case "Monday":
            case "Wednesday":
            case "Friday":
                return GetSumOfOddOrEvenDigits(firstNumber, 1);
            case "Tuesday":
            case "Thursday":
                return GetSumOfOddOrEvenDigits(secondNumber, 0);
            case "Saturday":
                return Maximum(firstNumber, secondNumber);
            case "Sunday":
                return Multiply(firstNumber, secondNumber);
            default:
                return int.MinValue;
        }
    }

    static int GetSumOfOddOrEvenDigits(int value, int remainder)
    {
        int answer = 0;
        int digit;
        value = Math.Abs(value);
        while (value > 0)
        {
            digit = value % 10;
            if (digit % 2 == remainder)
            {
                answer += digit;
            }
            value /= 10;
        }
        return answer;
    }

    static int Multiply(int firstValue, int secondValue)
    {
        return firstValue * secondValue;
    }

    static int Maximum(int firstValue, int secondValue)
    {
        return Math.Max(firstValue, secondValue);
    }
}