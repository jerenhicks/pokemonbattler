using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class BurnEffectTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public BurnEffectTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestBurnEffect()
    {
        // Arrange
        var burnEffect = new BurnEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");



        // Act
        var returnVals = burnEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.True(returnVals.Count == 0);
    }

    [Fact]
    public void SetModifier_DoesNotChangeAnything()
    {
        // Arrange
        var burnEffect = new BurnEffect();

        // Act
        burnEffect.SetModifier(1.0);

        // Assert
        // No assertion needed as SetModifier does nothing
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var burnEffect = new BurnEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Act
        var result = burnEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SetRandom()
    {
        // Arrange
        var BurnEffect = new BurnEffect();
        var random = new Random();
        BurnEffect.SetRandom(random);

        Assert.Equal(random, BurnEffect.Random);
    }

    [Fact]
    public void SeChance_SetsCorrectValue()
    {
        // Arrange
        var BurnEffect = new BurnEffect();
        double modifier = 0.33;

        // Act
        BurnEffect.SetChance(modifier);

        // Assert
        Assert.Equal(modifier, BurnEffect.Chance);
    }
}
