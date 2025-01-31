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
    [JsonIgnore]
    public TestFlag Flags { get; set; }
    public List<string> EffectNames { get; set; }
    [JsonIgnore]
    public List<BaseEffect> Effects { get; private set; } = new List<BaseEffect>();

    public string RangeName { get; set; }
    [JsonIgnore]
    public Range Range { get; private set; }

    public TestMove(int id, string name, Type type, MoveCategory category, int maxPP, int? power, decimal? accuracy, int priority, Range range, TestFlag flags, List<BaseEffect> effects)
    {
        Id = id;
        Name = name;
        Type = type;
        Category = category;
        MaxPP = maxPP;
        PP = MaxPP;
        Power = power;
        Accuracy = Accuracy.HasValue ? Accuracy / 100 : null; ;
        Priority = priority;
        Range = range;
        Flags = flags;
        if (Flags == null)
        {
            Flags = new TestFlag();
        }
        MakesContact = Flags.Contact.HasValue ? Flags.Contact == 1 : false;
        AffectedByProtect = Flags.Protect.HasValue ? Flags.Protect == 1 : false;
        AffectedBySnatch = Flags.Snatch.HasValue ? Flags.Snatch == 1 : false;
        AffectedByMirrorMove = Flags.Mirror.HasValue ? Flags.Mirror == 1 : false;
        Metronome = Flags.Metronome.HasValue ? Flags.Metronome == 1 : false;

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