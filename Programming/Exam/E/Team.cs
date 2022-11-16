using System;
using System.Collections.Generic;
using System.Linq;

public class Team : IComparable
{
    private List<IPlayer> Players { get; }
    public double Skill => Players.Sum(x => x.Skill);

    public Team()
    {
        Players = new List<IPlayer>();
    }

    public static Team operator +(Team team, Player player)
    {
        team.Players.Add(player);
        return team;
    }

    public static bool operator >(Team team1, Team team2)
    {
        return team1.CompareTo(team2) > 0;
    }

    public static bool operator <(Team team1, Team team2)
    {
        return team1.CompareTo(team2) < 0;
    }

    public int CompareTo(object obj)
    {
        // this - team1
        var team2 = (Team)obj;
        if (this.Skill > team2.Skill)
            return 1;
        if (this.Skill == team2.Skill)
            return 0;
        return -1;
    }
}