using Newtonsoft.Json;

public class TestMove
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
    public TestFlag flags { get; set; }
    public List<string> EffectNames { get; set; }
    [JsonIgnore]
    public List<BaseEffect> Effects { get; private set; } = new List<BaseEffect>();

    public string RangeName { get; set; }
    [JsonIgnore]
    public Range Range { get; private set; }

    public TestMove(int id, string name, Type type, MoveCategory category, int pp, int? power, decimal? accuracy, int priority, Range range, TestFlag flag, List<BaseEffect> effects)
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
        Range = range;
        flags = flag;

        MakesContact = flag.Contact.HasValue ? flag.Contact == 1 : false;
        AffectedByProtect = flag.Protect.HasValue ? flag.Protect == 1 : false;
        AffectedBySnatch = flag.Snatch.HasValue ? flag.Snatch == 1 : false;
        AffectedByMirrorMove = flag.Mirror.HasValue ? flag.Mirror == 1 : false;
        Metronome = flag.Metronome.HasValue ? flag.Metronome == 1 : false;

        if (effects != null)
        {
            Effects = effects;
        }
    }

    public void Unpack()
    {
        Type = TypeRepository.GetType(TypeName);
        Category = Enum.Parse<MoveCategory>(CategoryName, true);
        Range = Enum.Parse<Range>(RangeName, true);
        if (EffectNames == null)
        {
            EffectNames = new List<string>();
        }
        else
        {
            Effects = EffectNames.Select(effectName => EffectRepository.GetEffect(effectName)).ToList();
        }
    }



}