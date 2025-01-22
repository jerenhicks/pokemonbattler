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
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("struggle");

        // Set initial HP
        attacker.CurrentHP = attacker.HP;

        // Act
        var messages = recoilEffect.DoEffect(attacker, defender, move);

        // Calculate expected recoil damage (25% of max HP)
        var expectedDamage = (int)(attacker.HP * 0.25);

        // Assert
        Assert.Equal(attacker.HP - expectedDamage, attacker.CurrentHP);
        Assert.Contains($"{attacker.Name} is hit with recoil!", messages);
    }

    [Fact]
    public void TestDoEffect_NonRecoilMove()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("tackle");

        // Set initial HP
        attacker.CurrentHP = attacker.HP;

        // Act
        var messages = recoilEffect.DoEffect(attacker, defender, move);

        // Assert
        Assert.Equal(attacker.HP, attacker.CurrentHP); // No recoil damage should be applied
        Assert.Empty(messages); // No messages should be returned
    }

    [Fact]
    public void TestDoEffect_BraveBird()
    {
        // Arrange
        var recoilEffect = new RecoilEffect();
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("brave bird");

        // Set initial HP
        attacker.CurrentHP = attacker.HP;

        // Act
        var messages = recoilEffect.DoEffect(attacker, defender, move);

        // Calculate expected recoil damage (33% of max HP)
        var expectedDamage = (int)(attacker.HP * 0.33);

        // Assert
        Assert.Equal(attacker.HP - expectedDamage, attacker.CurrentHP);
        Assert.Contains($"{attacker.Name} is hit with recoil!", messages);
    }
}