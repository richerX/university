using System;
using System.Collections.Generic;

public class Company
{
    public List<TeamLead> TeamLeads;

    public Company(int teamLeadsAmount, int[] programmersAmount)
    {
        TeamLeads = new List<TeamLead>();
        for (int i = 0; i < teamLeadsAmount; i++)
        {
            List<Programmer> programmers = new List<Programmer>();
            for (int j = 0; j < programmersAmount[i]; j++)
            {
                int newId = (i + 1) * (int) Math.Pow(10, (j + 1) / 10 + 1) + (j + 1);
                var newProgrammer = new Programmer(newId);
                newProgrammer.adminId = i + 1;
                programmers.Add(newProgrammer);
            }
            TeamLeads.Add(new TeamLead(i + 1, programmers));
        }
    }

    public void PrintTeams()
    {
        foreach (var teamLead in TeamLeads)
        {
            Console.WriteLine($"Team lead #{teamLead.id}");
            Console.WriteLine($"Amount of programmers in team: {teamLead.programmers.Count}");
            int lines = teamLead.linesOfCode;
            foreach (var programmer in teamLead.programmers)
                lines += programmer.linesOfCode;
            Console.WriteLine($"Written lines of code: {lines}");
        }
        Console.WriteLine();
    }
}