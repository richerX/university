using System;

namespace TaskJ_Eval_Professional
{
    class Program
    {
        public static void ColoredConsoleWrite(string expression, string firstPart, string secondPart, char operation, int answer)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{expression} = ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{firstPart} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{operation} ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{secondPart} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"= {answer}");
            Console.ResetColor();
        }

        public static int Evaluate(string expression)
        {
            if (expression.Length == 0)
                return 0;
            if (int.TryParse(expression, out int number))
                return number;
            string subString = expression.Substring(1, expression.Length - 2);
            if (expression.StartsWith("(") && expression.EndsWith(")") && isCorrectSequence(subString))
                return Evaluate(subString);

            int index = LastIndexOfOperation(expression);
            char operation = expression[index];
            string firstPart = expression.Substring(0, index);
            string secondPart = expression.Substring(index + 1);
            int answer = EvaluateTwoParts(firstPart, secondPart, operation);
            ColoredConsoleWrite(expression, firstPart, secondPart, operation, answer);
            return answer;
        }

        public static int EvaluateTwoParts(string first, string second, char operation)
        {
            if (operation == '+')
                return Evaluate(first) + Evaluate(second);
            if (operation == '-')
                return Evaluate(first) - Evaluate(second);
            if (operation == '*')
                return Evaluate(first) * Evaluate(second);
            if (operation == '/')
                return Evaluate(first) / Evaluate(second);
            throw new ArgumentOutOfRangeException("Incorrect operation");
        }

        public static bool isCorrectSequence(string expression)
        {
            int nesting = 0;
            foreach (var elem in expression)
            {
                nesting += NestUpdate(elem);
                if (nesting < 0)
                    return false;
            }
            return nesting == 0;
        }

        public static int LastIndexOfOperation(string expression)
        {
            int nesting = 0;
            for (int i = expression.Length - 1; i > -1; i--)
            {
                nesting += NestUpdate(expression[i]);
                if ((expression[i] == '+' || expression[i] == '-') && nesting == 0)
                    return i;
            }
            for (int i = expression.Length - 1; i > -1; i--)
            {
                nesting += NestUpdate(expression[i]);
                if ((expression[i] == '*' || expression[i] == '/') && nesting == 0)
                    return i;
            }
            return 0;
        }

        public static int NestUpdate(char bracket)
        {
            if (bracket == '(')
                return 1;
            if (bracket == ')')
                return -1;
            return 0;
        }

        static void Main(string[] args)
        {
            string expression = "(5*7)-3";
            Evaluate(expression);
            Console.ReadLine();
        }
    }
}
