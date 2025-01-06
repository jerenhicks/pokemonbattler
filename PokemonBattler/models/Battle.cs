using System;
using System.Collections.Generic;

public class Battle
{
    private Pokemon Pokemon1 { get; }
    private Pokemon Pokemon2 { get; }
    private int TurnLimit { get; }
    private List<string> battleLog = new List<string>();
    private DateTime startTime;
    private DateTime endTime;

    public Battle(Pokemon pokemon1, Pokemon pokemon2, int turnLimit = 100)
    {
        Pokemon1 = pokemon1;
        Pokemon2 = pokemon2;
        TurnLimit = turnLimit;
    }

    public void CommenceBattle()
    {
        startTime = DateTime.Now;
        var turn = 0;
        do
        {
            turn++;
            battleLog.Add($"Turn {turn}:");
            if (Pokemon1.CurrentSpeed >= Pokemon2.CurrentSpeed)
            {
                // Pokemon1 attacks first
            }
            else
            {
                // Pokemon2 attacks first
            }
            // Battle logic will be implemented here
            // For now, we will just display the status of both Pokemon
            Pokemon1.DisplayStatus();
            Pokemon2.DisplayStatus();

        } while (turn <= TurnLimit && Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0);

        endTime = DateTime.Now;
        var duration = (endTime - startTime).TotalMilliseconds;
        battleLog.Add($"Battle Duration: {duration} milliseconds");
    }

    public List<string> GetBattleLog()
    {
        return battleLog;
    }
}