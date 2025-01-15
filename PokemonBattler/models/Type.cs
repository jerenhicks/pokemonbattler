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

    public double GetEffectiveness(Type type1, Type type2)
    {
        var effectivenessStage = 0;
        effectivenessStage += CheckEffectivessCategory(type1);
        effectivenessStage += CheckEffectivessCategory(type2);

        if (effectivenessStage == 0)
        {
            return 1;
        }
        else if (effectivenessStage == 1)
        {
            return 2;
        }
        else if (effectivenessStage == 2)
        {
            return 4;
        }
        else if (effectivenessStage == -1)
        {
            return 0.5;
        }
        else if (effectivenessStage == -2)
        {
            return 0.25;
        }
        else if (effectivenessStage == -10)
        {
            return 0;
        }

        return 1;
    }

    private int CheckEffectivessCategory(Type type)
    {
        if (type == null)
        {
            return 0;
        }
        if (SuperEffectiveAgainst.Contains(type))
        {
            return 1;
        }
        if (NotEffectiveAgainst.Contains(type))
        {
            return -1;
        }
        //FIXME: look into this
        if (NoEffectAgainst.Contains(type))
        {
            return -10;
        }

        return 0;
    }
}