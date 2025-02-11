using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AbsorbEffectTests : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public AbsorbEffectTests(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void SetModifier_SetsCorrectValue()
    {
        // Arrange
        var absorbEffect = new AbsorbEffect();
        double modifier = 0.5;

        // Act
        absorbEffect.SetModifier(modifier);

        // Assert
        Assert.Equal(modifier, absorbEffect.Modifier);
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var absorbEffect = new AbsorbEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("absorb");

        // Act
        var result = absorbEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void PostDamageEffect_ReducesDefenderHPAndReturnsMessage()
    {
        // Arrange
        var absorbEffect = new AbsorbEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        attacker.LevelUp(100);
        attacker.CurrentHP = attacker.HP - 40;
        var assumingEndHP = attacker.HP - 20;
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        defender.LevelUp(100);

        var move = MoveRepository.GetMove("absorb");
        int damageDone = 40;
        absorbEffect.SetModifier(0.5);

        // Act
        var result = absorbEffect.PostDamageEffect(attacker, defender, move, damageDone);

        // Assert
        Assert.Equal(assumingEndHP, attacker.CurrentHP);
        Assert.Single(result);
        Assert.Equal("Galvantula has had it's energy drained!", result[0]);
    }
}
