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

    [Fact]
    public void TestPokemonDecideMove_NoMovesLeft_ShouldUseStruggle()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant"));
        pokemon.LevelUp(100);

        var battle = new Battle(pokemon, pokemon2);

        // Act
        var move = battle.PokemonDecideMove(pokemon);

        // Assert
        Assert.Equal("struggle", move.Name.ToLower());
    }

    [Fact]
    public void TestPokemonDecideMove_AllMovesOutOfPP_ShouldUseStruggle()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant"));

        var battle = new Battle(pokemon, pokemon2);

        pokemon.LevelUp(100);
        pokemon.AddMove(MoveRepository.GetMove("pound"));
        pokemon.AddMove(MoveRepository.GetMove("tackle"));
        pokemon.AddMove(MoveRepository.GetMove("slam"));
        pokemon.AddMove(MoveRepository.GetMove("ice punch"));

        // Set all moves' PP to 0
        foreach (var move in pokemon.Moves)
        {
            while (move.PP > 0)
            {
                move.MoveUsed();
            }
        }

        // Act
        var moveToUse = battle.PokemonDecideMove(pokemon);

        // Assert
        Assert.Equal("struggle", moveToUse.Name.ToLower());
    }

    [Fact]
    public void TestPokemonDecideMove_HasMovesWithPP_ShouldUseOneOfThem()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant"));

        var battle = new Battle(pokemon, pokemon2);

        pokemon.LevelUp(100);
        pokemon.AddMove(MoveRepository.GetMove("pound"));
        pokemon.AddMove(MoveRepository.GetMove("tackle"));
        pokemon.AddMove(MoveRepository.GetMove("slam"));
        pokemon.AddMove(MoveRepository.GetMove("ice punch"));

        // Act
        var moveToUse = battle.PokemonDecideMove(pokemon);

        // Assert
        Assert.Contains(moveToUse, pokemon.Moves);
    }

    [Fact]
    public void TestPokemonDecideMove_SomeMovesOutOfPP_ShouldUseMoveWithPP()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant"));

        var battle = new Battle(pokemon, pokemon2);

        pokemon.LevelUp(100);
        pokemon.AddMove(MoveRepository.GetMove("pound"));
        pokemon.AddMove(MoveRepository.GetMove("tackle"));
        pokemon.AddMove(MoveRepository.GetMove("slam"));
        pokemon.AddMove(MoveRepository.GetMove("ice punch"));

        // Set some moves' PP to 0
        pokemon.Moves[0].MoveUsed();
        pokemon.Moves[0].MoveUsed();
        pokemon.Moves[0].MoveUsed();
        pokemon.Moves[0].MoveUsed();
        pokemon.Moves[0].MoveUsed();

        // Act
        var moveToUse = battle.PokemonDecideMove(pokemon);

        // Assert
        Assert.NotEqual("struggle", moveToUse.Name.ToLower());
        Assert.Contains(moveToUse, pokemon.Moves.FindAll(move => move.PP > 0));
    }

    [Fact]
    public void TestDecideWhoGoesFirst_MovePriority()
    {
        // Arrange

        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula

        var battle = new Battle(pokemon1, pokemon2);

        var move1 = MoveRepository.GetMove("quick attack");
        var move2 = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.DecideWhoGoesFirst(pokemon1, pokemon2, move1, move2);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void TestDecideWhoGoesFirst_SamePriority_DifferentSpeed()
    {
        // Arrange

        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula

        var battle = new Battle(pokemon1, pokemon2);

        pokemon1.LevelUp(50);
        pokemon2.LevelUp(50);
        var move1 = MoveRepository.GetMove("tackle");
        var move2 = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.DecideWhoGoesFirst(pokemon1, pokemon2, move1, move2);

        // Assert
        Assert.Equal(2, result); // Assuming Galvantula has higher speed than Magikarp
    }

    [Fact]
    public void TestDecideWhoGoesFirst_SamePriority_SameSpeed()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Another Magikarp
        var battle = new Battle(pokemon1, pokemon2);

        pokemon1.LevelUp(50);
        pokemon2.LevelUp(50);
        var move1 = MoveRepository.GetMove("tackle");
        var move2 = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.DecideWhoGoesFirst(pokemon1, pokemon2, move1, move2);

        // Assert
        Assert.True(result == 1 || result == 2); // Random decision
    }

    [Fact]
    public void TestGetAccuracyModifiers()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var battle = new Battle(attacker, defender);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.GetAccuracyModifiers(attacker, defender, move);

        // Assert
        Assert.Equal(1, result); // Assuming the default implementation returns 1
    }

    [Fact]
    public void TestGetAdjustedStages()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var battle = new Battle(attacker, defender);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.GetAdjustedStages(attacker, defender, move);

        // Assert
        Assert.Equal(1, result); // Assuming the default implementation returns 1
    }

    [Fact]
    public void TestGetMiracleBerry()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var battle = new Battle(attacker, defender);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.GetMiracleBerry(attacker, defender, move);

        // Assert
        Assert.Equal(1, result); // Assuming the default implementation returns 1
    }

    [Fact]
    public void TestGetAffection()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var battle = new Battle(attacker, defender);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battle.GetAffection(attacker, defender, move);

        // Assert
        Assert.Equal(0, result); // Assuming the default implementation returns 0
    }

}
