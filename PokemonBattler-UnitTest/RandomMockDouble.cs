// Mock Random class to control randomness
public class MockRandomDouble : Random
{
    private readonly double fixedValue;

    public MockRandomDouble(double fixedValue)
    {
        this.fixedValue = fixedValue;
    }

    public override double NextDouble()
    {
        return fixedValue;
    }
}