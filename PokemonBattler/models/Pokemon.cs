using System;

public class Pokemon
{
    public string Name { get; private set; }
    public string ID { get; private set; } = IDGenerator.GenerateID();
    public int PokedexNumber { get; private set; }
    public Type TypeOne { get; private set; }
    public Type TypeTwo { get; private set; }
    public Nature Nature { get; private set; }

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
    public int CurrentAtk { get; set; }
    public int CurrentDef { get; set; }
    public int CurrentSpAtk { get; set; }
    public int CurrentSpDef { get; set; }
    public int CurrentSpeed { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public Pokemon(PokemonTemplate template)
    {
        Name = template.Name;
        PokedexNumber = template.PokedexNumber;
        TypeOne = template.TypeOne;
        TypeTwo = template.TypeTwo;
        Nature = template.Nature;
        BaseHP = template.BaseHP;
        BaseAtk = template.BaseAtk;
        BaseDef = template.BaseDef;
        BaseSpAtk = template.BaseSpAtk;
        BaseSpDef = template.BaseSpDef;
        BaseSpeed = template.BaseSpeed;
        Level = 1;
        IVHP = template.IVHP;
        IVAtk = template.IVAtk;
        IVDef = template.IVDef;
        IVSpAtk = template.IVSpAtk;
        IVSpDef = template.IVSpDef;
        IVSpeed = template.IVSpeed;
        EVHP = template.EVHP;
        EVAtk = template.EVAtk;
        EVDef = template.EVDef;
        EVSpAtk = template.EVSpAtk;
        EVSpDef = template.EVSpDef;
        EVSpeed = template.EVSpeed;
        if (template.Moves != null)
        {
            Moves = template.Moves;
        }

        CalculateStats();
    }

    public void DisplayStatus()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Type: {TypeOne} {TypeTwo}");
        Console.WriteLine($"HP: {HP}");
        Console.WriteLine($"Atk: {Atk}");
        Console.WriteLine($"Def: {Def}");
        Console.WriteLine($"SpAtk: {SpAtk}");
        Console.WriteLine($"SpDef: {SpDef}");
        Console.WriteLine($"Speed: {Speed}");
    }

    private void CalculateStats()
    {
        HP = (2 * BaseHP + IVHP + EVHP / 4) * Level / 100 + Level + 10;
        Atk = (int)Math.Floor(Math.Floor((2 * BaseAtk + IVAtk + EVAtk / 4) * Level / 100.0 + 5) * Nature.AttackModifier);
        Def = (int)Math.Floor(Math.Floor((2 * BaseDef + IVDef + EVDef / 4) * Level / 100.0 + 5) * Nature.DefenseModifier);
        SpAtk = (int)Math.Floor(Math.Floor((2 * BaseSpAtk + IVSpAtk + EVSpAtk / 4) * Level / 100.0 + 5) * Nature.SpecialAttackModifier);
        SpDef = (int)Math.Floor(Math.Floor((2 * BaseSpDef + IVSpDef + EVSpDef / 4) * Level / 100.0 + 5) * Nature.SpecialDefenseModifier);
        Speed = (int)Math.Floor(Math.Floor((2 * BaseSpeed + IVSpeed + EVSpeed / 4) * Level / 100.0 + 5) * Nature.SpeedModifier);

        CurrentHP = HP;
        CurrentAtk = Atk;
        CurrentDef = Def;
        CurrentSpAtk = SpAtk;
        CurrentSpDef = SpDef;
        CurrentSpeed = Speed;
    }


    public void LevelUp(int levelToLevelTo)
    {
        Level = levelToLevelTo;
        CalculateStats();
    }

    public void BattleReady()
    {
        CurrentHP = HP;
        CurrentAtk = Atk;
        CurrentDef = Def;
        CurrentSpAtk = SpAtk;
        CurrentSpDef = SpDef;
        CurrentSpeed = Speed;
    }

    //FIXME: should maybe error catch to prevent more than 4? Tell someone it didn't work. Throw an exception?
    public void AddMove(Move move)
    {
        if (Moves.Count < 4)
        {
            Moves.Add(move);
        }
    }
}