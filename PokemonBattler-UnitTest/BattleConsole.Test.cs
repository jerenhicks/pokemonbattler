using Xunit;
using System.IO;
using System;

public class BattleConsoleTest : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public BattleConsoleTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    // [Fact]
    // public void TestLoadData()
    // {
    //     // Arrange
    //     var battleConsole = new BattleConsole(1);

    //     // Act
    //     battleConsole.LoadData(true);

    //     // Assert
    //     Assert.NotEmpty(EffectRepository.GetAllEffects());
    //     Assert.NotEmpty(NatureRepository.GetAllNatures());
    //     Assert.NotEmpty(TypeRepository.GetAllTypes());
    //     Assert.NotEmpty(MoveRepository.GetAllMoves());
    //     Assert.NotEmpty(PokedexRepository.GetAllPokemonTemplates());
    // }
}