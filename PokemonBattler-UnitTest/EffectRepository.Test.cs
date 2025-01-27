using Xunit;

public class EffectRepositoryTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public EffectRepositoryTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestLoadEffects()
    {
        // Act
        var burnEffect = EffectRepository.GetEffect("burneffect");
        var poisonEffect = EffectRepository.GetEffect("poisoneffect");

        // Assert
        Assert.NotNull(burnEffect);
        Assert.IsType<BurnEffect>(burnEffect);

        Assert.NotNull(poisonEffect);
        Assert.IsType<PoisonEffect>(poisonEffect);
    }

    [Fact]
    public void TestAddEffect()
    {
        // Arrange
        var customEffect = new CustomEffect();

        // Act
        EffectRepository.AddEffect(customEffect);
        var retrievedEffect = EffectRepository.GetEffect("customeffect");

        // Assert
        Assert.NotNull(retrievedEffect);
        Assert.IsType<CustomEffect>(retrievedEffect);
    }

    [Fact]
    public void TestGetEffect_NotFound()
    {
        // Act & Assert
        Assert.Equal(null, EffectRepository.GetEffect("nonexistent"));
    }
}

// Example of a custom effect class for testing
public class CustomEffect : BaseEffect
{

    public override void SetModifier(double amount)
    {
        // Implement custom modifier logic here
    }

    public override List<string> DoEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement custom effect logic here
        return new List<string>();
    }

}