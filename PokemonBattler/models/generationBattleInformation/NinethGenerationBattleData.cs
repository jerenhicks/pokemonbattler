using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class NinethGenerationBattleData : GenerationBattleData
{
    public NinethGenerationBattleData()
    {
    }

    public override bool CanHit(Pokemon attacker, Pokemon defender, Move move, Random random)
    {
        //these are always hit moves.
        if (move.Accuracy == null)
        {
            return true;
        }
        //check if the move hits
        //for now, we will just return true
        var accuracyModified = move.Accuracy * GetAccuracyModifiers(attacker, defender, move) * GetAdjustedStages(attacker, defender, move) * GetMiracleBerry(attacker, defender, move) - GetAffection(attacker, defender, move);
        //FIXME: PROBABLY NOT RIGHT HERE, but maybe????
        var accuracy = accuracyModified * 100;
        //need a number between 1 and 100
        int randomNumber = random.Next(1, 101);

        if (randomNumber <= accuracy)
        {
            return true;
        }
        return false;

    }

    public int GetAccuracyModifiers(Pokemon attacker, Pokemon defender, Move move)
    {
        var startValue = 4096;
        //likely some things will change this.
        var finalValue = startValue / 4096;
        return finalValue;
    }

    public decimal GetAdjustedStages(Pokemon attack, Pokemon defender, Move move)
    {
        //take the stage of the attacker and the stage of the defender and adjust the accuracy accordingly
        var adjustedStage = attack.StatModifiers.AccuracyStage - defender.StatModifiers.EvasionStage;

        if (adjustedStage <= -6)
        {
            return (decimal)3 / 9;
        }
        else if (adjustedStage == -5)
        {
            return (decimal)3 / 8;
        }
        else if (adjustedStage == -4)
        {
            return (decimal)3 / 7;
        }
        else if (adjustedStage == -3)
        {
            return (decimal)3 / 6;
        }
        else if (adjustedStage == -2)
        {
            return (decimal)3 / 5;
        }
        else if (adjustedStage == -1)
        {
            return (decimal)3 / 4;
        }
        else if (adjustedStage == 1)
        {
            return (decimal)4 / 3;
        }
        else if (adjustedStage == 2)
        {
            return (decimal)5 / 3;
        }
        else if (adjustedStage == 3)
        {
            return (decimal)6 / 3;
        }
        else if (adjustedStage == 4)
        {
            return (decimal)7 / 3;
        }
        else if (adjustedStage == 5)
        {
            return (decimal)8 / 3;
        }
        else if (adjustedStage >= 6)
        {
            return (decimal)9 / 3;
        }
        else
        {
            return (decimal)3 / 3;
        }

    }

    public int GetMiracleBerry(Pokemon attack, Pokemon defender, Move move)
    {
        return 1;
    }

    public int GetAffection(Pokemon attack, Pokemon defender, Move move)
    {
        return 0;
    }
}
