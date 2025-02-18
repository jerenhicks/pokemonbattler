using System;
using System.Collections.Generic;
using Xunit;

public class OpponentDefenseEffectTests : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public OpponentDefenseEffectTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void SeChance_SetsCorrectValue()
    {
        // Arrange
        var lowerDefenseEffect = new OpponentDefenseEffect();
        double modifier = 0.33;

        // Act
        lowerDefenseEffect.SetChance(modifier);

        // Assert
        Assert.Equal(modifier, lowerDefenseEffect.Chance);
    }

    [Fact]
    public void SetModifier_SetsCorrectValue()
    {
        // Arrange
        var lowerDefenseEffect = new OpponentDefenseEffect();
        double modifier = 0.33;

        // Act
        lowerDefenseEffect.SetModifier(modifier);

        // Assert
        Assert.Equal(modifier, lowerDefenseEffect.Modifier);
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var lowerDefenseEffect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");

        // Act
        var result = lowerDefenseEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void PostDamageEffect_LowersDefenseWithChance()
    {
        // Arrange
        var effect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");
        int damageDone = 40;
        effect.SetModifier(-1); // Lower defense
        effect.SetChance(1.0); // 100% chance for testing

        // Mock Random class to control randomness
        var MockRandomDouble = new MockRandomDouble(0.5); // Value within the chance
        effect.SetRandom(MockRandomDouble);

        // Act
        var result = effect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.Contains(result, message => message.Contains("defense fell"));
    }

    [Fact]
    public void PostDamageEffect_RaisesDefenseWithChance()
    {
        // Arrange
        var effect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");
        int damageDone = 40;
        effect.SetModifier(1); // Raise defense
        effect.SetChance(1.0); // 100% chance for testing

        // Mock Random class to control randomness
        var MockRandomDouble = new MockRandomDouble(0.5); // Value within the chance
        effect.SetRandom(MockRandomDouble);

        // Act
        var result = effect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.Contains(result, message => message.Contains("defense rose"));
    }

    [Fact]
    public void PostDamageEffect_DoesNotChangeDefenseWithLowChance()
    {
        // Arrange
        var effect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");
        int damageDone = 40;
        effect.SetModifier(-1); // Lower defense
        effect.SetChance(0.0); // 0% chance for testing

        // Mock Random class to control randomness
        var MockRandomDouble = new MockRandomDouble(0.5); // Value outside the chance
        effect.SetRandom(MockRandomDouble);

        // Act
        var result = effect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.DoesNotContain(result, message => message.Contains("defense fell"));
    }

    [Fact]
    public void PostDamageEffect_DefenseCannotGoLower()
    {
        // Arrange
        var effect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");
        int damageDone = 40;
        effect.SetModifier(-1); // Lower defense
        effect.SetChance(1.0); // 100% chance for testing

        // Mock Random class to control randomness
        var MockRandomDouble = new MockRandomDouble(0.5); // Value within the chance
        effect.SetRandom(MockRandomDouble);

        // Lower defense to the minimum
        for (int i = 0; i < 6; i++)
        {
            defender.StatModifiers.ChangeDefStage(-1);
        }

        // Act
        var result = effect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.Contains(result, message => message.Contains("defense can't go any lower"));
    }

    [Fact]
    public void PostDamageEffect_DefenseCannotGoHigher()
    {
        // Arrange
        var effect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");
        int damageDone = 40;
        effect.SetModifier(1); // Raise defense
        effect.SetChance(1.0); // 100% chance for testing

        // Mock Random class to control randomness
        var MockRandomDouble = new MockRandomDouble(0.5); // Value within the chance
        effect.SetRandom(MockRandomDouble);

        // Raise defense to the maximum
        for (int i = 0; i < 6; i++)
        {
            defender.StatModifiers.ChangeDefStage(1);
        }

        // Act
        var result = effect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.Contains(result, message => message.Contains("defense can't go any higher"));
    }

    [Fact]
    public void SetRandom()
    {
        // Arrange
        var OpponentDefenseEffect = new OpponentDefenseEffect();
        var random = new Random();
        OpponentDefenseEffect.SetRandom(random);

        Assert.Equal(random, OpponentDefenseEffect.Random);
    }

    [Fact]
    public void CheckAllConditions()
    {
        // Arrange
        var effect = new OpponentDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid");


        effect.SetModifier(-2);

        // Act
        var result = effect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense fell sharply"));

        effect.SetModifier(-1);

        // Act
        result = effect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense fell"));

        effect.SetModifier(1);

        // Act
        result = effect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense rose"));

        effect.SetModifier(2);

        // Act
        result = effect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense rose sharply"));

        effect.SetModifier(12);

        // Act
        effect.PostDamageEffect(attacker, defender, move, 0);
        result = effect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense can't go any higher"));

        effect.SetModifier(-12);

        // Act
        effect.PostDamageEffect(attacker, defender, move, 0);
        result = effect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense can't go any lower"));

    }
}


