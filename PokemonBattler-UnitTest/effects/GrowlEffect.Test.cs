using Xunit;

public class GrowlEffectTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public GrowlEffectTest(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void TestDoEffect()
    {
        // Arrange
        var growlEffect = new GrowlEffect();
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Initial AtkStage of defender
        var initialAtkStage = defender.StatModifiers.AtkStage;

        // Act
        growlEffect.PostDamageEffect(attacker, defender, move);

        // Assert
        Assert.Equal(initialAtkStage - 1, defender.StatModifiers.AtkStage);
    }

    [Fact]
    public void TestDoEffect_MinAtkStage()
    {
        // Arrange
        var growlEffect = new GrowlEffect();
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Set defender's AtkStage to minimum
        defender.StatModifiers.ChangeAtkStage(-6);

        // Act
        growlEffect.PostDamageEffect(attacker, defender, move);

        // Assert
        Assert.Equal(-6, defender.StatModifiers.AtkStage); // AtkStage should not go below -6
    }

    [Fact]
    public void SetModifier_DoesNotChangeAnything()
    {
        // Arrange
        var growlEffect = new GrowlEffect();

        // Act
        growlEffect.SetModifier(1.0);

        // Assert
        // No assertion needed as SetModifier does nothing
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var growlEffect = new GrowlEffect();
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Act
        var result = growlEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

}