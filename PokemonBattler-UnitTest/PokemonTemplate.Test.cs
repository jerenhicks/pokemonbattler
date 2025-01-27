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
        var pokedexNumber = 129;
        var typeOne = TypeRepository.GetType("Water");
        Type typeTwo = null;
        var baseHP = 20;
        var baseAtk = 10;
        var baseDef = 55;
        var baseSpAtk = 15;
        var baseSpDef = 20;
        var baseSpeed = 80;
        var moves = new List<Move>
        {
            new Move(150,"Splash", TypeRepository.GetType("Normal"), MoveCategory.Status, 40, null, null, 0, false, false, false, false, false, false, Range.Normal, null),
            new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, false, Range.Normal, null)
        };

        // Act
        var pokemonTemplate = new PokemonTemplate(name, pokedexNumber, typeOne, typeTwo, baseHP, baseAtk, baseDef, baseSpAtk, baseSpDef, baseSpeed, moves);

        // Assert
        Assert.Equal(name, pokemonTemplate.Name);
        Assert.Equal(pokedexNumber, pokemonTemplate.PokedexNumber);
        Assert.Equal(typeOne, pokemonTemplate.TypeOne);
        Assert.Equal(typeTwo, pokemonTemplate.TypeTwo);
        Assert.Equal(baseHP, pokemonTemplate.BaseHP);
        Assert.Equal(baseAtk, pokemonTemplate.BaseAtk);
        Assert.Equal(baseDef, pokemonTemplate.BaseDef);
        Assert.Equal(baseSpAtk, pokemonTemplate.BaseSpAtk);
        Assert.Equal(baseSpDef, pokemonTemplate.BaseSpDef);
        Assert.Equal(baseSpeed, pokemonTemplate.BaseSpeed);
        Assert.Equal(1, pokemonTemplate.Level);
        Assert.Equal(moves, pokemonTemplate.Moves);
    }

    [Fact]
    public void TestPokemonTemplateInitializationWithoutMoves()
    {
        // Arrange
        var name = "Magikarp";
        var pokedexNumber = 129;
        var typeOne = TypeRepository.GetType("Water");
        Type typeTwo = null;
        var baseHP = 20;
        var baseAtk = 10;
        var baseDef = 55;
        var baseSpAtk = 15;
        var baseSpDef = 20;
        var baseSpeed = 80;

        // Act
        var pokemonTemplate = new PokemonTemplate(name, pokedexNumber, typeOne, typeTwo, baseHP, baseAtk, baseDef, baseSpAtk, baseSpDef, baseSpeed);

        // Assert
        Assert.Equal(name, pokemonTemplate.Name);
        Assert.Equal(pokedexNumber, pokemonTemplate.PokedexNumber);
        Assert.Equal(typeOne, pokemonTemplate.TypeOne);
        Assert.Equal(typeTwo, pokemonTemplate.TypeTwo);
        Assert.Equal(baseHP, pokemonTemplate.BaseHP);
        Assert.Equal(baseAtk, pokemonTemplate.BaseAtk);
        Assert.Equal(baseDef, pokemonTemplate.BaseDef);
        Assert.Equal(baseSpAtk, pokemonTemplate.BaseSpAtk);
        Assert.Equal(baseSpDef, pokemonTemplate.BaseSpDef);
        Assert.Equal(baseSpeed, pokemonTemplate.BaseSpeed);
        Assert.Equal(1, pokemonTemplate.Level);
        Assert.Empty(pokemonTemplate.Moves);
    }
}