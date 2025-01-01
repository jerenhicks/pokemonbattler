public class Nature
{
    public string Name { get; private set; }
    public double AttackModifier { get; private set; }
    public double DefenseModifier { get; private set; }
    public double SpecialAttackModifier { get; private set; }
    public double SpecialDefenseModifier { get; private set; }
    public double SpeedModifier { get; private set; }

    public Nature(string name, double attackModifier, double defenseModifier, double specialAttackModifier, double specialDefenseModifier, double speedModifier)
    {
        Name = name;
        AttackModifier = attackModifier;
        DefenseModifier = defenseModifier;
        SpecialAttackModifier = specialAttackModifier;
        SpecialDefenseModifier = specialDefenseModifier;
        SpeedModifier = speedModifier;
    }
}