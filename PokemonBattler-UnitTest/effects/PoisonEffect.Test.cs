using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class PoisonEffectTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public PoisonEffectTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestPoisonEffect()
    {
        // Arrange
        var poisonEffect = new PoisonEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");



        // Act
        var returnVals = poisonEffect.PostDamageEffect(attacker, defender, move);

        // Assert
        Assert.True(returnVals.Count == 0);
    }

    [Fact]
    public void SetModifier_DoesNotChangeAnything()
    {
        // Arrange
        var poisonEffect = new PoisonEffect();

        // Act
        poisonEffect.SetModifier(1.0);

        // Assert
        // No assertion needed as SetModifier does nothing
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var poisonEffect = new PoisonEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Act
        var result = poisonEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }
}
