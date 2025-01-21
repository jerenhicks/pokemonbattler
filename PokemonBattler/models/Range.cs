

public class Range
{
    public bool Opponent1 { get; private set; }
    public bool Opponent2 { get; private set; }
    public bool Opponent3 { get; private set; }
    public bool Self { get; private set; }
    public bool Ally1 { get; private set; }
    public bool Ally2 { get; private set; }

    public Range(bool opponent1, bool opponent2, bool opponent3, bool self, bool ally1, bool ally2)
    {
        Opponent1 = opponent1;
        Opponent2 = opponent2;
        Opponent3 = opponent3;
        Self = self;
        Ally1 = ally1;
        Ally2 = ally2;
    }
}
