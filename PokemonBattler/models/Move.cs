public class Move
{
    public string Name { get; private set; }
    public Type Type { get; private set; }
    public MoveCategory Category { get; private set; } // Physical, Special, or Status
    public int PP { get; private set; }
    public int Power { get; private set; }
    public decimal? Accuracy { get; private set; }

    public Move(string name, Type type, MoveCategory category, int pp, int power, decimal? accuracy)
    {
        Name = name;
        Type = type;
        Category = category;
        PP = pp;
        Power = power;
        Accuracy = accuracy;
    }
}