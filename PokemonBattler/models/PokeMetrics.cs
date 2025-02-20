using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class PokeMetrics
{
    public Dictionary<BattleTeam, Metric> Metrics { get; set; }

    public PokeMetrics()
    {
        Metrics = new Dictionary<BattleTeam, Metric>();
    }

    public void AddMetrics(Battle battle)
    {
        //check to see if the pokemon in the battle object are already in the dictionary.
        //if they are not, create a new metric for them
        //if they are, update the metric for them
        if (!Metrics.Keys.Contains(battle.Team1))
        {
            CreateNewMetric(battle.Team1);
        }
        if (!Metrics.Keys.Contains(battle.Team2))
        {
            CreateNewMetric(battle.Team2);
        }

        //FIXME: OLD CODE DID THIS, BUT DO WE?
        if (Metrics[battle.Team1].WinsAgainst.Contains(battle.Team2) || Metrics[battle.Team1].LossesAgainst.Contains(battle.Team2) || Metrics[battle.Team1].TiesAgainst.Contains(battle.Team2))
        {
            //we've already logged this battle, move on.
            return;
        }

        var teamOneKnockedOut = battle.Team1.KnockedOut();
        var teamTwoKnockedOut = battle.Team2.KnockedOut();
        if (teamOneKnockedOut && teamTwoKnockedOut)
        {
            Metrics[battle.Team1].TiesAgainst.Add(battle.Team2);
            Metrics[battle.Team2].TiesAgainst.Add(battle.Team1);
        }
        else if (teamOneKnockedOut)
        {
            Metrics[battle.Team1].LossesAgainst.Add(battle.Team2);
            Metrics[battle.Team2].WinsAgainst.Add(battle.Team1);
        }
        else
        {
            Metrics[battle.Team1].WinsAgainst.Add(battle.Team2);
            Metrics[battle.Team2].LossesAgainst.Add(battle.Team1);
        }
    }

    public void CreateNewMetric(BattleTeam team)
    {
        Metrics.Add(team, new Metric { Team = team, WinsAgainst = new List<BattleTeam>(), LossesAgainst = new List<BattleTeam>(), TiesAgainst = new List<BattleTeam>() });
    }

    public void OutputResultsToConsole()
    {
        foreach (var metric in Metrics)
        {
            Console.WriteLine($"{metric.Key} has {metric.Value.Wins} wins, {metric.Value.Losses} losses, and {metric.Value.Ties} ties.");
        }
    }

    public void OutputResultsToFile(String path)
    {
        var sortedMetrics = Metrics.OrderByDescending(m => m.Value.Wins)
                                   .ThenByDescending(m => m.Value.Ties)
                                   .ToList();

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
        {
            foreach (var metric in sortedMetrics)
            {
                file.WriteLine($"{metric.Key} has {metric.Value.Wins} wins, {metric.Value.Losses} losses, and {metric.Value.Ties} ties.");
            }
        }
    }

}
