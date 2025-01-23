using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class PokeMetrics
{
    public Dictionary<string, Metric> Metrics { get; set; }

    public PokeMetrics()
    {
        Metrics = new Dictionary<string, Metric>();
    }

    public void AddMetrics(Battle battle)
    {
        if (battle.Pokemon1.CurrentHP == 0 && battle.Pokemon2.CurrentHP == 0)
        {
            if (!Metrics.ContainsKey(battle.Pokemon1.Name))
            {
                CreateNewMetric(battle.Pokemon1);
            }
            if (!Metrics.ContainsKey(battle.Pokemon2.Name))
            {
                CreateNewMetric(battle.Pokemon2);
            }

            if (!Metrics[battle.Pokemon1.Name].TiesAgainst.Contains(battle.Pokemon2))
            {
                Metrics[battle.Pokemon1.Name].TiesAgainst.Add(battle.Pokemon2);
            }
            if (!Metrics[battle.Pokemon2.Name].TiesAgainst.Contains(battle.Pokemon1))
            {
                Metrics[battle.Pokemon2.Name].TiesAgainst.Add(battle.Pokemon1);
            }
        }
        else
        {
            if (battle.Pokemon1.CurrentHP == 0)
            {
                if (!Metrics.ContainsKey(battle.Pokemon1.Name))
                {
                    CreateNewMetric(battle.Pokemon1);
                }
                if (!Metrics.ContainsKey(battle.Pokemon2.Name))
                {
                    CreateNewMetric(battle.Pokemon2);
                }
                if (!Metrics[battle.Pokemon1.Name].LossesAgainst.Contains(battle.Pokemon2))
                {
                    Metrics[battle.Pokemon1.Name].LossesAgainst.Add(battle.Pokemon2);
                }
                if (!Metrics[battle.Pokemon2.Name].WinsAgainst.Contains(battle.Pokemon1))
                {
                    Metrics[battle.Pokemon2.Name].WinsAgainst.Add(battle.Pokemon1);
                }
            }
            else
            {
                if (!Metrics.ContainsKey(battle.Pokemon1.Name))
                {
                    CreateNewMetric(battle.Pokemon1);
                }
                if (!Metrics.ContainsKey(battle.Pokemon2.Name))
                {
                    CreateNewMetric(battle.Pokemon2);
                }
                if (!Metrics[battle.Pokemon1.Name].WinsAgainst.Contains(battle.Pokemon2))
                {
                    Metrics[battle.Pokemon1.Name].WinsAgainst.Add(battle.Pokemon2);
                }
                if (!Metrics[battle.Pokemon2.Name].LossesAgainst.Contains(battle.Pokemon1))
                {
                    Metrics[battle.Pokemon2.Name].LossesAgainst.Add(battle.Pokemon1);
                }
            }
        }
    }

    public void CreateNewMetric(Pokemon pokemon)
    {
        Metrics.Add(pokemon.Name, new Metric { Pokemon = pokemon, WinsAgainst = new List<Pokemon>(), LossesAgainst = new List<Pokemon>(), TiesAgainst = new List<Pokemon>() });
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
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
        {
            foreach (var metric in Metrics)
            {
                file.WriteLine($"{metric.Key} has {metric.Value.Wins} wins, {metric.Value.Losses} losses, and {metric.Value.Ties} ties.");
            }
        }
    }

}
