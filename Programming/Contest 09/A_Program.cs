using System;

class Program
{
    public static void Main(string[] args)
    {
        int teamLeadsAmount = int.Parse(Console.ReadLine());
        int[] programmersAmount = Array.ConvertAll(
            Console.ReadLine().Split(' '), int.Parse);

        var company = new Company(teamLeadsAmount, programmersAmount);
        
        company.PrintTeams();
        
        foreach (var teamLead in company.TeamLeads)
        {
            teamLead.HuntProgrammers(company.TeamLeads);
        }

        company.PrintTeams();

        foreach (var teamLead in company.TeamLeads)
        {
            Console.WriteLine($"Team lead #{teamLead.id}");
            foreach (var programmer in teamLead.programmers)
            {
                Console.WriteLine($"Programmer {programmer.linesOfCode} {programmer.linesOfCode % 2 == 0} {programmer.linesOfCode % 3 == 0} {programmer.linesOfCode % 4 == 0}");
            }
            Console.WriteLine();
        }
    }
}