public class Move
{
    public string Name { get; private set; }
    public Type Type { get; private set; }
    public MoveCategory Category { get; private set; } // Physical, Special, or Status
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
    public BaseEffect Effect { get; private set; }
    public bool IsNonDamage => Power == null;

    public Move(string name, Type type, MoveCategory category, int pp, int? power, decimal? accuracy, int priority, bool makesContact, bool affectedByProtect, bool affectedByMagicCoat, bool affectedBySnatch, bool affectedByMirrorMove, bool affectedByKingsRock, BaseEffect effect)
    {
        Name = name;
        Type = type;
        Category = category;
        PP = pp;
        Power = power;
        Accuracy = accuracy;
        Priority = priority;
        MakesContact = MakesContact;
        AffectedByProtect = affectedByProtect;
        AffectedByMagicCoat = affectedByMagicCoat;
        AffectedBySnatch = affectedBySnatch;
        AffectedByMirrorMove = affectedByMirrorMove;
        AffectedByKingsRock = affectedByKingsRock;
        Effect = effect;
    }


    public Move Clone()
    {
        return new Move(Name, Type, Category, PP, Power, Accuracy, Priority, MakesContact, AffectedByProtect, AffectedByMagicCoat, AffectedBySnatch, AffectedByMirrorMove, AffectedByKingsRock, Effect);
    }

    public void MoveUsed()
    {
        PP--;
    }

}