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
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");



        // Act
        var returnVals = burnEffect.PostDamageEffect(attacker, defender, move);

        // Assert
        Assert.True(returnVals.Count == 0);
    }
}
