using System;
using System.Collections.Generic;

public class TeamLead : Programmer
{
    public List<Programmer> programmers;

    public TeamLead(int id, List<Programmer> programmers) : base(id)
    {
        this.id = id;
        this.programmers = programmers;
    }

    public void HuntProgrammers(List<TeamLead> teamLeads)
    {
        foreach (var teamLead in teamLeads)
        {
            var curList = new List<Programmer>();
            foreach (var programmer in teamLead.programmers)
            {
                if (programmer.linesOfCode % (this.id + 1) == 0)
                    curList.Add(programmer);
            }

            foreach (var programmer in curList)
            {
                teamLead.programmers.Remove(programmer);
                this.programmers.Add(programmer);
            }
        }
    }

    public override string ToString()
    {
        return String.Format("Team lead #{0}\nAmount of programmers in team: {1}", id, programmers.Count);
    }
}