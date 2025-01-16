using Xunit;
using Xunit.Abstractions;


public class BattleTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public BattleTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestDamage()
    {
        Pokemon glaceon = PokedexRepository.CreatePokemon(471, NatureRepository.GetNature("adamant"));
        Pokemon garchomp = PokedexRepository.CreatePokemon(445, NatureRepository.GetNature("adamant"));

        glaceon.LevelUp(100);
        garchomp.LevelUp(100);

        glaceon.AddMove(MoveRepository.GetMove("ice fang"));

        Battle battle = new Battle(glaceon, garchomp);
        var result = battle.CalculateDamage(glaceon, garchomp, glaceon.Moves[0], 1);

        //first let's make sure a critical didn't happen, we turned those off.
        Assert.False(result.Item2);
        //now let's make sure the damage is within the expected range
        Assert.InRange(result.Item1, 205, 242);
    }

}
