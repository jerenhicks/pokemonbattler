public class Pokemon
{
    public string Name { get; set; }
    public string TypeOne { get; set; }
    public string TypeTwo { get; set; }

    public int BaseHP { get; set; }
    public int BaseAtk { get; set; }
    public int BaseDef { get; set; }
    public int BaseSpAtk { get; set; }
    public int BaseSpDef { get; set; }
    public int BaseSpeed { get; set; }
    public int Level { get; set; }

    public int IVHP { get; set; }
    public int IVAtk { get; set; }
    public int IVDef { get; set; }
    public int IVSpAtk { get; set; }
    public int IVSpDef { get; set; }
    public int IVSpeed { get; set; }

    public int EVHP { get; set; }
    public int EVAtk { get; set; }
    public int EVDef { get; set; }
    public int EVSpAtk { get; set; }
    public int EVSpDef { get; set; }
    public int EVSpeed { get; set; }

    public Pokemon(string name, string typeOne, string typeTwo, int baseHP, int baseAtk, int baseDef, int baseSpAtk, int baseSpDef, int baseSpeed, int ivHP, int ivAtk, int ivDef, int ivSpAtk, int ivSpDef, int ivSpeed, int evHP, int evAtk, int evDef, int evSpAtk, int evSpDef, int evSpeed)
    {
        Name = name;
        TypeOne = typeOne;
        TypeTwo = typeTwo;
        BaseHP = baseHP;
        BaseAtk = baseAtk;
        BaseDef = baseDef;
        BaseSpAtk = baseSpAtk;
        BaseSpDef = baseSpDef;
        BaseSpeed = baseSpeed;
        Level = 1;
        IVHP = ivHP;
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
    }

    public void DisplayStatus()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Type: {TypeOne} {TypeTwo}");
        Console.WriteLine($"HP: {BaseHP}");
        Console.WriteLine($"Atk: {BaseAtk}");
        Console.WriteLine($"Def: {BaseDef}");
        Console.WriteLine($"SpAtk: {BaseSpAtk}");
        Console.WriteLine($"SpDef: {BaseSpDef}");
        Console.WriteLine($"Speed: {BaseSpeed}");
    }

    public void CalculateStats()
    {
        int hp = (2 * BaseHP + IVHP + EVHP / 4) * Level / 100 + Level + 10;
        int atk = (int)Math.Floor(Math.Floor((2 * BaseAtk + IVAtk + EVAtk) * Level / 100.0 + 5) * 1);
        int def = (int)Math.Floor(Math.Floor((2 * BaseDef + IVDef + EVDef) * Level / 100.0 + 5) * 1);
        int spAtk = (int)Math.Floor(Math.Floor((2 * BaseSpAtk + IVSpAtk + EVSpAtk) * Level / 100.0 + 5) * 1);
        int spDef = (int)Math.Floor(Math.Floor((2 * BaseSpDef + IVSpDef + EVSpDef) * Level / 100.0 + 5) * 1);
        int speed = (int)Math.Floor(Math.Floor((2 * BaseSpeed + IVSpeed + EVSpeed) * Level / 100.0 + 5) * 1);

        Console.WriteLine($"HP: {hp}");
        Console.WriteLine($"Atk: {atk}");
        Console.WriteLine($"Def: {def}");
        Console.WriteLine($"SpAtk: {spAtk}");
        Console.WriteLine($"SpDef: {spDef}");
        Console.WriteLine($"Speed: {speed}");
    }
}