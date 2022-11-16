using System;

class Program
{
    private static void Main(string[] args)
    {
        Linguist linguist;
        Corrector corrector;

        try
        {
            linguist = Linguist.Parse(Console.ReadLine());
            corrector = Corrector.Parse(Console.ReadLine());
        }

        catch (ArgumentException argumentException)
        {
            Console.WriteLine(argumentException.Message);
            return;
        }

        string text = Console.ReadLine();
        int totalSalary = 0;

        string editedText = linguist.EditHeader(text);
        totalSalary += linguist.CountSalary(text, editedText);

        string newEditedText = corrector.EditHeader(editedText);
        totalSalary += corrector.CountSalary(editedText, newEditedText);

        Console.WriteLine(newEditedText);
        Console.WriteLine(totalSalary);
    }
}