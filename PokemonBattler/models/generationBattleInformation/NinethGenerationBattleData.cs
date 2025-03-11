using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class NinethGenerationBattleData : GenerationBattleData
{
    private Random random = new Random();

    public NinethGenerationBattleData()
    {
    }

    public void SetRandom(Random random)
    {
        this.random = random;
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

    public override (int, Boolean, double) CalculateDamage(Pokemon attacker, Pokemon defender, Move move, int criticalOverride = 0)
    {
        double categoryDamage = 0;
        var BurnStatus = 1.0;
        if (attacker.NonVolatileStatus == NonVolatileStatus.Burn && move.Category == MoveCategory.Physical)
        {
            BurnStatus = 0.5;
        }

        var TargetsStatus = 1;
        var WeatherStatus = 1;

        var CriticalHitStatus = 1;


        var PBStatus = 1;
        var GlaiveRushStatus = 1;
        var OtherStatus = 1;
        var ZMoveStatus = 1;
        var TeraShieldStatus = 1;

        var RandomStatus = random.Next(85, 101) / 100.0;

        var criticalStage = 0;
        var randomNumber = 0;
        if (criticalStage == 0)
        {
            randomNumber = random.Next(1, 25);
        }
        else if (criticalStage == 1)
        {
            randomNumber = random.Next(1, 9);
        }
        else if (criticalStage == 2)
        {
            randomNumber = random.Next(1, 3);
        }
        else if (criticalStage >= 3)
        {
            randomNumber = random.Next(1, 1);
        }

        if (randomNumber == 1 && criticalOverride == 0) // 1/24 chance
        {
            CriticalHitStatus = 2; // Critical hit doubles the damage
        }

        var STABStatus = 1.0;
        if (attacker.TypeOne == move.Type || attacker.TypeTwo == move.Type)
        {
            STABStatus = 1.5;
        }

        var Type1Status = 1.0;
        Type1Status = move.Type.GetEffectiveness(defender.TypeOne, defender.TypeTwo);


        if (move.Category == MoveCategory.Physical)
        {
            categoryDamage = (double)attacker.CurrentAtk / defender.CurrentDef;
        }
        else if (move.Category == MoveCategory.Special)
        {
            categoryDamage = (double)attacker.CurrentSpAtk / defender.CurrentSpDef;
        }

        double initialDamage = (((2 * attacker.Level / 5 + 2) * (move.Power ?? 0) * categoryDamage) / 50) + 2;
        var secondEffects = initialDamage * TargetsStatus * PBStatus * WeatherStatus * GlaiveRushStatus * CriticalHitStatus * RandomStatus * STABStatus * Type1Status * BurnStatus * OtherStatus * ZMoveStatus * TeraShieldStatus;


        if (secondEffects == 0 && Type1Status != 0)
        {
            secondEffects = 1;
        }
        //FIXME: THIS IS PROBABLY NOT RIGHT
        return ((int)secondEffects, CriticalHitStatus == 2, Type1Status);
    }

}
