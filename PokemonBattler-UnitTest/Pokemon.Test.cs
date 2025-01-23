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

    [Fact]
    public void TestAddNonVolatileStatus_WhenNone_ShouldAddStatus()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var status = NonVolatileStatus.Burn;

        // Act
        var result = pokemon.AddNonVolatileStatus(status);

        // Assert
        Assert.True(result);
        Assert.Equal(status, pokemon.NonVolatileStatus);
    }

    [Fact]
    public void TestAddNonVolatileStatus_WhenAlreadyHasStatus_ShouldNotAddStatus()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Burn);
        var newStatus = NonVolatileStatus.Paralysis;

        // Act
        var result = pokemon.AddNonVolatileStatus(newStatus);

        // Assert
        Assert.False(result);
        Assert.Equal(NonVolatileStatus.Burn, pokemon.NonVolatileStatus);
    }

    [Fact]
    public void TestAddNonVolatileStatus_WhenAlreadyHasSameStatus_ShouldNotChangeStatus()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Burn);

        // Act
        var result = pokemon.AddNonVolatileStatus(NonVolatileStatus.Burn);

        // Assert
        Assert.False(result);
        Assert.Equal(NonVolatileStatus.Burn, pokemon.NonVolatileStatus);
    }

    [Fact]
    public void TestAddNonVolatileStatus_WhenNone_ShouldAddDifferentStatus()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var status = NonVolatileStatus.Paralysis;

        // Act
        var result = pokemon.AddNonVolatileStatus(status);

        // Assert
        Assert.True(result);
        Assert.Equal(status, pokemon.NonVolatileStatus);
    }

    [Fact]
    public void TestCurrentStatsWithModifiers()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        pokemon.LevelUp(50);
        pokemon.Reset();

        // Act
        pokemon.StatModifiers.ChangeAtkStage(2);
        pokemon.StatModifiers.ChangeDefStage(-1);
        pokemon.StatModifiers.ChangeSpAtkStage(3);
        pokemon.StatModifiers.ChangeSpDefStage(-2);
        pokemon.StatModifiers.ChangeSpeedStage(1);

        // Assert
        Assert.Equal((int)(pokemon.Atk * pokemon.StatModifiers.GetAtkModifier()), pokemon.CurrentAtk);
        Assert.Equal((int)(pokemon.Def * pokemon.StatModifiers.GetDefModifier()), pokemon.CurrentDef);
        Assert.Equal((int)(pokemon.SpAtk * pokemon.StatModifiers.GetSpAtkModifier()), pokemon.CurrentSpAtk);
        Assert.Equal((int)(pokemon.SpDef * pokemon.StatModifiers.GetSpDefModifier()), pokemon.CurrentSpDef);
        Assert.Equal((int)(pokemon.Speed * pokemon.StatModifiers.GetSpeedModifier()), pokemon.CurrentSpeed);
    }

    [Fact]
    public void TestCurrentStatsWithoutModifiers()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        pokemon.LevelUp(50);
        pokemon.Reset();

        // Assert
        Assert.Equal(pokemon.Atk, pokemon.CurrentAtk);
        Assert.Equal(pokemon.Def, pokemon.CurrentDef);
        Assert.Equal(pokemon.SpAtk, pokemon.CurrentSpAtk);
        Assert.Equal(pokemon.SpDef, pokemon.CurrentSpDef);
        Assert.Equal(pokemon.Speed, pokemon.CurrentSpeed);
    }

    [Fact]
    public void TestResetModifiers()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        pokemon.LevelUp(50);
        pokemon.Reset();

        // Act
        pokemon.StatModifiers.ChangeAtkStage(2);
        pokemon.StatModifiers.ChangeDefStage(-1);
        pokemon.StatModifiers.ChangeSpAtkStage(3);
        pokemon.StatModifiers.ChangeSpDefStage(-2);
        pokemon.StatModifiers.ChangeSpeedStage(1);
        pokemon.Reset(); // Reset modifiers

        // Assert
        Assert.Equal(pokemon.Atk, pokemon.CurrentAtk);
        Assert.Equal(pokemon.Def, pokemon.CurrentDef);
        Assert.Equal(pokemon.SpAtk, pokemon.CurrentSpAtk);
        Assert.Equal(pokemon.SpDef, pokemon.CurrentSpDef);
        Assert.Equal(pokemon.Speed, pokemon.CurrentSpeed);
    }
}
