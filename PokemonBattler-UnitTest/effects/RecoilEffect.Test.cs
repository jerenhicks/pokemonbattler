using Xunit;
using System.Collections.Generic;

public class RecoilEffectTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public RecoilEffectTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestDoEffect_Struggle()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        recoilEffect.SetModifier(0.25);
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("struggle");

        // Set initial HP
        attacker.CurrentHP = attacker.HP;

        // Act
        var messages = recoilEffect.PostDamageEffect(attacker, defender, move, 0);

        // Calculate expected recoil damage (25% of max HP)
        var expectedDamage = (int)(attacker.HP * 0.25);

        // Assert
        Assert.Equal(attacker.HP - expectedDamage, attacker.CurrentHP);
        Assert.Contains($"{attacker.Name} is hit with recoil!", messages);
    }


    [Fact]
    public void TestDoEffect_BraveBird()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        recoilEffect.SetModifier(0.33);
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("brave bird");

        // Set initial HP
        attacker.CurrentHP = attacker.HP;

        // Act
        var messages = recoilEffect.PostDamageEffect(attacker, defender, move, 0);

        // Calculate expected recoil damage (33% of max HP)
        var expectedDamage = (int)(attacker.HP * 0.33);

        // Assert
        Assert.Equal(attacker.HP - expectedDamage, attacker.CurrentHP);
        Assert.Contains($"{attacker.Name} is hit with recoil!", messages);
    }

    [Fact]
    public void SetRandom()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        var random = new Random();
        recoilEffect.SetRandom(random);

        Assert.Equal(random, recoilEffect.Random);
    }

    [Fact]
    public void SeChance_SetsCorrectValue()
    {
        // Arrange
        var RecoilEffect = new RecoilEffect();
        double modifier = 0.33;

        // Act
        RecoilEffect.SetChance(modifier);

        // Assert
        Assert.Equal(modifier, RecoilEffect.Chance);
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("brave bird");

        // Act
        var result = recoilEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void PostDamageEffect_ModifierIsZero()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        recoilEffect.SetModifier(0);
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("brave bird");

        // Set initial HP
        attacker.CurrentHP = attacker.HP;

        // Act
        var messages = recoilEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Empty(messages);
    }
}