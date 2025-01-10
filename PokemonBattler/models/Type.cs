using System.Collections.Generic;

public class Type
{
    public string Name { get; private set; }
    public List<Type> SuperEffectiveAgainst { get; private set; }
    public List<Type> NotEffectiveAgainst { get; private set; }
    public List<Type> NoEffectAgainst { get; private set; }

    public Type(string name)
    {
        Name = name;
        SuperEffectiveAgainst = new List<Type>();
        NotEffectiveAgainst = new List<Type>();
        NoEffectAgainst = new List<Type>();
    }

    public int GetEffectiveness(Type type1, Type type2)
    {
        var effectivenessStage = 0;


        return 1;
    }
}