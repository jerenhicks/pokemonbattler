public class Move
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Type Type { get; private set; }
    public MoveCategory Category { get; private set; } // Physical, Special, or Status
    public int MaxPP { get; private set; }
    public int PP { get; private set; }
    public int? Power { get; private set; }
    public decimal? Accuracy { get; private set; }
    public int Priority { get; private set; }
    public bool MakesContact { get; private set; }
    public bool AffectedByProtect { get; private set; }
    public bool AffectedByMagicCoat { get; private set; }
    public bool AffectedBySnatch { get; private set; }
    public bool AffectedByMirrorMove { get; private set; }
    public bool AffectedByKingsRock { get; private set; }
    public List<BaseEffect> Effects { get; private set; }
    public bool IsNonDamage => Power == null;
    public Range Range { get; private set; }

    public Move(int id, string name, Type type, MoveCategory category, int pp, int? power, decimal? accuracy, int priority, bool makesContact, bool affectedByProtect, bool affectedByMagicCoat, bool affectedBySnatch, bool affectedByMirrorMove, bool affectedByKingsRock, Range range, List<BaseEffect> effects)
    {
        Id = id;
        Name = name;
        Type = type;
        Category = category;
        MaxPP = pp;
        PP = MaxPP;
        Power = power;
        Accuracy = accuracy;
        Priority = priority;
        MakesContact = MakesContact;
        AffectedByProtect = affectedByProtect;
        AffectedByMagicCoat = affectedByMagicCoat;
        AffectedBySnatch = affectedBySnatch;
        AffectedByMirrorMove = affectedByMirrorMove;
        AffectedByKingsRock = affectedByKingsRock;
        Range = range;
        Effects = effects;
    }


    public Move Clone()
    {
        return new Move(Id, Name, Type, Category, PP, Power, Accuracy, Priority, MakesContact, AffectedByProtect, AffectedByMagicCoat, AffectedBySnatch, AffectedByMirrorMove, AffectedByKingsRock, Range, new List<BaseEffect>(Effects));
    }

    public void MoveUsed()
    {
        PP--;
    }

    public void Reset()
    {
        PP = MaxPP;
    }

}