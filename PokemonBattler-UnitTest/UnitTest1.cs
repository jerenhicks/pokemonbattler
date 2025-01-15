using Xunit;
using Xunit.Abstractions;


public class UnitTest1
{

    public UnitTest1()
    {
        // Load natures from CSV file
        NatureRepository.LoadNaturesFromFile("../../../../PokemonBattler/data/natures.csv");
        NatureRepository.LoadNaturesFromFile("../../../../PokemonBattler/data/natures.csv");
        TypeRepository.LoadTypesFromFile("../../../../PokemonBattler/data/types.csv");
        MoveRepository.LoadMovesFromFile("../../../../PokemonBattler/data/moves.csv");
        PokedexRepository.LoadPokedexFromFile("../../../../PokemonBattler/data/pokedex.csv");
    }

    [Fact]
    public void TestDamage()
    {
        Pokemon glaceon = PokedexRepository.CreatePokemon(471);
        Pokemon garchomp = PokedexRepository.CreatePokemon(445);

        glaceon.LevelUp(75);
        garchomp.LevelUp(75);

        glaceon.AddMove(MoveRepository.GetMove("ice fang"));

        Battle battle = new Battle(glaceon, garchomp);
        var result = battle.CalculateDamage(glaceon, garchomp, glaceon.Moves[0], 1);

        //first let's make sure a critical didn't happen, we turned those off.
        Assert.False(result.Item2);
        //now let's make sure the damage is within the expected range
        Assert.InRange(result.Item1, 168, 196);
    }

    [Fact]
    public void TestCalculateStats_MaxIVsMaxEVsLevel50()
    {
        Pokemon pokemon = PokedexRepository.CreatePokemon(129);
        //pokemon.IVHP = 31;
        //pokemon.IVAtk = 31;


        // Arrange
        // var pokemon = new Pokemon(
        //     name: "Magikarp",
        //     pokedexNumber: 129,
        //     typeOne: TypeRepository.GetType("Water"),
        //     typeTwo: null,
        //     nature: NatureRepository.GetNature("Adamant"),
        //     baseHP: 20,
        //     baseAtk: 10,
        //     baseDef: 55,
        //     baseSpAtk: 15,
        //     baseSpDef: 20,
        //     baseSpeed: 80,
        //     ivHP: 31,
        //     ivAtk: 31,
        //     ivDef: 31,
        //     ivSpAtk: 31,
        //     ivSpDef: 31,
        //     ivSpeed: 31,
        //     evHP: 255,
        //     evAtk: 255,
        //     evDef: 0,
        //     evSpAtk: 0,
        //     evSpDef: 0,
        //     evSpeed: 0
        // );
        pokemon.LevelUp(50);


        // Assert
        //Assert.Equal(127, pokemon.HP);
        //Assert.Equal(68, pokemon.Atk);
        //Assert.Equal(75, pokemon.Def);
        //Assert.Equal(31, pokemon.SpAtk);
        //Assert.Equal(40, pokemon.SpDef);
        //Assert.Equal(100, pokemon.Speed);
    }

    [Fact]
    public void TestCalculateStats_MaxIVsMaxEVsLevel100()
    {
        // Arrange
        Pokemon pokemon = PokedexRepository.CreatePokemon(129);

        // var pokemon = new Pokemon(
        //     name: "Magikarp",
        //     pokedexNumber: 129,
        //     typeOne: TypeRepository.GetType("Water"),
        //     typeTwo: null,
        //     nature: NatureRepository.GetNature("Adamant"),
        //     baseHP: 20,
        //     baseAtk: 10,
        //     baseDef: 55,
        //     baseSpAtk: 15,
        //     baseSpDef: 20,
        //     baseSpeed: 80,
        //     ivHP: 31,
        //     ivAtk: 31,
        //     ivDef: 31,
        //     ivSpAtk: 31,
        //     ivSpDef: 31,
        //     ivSpeed: 31,
        //     evHP: 255,
        //     evAtk: 255,
        //     evDef: 0,
        //     evSpAtk: 0,
        //     evSpDef: 0,
        //     evSpeed: 0
        // );
        pokemon.LevelUp(100);


        // Assert
        // Assert.Equal(244, pokemon.HP);
        // Assert.Equal(130, pokemon.Atk);
        // Assert.Equal(146, pokemon.Def);
        // Assert.Equal(59, pokemon.SpAtk);
        // Assert.Equal(76, pokemon.SpDef);
        // Assert.Equal(196, pokemon.Speed);
    }

    [Fact]
    public void TestCalculateStats_MinIVsMinEVsLevel50()
    {
        Pokemon pokemon = PokedexRepository.CreatePokemon(129);

        // var pokemon = new Pokemon(
        //     name: "Magikarp",
        //     pokedexNumber: 129,
        //     typeOne: TypeRepository.GetType("Water"),
        //     typeTwo: null,
        //     nature: NatureRepository.GetNature("Adamant"),
        //     baseHP: 20,
        //     baseAtk: 10,
        //     baseDef: 55,
        //     baseSpAtk: 15,
        //     baseSpDef: 20,
        //     baseSpeed: 80,
        //     ivHP: 0,
        //     ivAtk: 0,
        //     ivDef: 0,
        //     ivSpAtk: 0,
        //     ivSpDef: 0,
        //     ivSpeed: 0,
        //     evHP: 0,
        //     evAtk: 0,
        //     evDef: 0,
        //     evSpAtk: 0,
        //     evSpDef: 0,
        //     evSpeed: 0
        // );
        pokemon.LevelUp(50);


        // Assert
        Assert.Equal(80, pokemon.HP);
        Assert.Equal(16, pokemon.Atk);
        Assert.Equal(60, pokemon.Def);
        Assert.Equal(18, pokemon.SpAtk);
        Assert.Equal(25, pokemon.SpDef);
        Assert.Equal(85, pokemon.Speed);
    }

    [Fact]
    public void TestCalculateStats_MinIVsMinEVsLevel100()
    {
        // Arrange
        Pokemon pokemon = PokedexRepository.CreatePokemon(129);

        // var pokemon = new Pokemon(
        //     name: "Magikarp",
        //     pokedexNumber: 129,
        //     typeOne: TypeRepository.GetType("Water"),
        //     typeTwo: null,
        //     nature: NatureRepository.GetNature("Adamant"),
        //     baseHP: 20,
        //     baseAtk: 10,
        //     baseDef: 55,
        //     baseSpAtk: 15,
        //     baseSpDef: 20,
        //     baseSpeed: 80,
        //     ivHP: 0,
        //     ivAtk: 0,
        //     ivDef: 0,
        //     ivSpAtk: 0,
        //     ivSpDef: 0,
        //     ivSpeed: 0,
        //     evHP: 0,
        //     evAtk: 0,
        //     evDef: 0,
        //     evSpAtk: 0,
        //     evSpDef: 0,
        //     evSpeed: 0
        // );
        pokemon.LevelUp(100);


        // Assert
        Assert.Equal(150, pokemon.HP);
        Assert.Equal(27, pokemon.Atk);
        Assert.Equal(115, pokemon.Def);
        Assert.Equal(31, pokemon.SpAtk);
        Assert.Equal(45, pokemon.SpDef);
        Assert.Equal(165, pokemon.Speed);
    }
}
