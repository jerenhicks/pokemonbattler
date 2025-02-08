using Xunit;
using System.Collections.Generic;

public class PokemonTemplateTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public PokemonTemplateTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestPokemonTemplateInitialization()
    {
        // Arrange
        var name = "Magikarp";
        var pokedexNumber = "" + 129;
        string typeOne = "water";
        string typeTwo = null;
        List<Generation> generations = new List<Generation>
        {
            Generation.NINE
        };
        BaseStats stats = new BaseStats(generations, 20, 10, 55, 15, 20, 80);
        List<BaseStats> baseStats = new List<BaseStats>
        {
            stats
        };

        var generation = 9;

        // Act
        var pokemonTemplate = new PokemonTemplate(name, pokedexNumber, typeOne, typeTwo, baseStats);

        // Assert
        Assert.Equal(name, pokemonTemplate.Name);
        Assert.Equal(pokedexNumber, pokemonTemplate.PokedexNumber);
        Assert.Equal(typeOne, pokemonTemplate.TypeOneName);
        Assert.Equal(typeTwo, pokemonTemplate.TypeTwoName);
        Assert.Equal(stats.BaseHP, pokemonTemplate.BaseStats[0].BaseHP);
        Assert.Equal(stats.BaseAtk, pokemonTemplate.BaseStats[0].BaseAtk);
        Assert.Equal(stats.BaseDef, pokemonTemplate.BaseStats[0].BaseDef);
        Assert.Equal(stats.BaseSpAtk, pokemonTemplate.BaseStats[0].BaseSpAtk);
        Assert.Equal(stats.BaseSpDef, pokemonTemplate.BaseStats[0].BaseSpDef);
        Assert.Equal(stats.BaseSpeed, pokemonTemplate.BaseStats[0].BaseSpeed);
        Assert.Equal(1, pokemonTemplate.Level);
    }

    [Fact]
    public void TestPokemonTemplateInitializationWithoutMoves()
    {
        // Arrange
        var name = "Magikarp";
        string pokedexNumber = "" + 129;
        string typeOne = "water";
        string typeTwo = null;
        var generation = 9;
        List<Generation> generations = new List<Generation>
        {
            Generation.NINE
        };
        BaseStats stats = new BaseStats(generations, 20, 10, 55, 15, 20, 80);
        List<BaseStats> baseStats = new List<BaseStats>
        {
            stats
        };


        // Act
        var pokemonTemplate = new PokemonTemplate(name, pokedexNumber, typeOne, typeTwo, baseStats);

        // Assert
        Assert.Equal(name, pokemonTemplate.Name);
        Assert.Equal(pokedexNumber, pokemonTemplate.PokedexNumber);
        Assert.Equal(typeOne, pokemonTemplate.TypeOneName);
        Assert.Equal(typeTwo, pokemonTemplate.TypeTwoName);
        Assert.Equal(stats.BaseHP, pokemonTemplate.BaseStats[0].BaseHP);
        Assert.Equal(stats.BaseAtk, pokemonTemplate.BaseStats[0].BaseAtk);
        Assert.Equal(stats.BaseDef, pokemonTemplate.BaseStats[0].BaseDef);
        Assert.Equal(stats.BaseSpAtk, pokemonTemplate.BaseStats[0].BaseSpAtk);
        Assert.Equal(stats.BaseSpDef, pokemonTemplate.BaseStats[0].BaseSpDef);
        Assert.Equal(stats.BaseSpeed, pokemonTemplate.BaseStats[0].BaseSpeed);
        Assert.Equal(1, pokemonTemplate.Level);
        Assert.Empty(pokemonTemplate.Moves);
    }
}