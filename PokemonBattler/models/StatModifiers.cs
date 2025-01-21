using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class StatModifiers
{
    public int AtkStage { get; private set; }
    public int DefStage { get; private set; }
    public int SpAtkStage { get; private set; }
    public int SpDefStage { get; private set; }
    public int SpeedStage { get; private set; }
    public int AccuracyStage { get; private set; }
    public int EvasionStage { get; private set; }
    public Dictionary<int, double> statStageMultipliers = new Dictionary<int, double>();
    public Dictionary<int, double> accEvaStageMultipliers = new Dictionary<int, double>();

    public StatModifiers()
    {
        AtkStage = 0;
        DefStage = 0;
        SpAtkStage = 0;
        SpDefStage = 0;
        SpeedStage = 0;
        AccuracyStage = 0;
        EvasionStage = 0;

        statStageMultipliers.Add(-6, 2.0 / 8.0);
        statStageMultipliers.Add(-5, 2.0 / 7.0);
        statStageMultipliers.Add(-4, 2.0 / 6.0);
        statStageMultipliers.Add(-3, 2.0 / 5.0);
        statStageMultipliers.Add(-2, 2.0 / 4.0);
        statStageMultipliers.Add(-1, 2.0 / 3.0);
        statStageMultipliers.Add(0, 2.0 / 2.0);
        statStageMultipliers.Add(1, 3.0 / 2.0);
        statStageMultipliers.Add(2, 4.0 / 2.0);
        statStageMultipliers.Add(3, 5.0 / 2.0);
        statStageMultipliers.Add(4, 6.0 / 2.0);
        statStageMultipliers.Add(5, 7.0 / 2.0);
        statStageMultipliers.Add(6, 8.0 / 2.0);

        accEvaStageMultipliers.Add(-6, 3.0 / 9.0);
        accEvaStageMultipliers.Add(-5, 3.0 / 8.0);
        accEvaStageMultipliers.Add(-4, 3.0 / 7.0);
        accEvaStageMultipliers.Add(-3, 3.0 / 6.0);
        accEvaStageMultipliers.Add(-2, 3.0 / 5.0);
        accEvaStageMultipliers.Add(-1, 3.0 / 4.0);
        accEvaStageMultipliers.Add(0, 3.0 / 3.0);
        accEvaStageMultipliers.Add(1, 4.0 / 3.0);
        accEvaStageMultipliers.Add(2, 5.0 / 3.0);
        accEvaStageMultipliers.Add(3, 6.0 / 3.0);
        accEvaStageMultipliers.Add(4, 7.0 / 3.0);
        accEvaStageMultipliers.Add(5, 8.0 / 3.0);
        accEvaStageMultipliers.Add(6, 9.0 / 3.0);
    }

    public bool ChangeAtkStage(int amount)
    {
        AtkStage += amount;
        if (AtkStage > 6)
        {
            AtkStage = 6;
            return false;
        }
        else if (AtkStage < -6)
        {
            AtkStage = -6;
            return false;
        }
        return true;
    }

    public double GetAtkModifier()
    {
        return statStageMultipliers[AtkStage];
    }

    public bool ChangeDefStage(int amount)
    {
        DefStage += amount;
        if (DefStage > 6)
        {
            DefStage = 6;
            return false;
        }
        else if (DefStage < -6)
        {
            DefStage = -6;
            return false;
        }
        return true;
    }

    public double GetDefModifier()
    {
        return statStageMultipliers[DefStage];
    }

    public bool ChangeSpAtkStage(int amount)
    {
        SpAtkStage += amount;
        if (SpAtkStage > 6)
        {
            SpAtkStage = 6;
            return false;
        }
        else if (SpAtkStage < -6)
        {
            SpAtkStage = -6;
            return false;
        }
        return true;
    }

    public double GetSpAtkModifier()
    {
        return statStageMultipliers[SpAtkStage];
    }

    public bool ChangeSpDefStage(int amount)
    {
        SpDefStage += amount;
        if (SpDefStage > 6)
        {
            SpDefStage = 6;
            return false;
        }
        else if (SpDefStage < -6)
        {
            SpDefStage = -6;
            return false;
        }
        return true;
    }

    public double GetSpDefModifier()
    {
        return statStageMultipliers[SpDefStage];
    }

    public bool ChangeSpeedStage(int amount)
    {
        SpeedStage += amount;
        if (SpeedStage > 6)
        {
            SpeedStage = 6;
            return false;
        }
        else if (SpeedStage < -6)
        {
            SpeedStage = -6;
            return false;
        }
        return true;
    }

    public double GetSpeedModifier()
    {
        return statStageMultipliers[SpeedStage];
    }

    public bool ChangeAccuracyStage(int amount)
    {
        AccuracyStage += amount;
        if (AccuracyStage > 6)
        {
            AccuracyStage = 6;
            return false;
        }
        else if (AccuracyStage < -6)
        {
            AccuracyStage = -6;
            return false;
        }
        return true;
    }

    public double GetAccuracyModifier()
    {
        return accEvaStageMultipliers[AccuracyStage];
    }

    public bool ChangeEvasionStage(int amount)
    {
        EvasionStage += amount;
        if (EvasionStage > 6)
        {
            EvasionStage = 6;
            return false;
        }
        else if (EvasionStage < -6)
        {
            EvasionStage = -6;
            return false;
        }
        return true;
    }

    public double GetEvasionModifier()
    {
        return accEvaStageMultipliers[EvasionStage];
    }

    public void ResetAll()
    {
        AtkStage = 0;
        DefStage = 0;
        SpAtkStage = 0;
        SpDefStage = 0;
        SpeedStage = 0;
        AccuracyStage = 0;
        EvasionStage = 0;
    }

}
