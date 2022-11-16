using System;
using System.Collections.Generic;

public static class Methods
{
    public static int FindBestBiathlonistValue(List<Sportsman> sportsmen)
    {
        int best = int.MinValue;
        int shot, run;
        int current;
        foreach (var man in sportsmen)
        {
            var qualities = getQualities(man);
            run = qualities.Item1;
            shot = qualities.Item2;
            current = (int)(0.4 * Math.Max(shot, run) + 0.6 * Math.Min(shot, run));
            if (current > best)
                best = current;
        }
        return best;
    }

    public static int FindBestShooterValue(List<Sportsman> sportsmen)
    {
        int best = int.MinValue;
        foreach (var man in sportsmen)
        {
            var qualities = getQualities(man);
            if (qualities.Item1 > best)
                best = qualities.Item1;
        }
        return best;
    }

    public static int FindBestRunnerValue(List<Sportsman> sportsmen)
    {
        int best = int.MinValue;
        foreach (var man in sportsmen)
        {
            var qualities = getQualities(man);
            if (qualities.Item2 > best)
                best = qualities.Item2;
        }
        return best;
    }

    public static Tuple<int, int> getQualities(Sportsman man)
    {
        if (man.GetType() == typeof(SkiRunner))
            return Tuple.Create(0, ((ISkiRunner)man).Run());
        if (man.GetType() == typeof(Shooter))
            return Tuple.Create(((IShooter)man).Shoot(), 0);
        return Tuple.Create(((IShooter)man).Shoot(), ((ISkiRunner)man).Run());
    }
}