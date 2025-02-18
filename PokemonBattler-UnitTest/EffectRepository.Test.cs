using System;
using System.Collections.Generic;
using Xunit;

public class EffectRepositoryTests
{
    [Fact]
    public void AddEffect_AddsEffectToRepository()
    {
        // Arrange
        var effect = new MockEffect();
        EffectRepository.ClearEffects(); // Clear existing effects for a clean test

        // Act
        EffectRepository.AddEffect(effect);
        var result = EffectRepository.GetEffect("MockEffect");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MockEffect>(result);
    }

    [Fact]
    public void GetEffect_ReturnsNullIfEffectNotFound()
    {
        // Arrange
        EffectRepository.ClearEffects(); // Clear existing effects for a clean test

        // Act
        var result = EffectRepository.GetEffect("NonExistentEffect");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void LoadEffectsFromAssembly_LoadsAllEffects()
    {
        // Arrange
        EffectRepository.ClearEffects(); // Clear existing effects for a clean test

        // Act
        EffectRepository.LoadEffectsFromAssembly();
        var effect = new MockEffect();
        EffectRepository.AddEffect(effect);
        var result = EffectRepository.GetEffect("MockEffect");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MockEffect>(result);
    }
}

// Mock effect class for testing
public class MockEffect : BaseEffect
{
    // Implement necessary members of BaseEffect
    public override void SetModifier(double amount)
    {
        // No modifier needed for mock effect
    }

    public override List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }

    public override List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone)
    {
        // Implement the effect logic here
        return new List<String>();
    }

    public override void SetChance(double chance)
    {
        // Implement the SetChance logic here
    }

    public override void SetRandom(Random random)
    {
        // Implement the SetRandom logic here
    }
}

