using System;
using System.Collections;

public class Pokemon
{
    public string Name { get; private set; }
    public Guid ID { get; private set; } = Guid.NewGuid();
    public int PokedexNumber { get; private set; }
    public Type TypeOne { get; private set; }
    public Type TypeTwo { get; private set; }
    public Nature Nature { get; private set; }
    public StatModifiers StatModifiers { get; private set; } = new StatModifiers();

    public int BaseHP { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseSpAtk { get; private set; }
    public int BaseSpDef { get; private set; }
    public int BaseSpeed { get; private set; }
    public int Level { get; private set; }

    public int IVHP { get; private set; }
    public int IVAtk { get; private set; }
    public int IVDef { get; private set; }
    public int IVSpAtk { get; private set; }
    public int IVSpDef { get; private set; }
    public int IVSpeed { get; private set; }

    public int EVHP { get; private set; }
    public int EVAtk { get; private set; }
    public int EVDef { get; private set; }
    public int EVSpAtk { get; private set; }
    public int EVSpDef { get; private set; }
    public int EVSpeed { get; private set; }

    // New properties for calculated stats
    public int HP { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int SpAtk { get; private set; }
    public int SpDef { get; private set; }
    public int Speed { get; private set; }

    public int CurrentHP { get; set; }
    public int CurrentAtk => (int)(Atk * StatModifiers.GetAtkModifier());
    public int CurrentDef => (int)(Def * StatModifiers.GetDefModifier());
    public int CurrentSpAtk => (int)(SpAtk * StatModifiers.GetSpAtkModifier());
    public int CurrentSpDef => (int)(SpDef * StatModifiers.GetSpDefModifier());
    public int CurrentSpeed => (int)(Speed * StatModifiers.GetSpeedModifier());

    public List<Move> Moves { get; set; } = new List<Move>();
    public NonVolatileStatus NonVolatileStatus { get; private set; } = NonVolatileStatus.None;

    public Pokemon(PokemonTemplate template, Nature nature, int ivhp = 0, int ivAtk = 0, int ivDef = 0, int ivSpAtk = 0, int ivSpDef = 0, int ivSpeed = 0, int evHP = 0, int evAtk = 0, int evDef = 0, int evSpAtk = 0, int evSpDef = 0, int evSpeed = 0, int level = 1)
    {
        Name = template.Name;
        PokedexNumber = template.PokedexNumber;
        TypeOne = template.TypeOne;
        TypeTwo = template.TypeTwo;
        Nature = nature;
        BaseHP = template.BaseHP;
        BaseAtk = template.BaseAtk;
        BaseDef = template.BaseDef;
        BaseSpAtk = template.BaseSpAtk;
        BaseSpDef = template.BaseSpDef;
        BaseSpeed = template.BaseSpeed;
        Level = level;

        // Check IVs
        if (ivhp < 0 || ivhp > 31 || ivAtk < 0 || ivAtk > 31 || ivDef < 0 || ivDef > 31 || ivSpAtk < 0 || ivSpAtk > 31 || ivSpDef < 0 || ivSpDef > 31 || ivSpeed < 0 || ivSpeed > 31)
        {
            throw new ArgumentException("IV stats must be between 0 and 31.");
        }

        // Check EVs
        if (evHP < 0 || evHP > 252 || evAtk < 0 || evAtk > 252 || evDef < 0 || evDef > 252 || evSpAtk < 0 || evSpAtk > 252 || evSpDef < 0 || evSpDef > 252 || evSpeed < 0 || evSpeed > 252)
        {
            throw new ArgumentException("EV stats must be between 0 and 252.");
        }

        // Check total EVs
        int totalEVs = evHP + evAtk + evDef + evSpAtk + evSpDef + evSpeed;
        if (totalEVs > 510)
        {
            throw new ArgumentException("Total EV stats must not exceed 510.");
        }

        IVHP = ivhp;
        IVAtk = ivAtk;
        IVDef = ivDef;
        IVSpAtk = ivSpAtk;
        IVSpDef = ivSpDef;
        IVSpeed = ivSpeed;

        EVHP = evHP;
        EVAtk = evAtk;
        EVDef = evDef;
        EVSpAtk = evSpAtk;
        EVSpDef = evSpDef;
        EVSpeed = evSpeed;

        CalculateStats();
        ResetCurrentStats();
    }

    private void CalculateStats()
    {
        HP = (2 * BaseHP + IVHP + EVHP / 4) * Level / 100 + Level + 10;
        Atk = (int)Math.Floor(Math.Floor((2 * BaseAtk + IVAtk + EVAtk / 4) * Level / 100.0 + 5) * Nature.AttackModifier);
        Def = (int)Math.Floor(Math.Floor((2 * BaseDef + IVDef + EVDef / 4) * Level / 100.0 + 5) * Nature.DefenseModifier);
        SpAtk = (int)Math.Floor(Math.Floor((2 * BaseSpAtk + IVSpAtk + EVSpAtk / 4) * Level / 100.0 + 5) * Nature.SpecialAttackModifier);
        SpDef = (int)Math.Floor(Math.Floor((2 * BaseSpDef + IVSpDef + EVSpDef / 4) * Level / 100.0 + 5) * Nature.SpecialDefenseModifier);
        Speed = (int)Math.Floor(Math.Floor((2 * BaseSpeed + IVSpeed + EVSpeed / 4) * Level / 100.0 + 5) * Nature.SpeedModifier);
    }


    public void LevelUp(int levelToLevelTo)
    {
        Level = levelToLevelTo;
        CalculateStats();
    }

    public void ResetCurrentStats()
    {
        CurrentHP = HP;
        StatModifiers.ResetAll();
    }

    public void ResetNonVolatileStatuses()
    {
        NonVolatileStatus = NonVolatileStatus.None;
    }

    //FIXME: should maybe error catch to prevent more than 4? Tell someone it didn't work. Throw an exception?
    public void AddMove(Move move)
    {
        if (Moves.Count < 4)
        {
            Moves.Add(move);
        }
    }

    public bool AddNonVolatileStatus(NonVolatileStatus status)
    {
        bool wasAdded = false;
        if (NonVolatileStatus == NonVolatileStatus.None)
        {
            NonVolatileStatus = status;
            wasAdded = true;
        }
        return wasAdded;
    }
}