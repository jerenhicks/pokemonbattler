using Newtonsoft.Json;

public class Move
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string TypeName { get; set; }
    [JsonIgnore]
    public Type Type { get; private set; }
    public string CategoryName { get; set; }
    [JsonIgnore]
    public MoveCategory Category { get; private set; } // Physical, Special, or Status
    public int MaxPP { get; private set; }
    [JsonIgnore]
    public int PP { get; private set; }
    public int? Power { get; private set; }
    public decimal? Accuracy { get; private set; }
    public int Priority { get; private set; }
    public bool MakesContact { get; private set; }
    public bool AffectedByProtect { get; private set; }
    public bool AffectedBySnatch { get; private set; }
    public bool AffectedByMirrorMove { get; private set; }
    public bool Metronome { get; private set; }
    public List<string> EffectNames { get; set; }
    [JsonIgnore]
    public List<BaseEffect> Effects { get; private set; } = new List<BaseEffect>();
    [JsonIgnore]
    public bool IsNonDamage => Power == null || Power == 0;
    public string RangeName { get; set; }
    [JsonIgnore]
    public Range Range { get; private set; }

    public Move(int id, string name, Type type, MoveCategory category, int maxPP, int? power, decimal? accuracy, int priority, bool makesContact, bool affectedByProtect, bool metronome, bool affectedBySnatch, bool affectedByMirrorMove, Range range, List<BaseEffect> effects)
    {
        Id = id;
        Name = name;
        Type = type;
        Category = category;
        MaxPP = maxPP;
        PP = MaxPP;
        Power = power;
        Accuracy = accuracy;
        Priority = priority;
        MakesContact = makesContact;
        AffectedByProtect = affectedByProtect;
        AffectedBySnatch = affectedBySnatch;
        AffectedByMirrorMove = affectedByMirrorMove;
        Metronome = metronome;
        Range = range;
        if (effects != null)
        {
            Effects = effects;
        }
    }


    public Move Clone()
    {
        return new Move(Id, Name, Type, Category, PP, Power, Accuracy, Priority, MakesContact, AffectedByProtect, Metronome, AffectedBySnatch, AffectedByMirrorMove, Range, new List<BaseEffect>(Effects));
    }

    public void MoveUsed()
    {
        PP--;
    }

    public void Reset()
    {
        PP = MaxPP;
    }

    public void Unpack()
    {
        Type = TypeRepository.GetType(TypeName);
        Category = Enum.Parse<MoveCategory>(CategoryName, true);
        Range = Enum.Parse<Range>(RangeName, true);
        foreach (var effectName in EffectNames)
        {
            BaseEffect effect = null;

            if (effectName.Contains("(") && effectName.Contains(")"))
            {
                var effectNameParsed = effectName.Substring(0, effectName.IndexOf("("));
                var parameterString = effectName.Substring(effectName.IndexOf("(") + 1, effectName.IndexOf(")") - effectName.IndexOf("(") - 1);
                //var parameters = double.Parse(parameterString);
                //split parameters based on comma
                var paramsd = parameterString.ToString().Split(',');


                effect = EffectRepository.GetEffect(effectNameParsed);
                if (effect != null)
                {
                    effect.SetModifier(double.Parse(paramsd[0]));
                    if (paramsd.Length > 1)
                    {
                        effect.SetChance(double.Parse(paramsd[1]));
                    }
                }
            }
            else
            {
                effect = EffectRepository.GetEffect(effectName);
            }

            if (effect != null)
            {
                Effects.Add(effect);
            }
        }
    }

}