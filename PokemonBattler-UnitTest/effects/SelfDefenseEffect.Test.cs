using System;
using System.Collections.Generic;
using Xunit;

public class SelfDefenseEffectTests : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public SelfDefenseEffectTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void SeChance_SetsCorrectValue()
    {
        // Arrange
        var SelfRaiseDefenseEffect = new SelfDefenseEffect();
        double modifier = 0.33;

        // Act
        SelfRaiseDefenseEffect.SetChance(modifier);

        // Assert
        Assert.Equal(modifier, SelfRaiseDefenseEffect.Chance);
    }

    [Fact]
    public void SetModifier_SetsCorrectValue()
    {
        // Arrange
        var SelfRaiseDefenseEffect = new SelfDefenseEffect();
        double modifier = 0.33;

        // Act
        SelfRaiseDefenseEffect.SetModifier(modifier);

        // Assert
        Assert.Equal(modifier, SelfRaiseDefenseEffect.Modifier);
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var SelfRaiseDefenseEffect = new SelfDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid armor");

        // Act
        var result = SelfRaiseDefenseEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void PostDamageEffect_RaiseDefenseWithChance()
    {
        // Arrange
        var SelfRaiseDefenseEffect = new SelfDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid armor");
        int damageDone = 40;
        SelfRaiseDefenseEffect.SetModifier(1.0); // 100% chance for testing
        SelfRaiseDefenseEffect.SetModifier(2.0); // 100% chance for testing

        // Mock Random class to control randomness
        var mockRandom = new MockRandomDouble(0.5); // Value within the chance
        SelfRaiseDefenseEffect.SetRandom(mockRandom);

        // Act
        var result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.Contains(result, message => message.Contains("defense rose sharply!") || message.Contains("defense can't go any higher"));
    }

    [Fact]
    public void PostDamageEffect_DoesNotLowerDefenseWithLowChance()
    {
        // Arrange
        var SelfRaiseDefenseEffect = new SelfDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid armor");
        int damageDone = 40;
        SelfRaiseDefenseEffect.SetModifier(0.0); // 0% chance for testing

        // Mock Random class to control randomness
        var mockRandom = new MockRandomDouble(0.5); // Value outside the chance
        SelfRaiseDefenseEffect.SetRandom(mockRandom);

        // Act
        var result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, damageDone);
    }

    [Fact]
    public void SetRandom()
    {
        // Arrange
        var SelfDefenseEffect = new SelfDefenseEffect();
        var random = new Random();
        SelfDefenseEffect.SetRandom(random);

        Assert.Equal(random, SelfDefenseEffect.Random);
    }


    [Fact]
    public void CheckAllConditions()
    {
        // Arrange
        var SelfRaiseDefenseEffect = new SelfDefenseEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("acid armor");


        SelfRaiseDefenseEffect.SetModifier(-2);

        // Act
        var result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense fell sharply"));

        SelfRaiseDefenseEffect.SetModifier(-1);

        // Act
        result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense fell"));

        SelfRaiseDefenseEffect.SetModifier(1);

        // Act
        result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense rose"));

        SelfRaiseDefenseEffect.SetModifier(2);

        // Act
        result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense rose sharply"));

        SelfRaiseDefenseEffect.SetModifier(12);

        // Act
        SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);
        result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense can't go any higher"));

        SelfRaiseDefenseEffect.SetModifier(-12);

        // Act
        SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);
        result = SelfRaiseDefenseEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("defense can't go any lower"));

    }
}

