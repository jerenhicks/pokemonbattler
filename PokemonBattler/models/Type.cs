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

        var effectivenessOneStage = CheckEffectivessCategory(type1);
        var effectivenessTwoStage = CheckEffectivessCategory(type2);

        if (effectivenessOneStage == -10 || effectivenessTwoStage == -10)
        {
            return 0;
        }

        if (effectivenessOneStage + effectivenessTwoStage == 0)
        {
            return 1;
        }
        else if (effectivenessOneStage + effectivenessTwoStage == 1)
        {
            return 2;
        }
        else if (effectivenessOneStage + effectivenessTwoStage == 2)
        {
            return 4;
        }
        else if (effectivenessOneStage + effectivenessTwoStage == -1)
        {
            return 0.5;
        }
        else if (effectivenessOneStage + effectivenessTwoStage == -2)
        {
            return 0.25;
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