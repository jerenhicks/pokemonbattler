using Xunit;
using Xunit.Abstractions;


public class PokemonTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public PokemonTest(TestFixture fixture)
    {
        _fixture = fixture;
    }



    [Fact]
    public void TestCalculateStats_MaxIVsMaxEVsLevel50()
    {
        Pokemon pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 31, 31, 31, 31, 31, 31, 252, 252, 0, 0, 0, 0);

        pokemon.LevelUp(50);

        Assert.Equal(127, pokemon.HP);
        Assert.Equal(68, pokemon.Atk);
        Assert.Equal(75, pokemon.Def);
        Assert.Equal(31, pokemon.SpAtk);
        Assert.Equal(40, pokemon.SpDef);
        Assert.Equal(100, pokemon.Speed);
    }

    [Fact]
    public void TestCalculateStats_MaxIVsMaxEVsLevel100()
    {
        Pokemon pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 31, 31, 31, 31, 31, 31, 252, 252, 0, 0, 0, 0);

        pokemon.LevelUp(100);

        Assert.Equal(244, pokemon.HP);
        Assert.Equal(130, pokemon.Atk);
        Assert.Equal(146, pokemon.Def);
        Assert.Equal(59, pokemon.SpAtk);
        Assert.Equal(76, pokemon.SpDef);
        Assert.Equal(196, pokemon.Speed);
    }

    [Fact]
    public void TestCalculateStats_MinIVsMinEVsLevel50()
    {
        Pokemon pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        pokemon.LevelUp(50);

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
        Pokemon pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        pokemon.LevelUp(100);

        Assert.Equal(150, pokemon.HP);
        Assert.Equal(27, pokemon.Atk);
        Assert.Equal(115, pokemon.Def);
        Assert.Equal(31, pokemon.SpAtk);
        Assert.Equal(45, pokemon.SpDef);
        Assert.Equal(165, pokemon.Speed);
    }

    [Fact]
    public void TestCreatePokemon_ExceedMaxEVs_ThrowsException()
    {
        // Check if an exception is thrown for each EV stat exceeding 252
        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 253, 0, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 253, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 0, 253, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 253, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 253, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 253);
        });
    }

    [Fact]
    public void TestCreatePokemon_ExceedMaxIVs_ThrowsException()
    {
        // Check if an exception is thrown for each IV stat exceeding 31
        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0);
        });

    }

    [Fact]
    public void TestCreatePokemon_ExceedTotalEVs_ThrowsException()
    {
        // Check if an exception is thrown when total EVs exceed 510
        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 252, 252, 252, 252, 252, 252);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"), 0, 0, 0, 0, 0, 0, 100, 100, 100, 100, 100, 11);
        });
    }

    [Fact]
    public void TestAddMovesToMagikarp()
    {
        // Create two Magikarps and set their levels to 100
        Pokemon magikarp1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"));
        Pokemon magikarp2 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"));

        magikarp1.LevelUp(100);
        magikarp2.LevelUp(100);

        // Add 4 moves to Magikarp 2
        magikarp2.AddMove(MoveRepository.GetMove("pound"));
        magikarp2.AddMove(MoveRepository.GetMove("tackle"));
        magikarp2.AddMove(MoveRepository.GetMove("slam"));
        magikarp2.AddMove(MoveRepository.GetMove("ice punch"));

        // Check that Magikarp 1 has 0 moves and Magikarp 2 has 4 moves
        Assert.Equal(0, magikarp1.Moves.Count);
        Assert.Equal(4, magikarp2.Moves.Count);

        magikarp1.AddMove(MoveRepository.GetMove("pound"));

        Assert.Equal(1, magikarp1.Moves.Count);
        Assert.Equal(4, magikarp2.Moves.Count);
    }
}
