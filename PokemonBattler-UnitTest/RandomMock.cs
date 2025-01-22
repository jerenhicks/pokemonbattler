// Mock class for Random
public class RandomMock : Random
{
    private readonly int _fixedValue;

    public RandomMock(int fixedValue)
    {
        _fixedValue = fixedValue;
    }

    public override int Next(int minValue, int maxValue)
    {
        return _fixedValue;
    }
}