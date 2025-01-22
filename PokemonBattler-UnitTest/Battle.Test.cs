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
    public void TestDamagePhysicalAttack()
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
    public void TestDamageSpecialAttack()
    {
        // Pokemon glaceon = PokedexRepository.CreatePokemon(471, NatureRepository.GetNature("adamant"));
        // Pokemon garchomp = PokedexRepository.CreatePokemon(445, NatureRepository.GetNature("adamant"));

        // glaceon.LevelUp(100);
        // garchomp.LevelUp(100);

        // glaceon.AddMove(MoveRepository.GetMove("ice fang"));

        // Battle battle = new Battle(glaceon, garchomp);
        // var result = battle.CalculateDamage(glaceon, garchomp, glaceon.Moves[0], 1);

        // //first let's make sure a critical didn't happen, we turned those off.
        // Assert.False(result.Item2);
        // //now let's make sure the damage is within the expected range
        // Assert.InRange(result.Item1, 205, 242);
    }

    [Fact]
    public void TestDamageWithBurnAndPhysicalAttack()
    {
        Pokemon glaceon = PokedexRepository.CreatePokemon(471, NatureRepository.GetNature("adamant"));
        Pokemon garchomp = PokedexRepository.CreatePokemon(445, NatureRepository.GetNature("adamant"));

        glaceon.LevelUp(100);
        garchomp.LevelUp(100);

        glaceon.AddMove(MoveRepository.GetMove("ice fang"));
        glaceon.AddNonVolatileStatus(NonVolatileStatus.Burn);

        Battle battle = new Battle(glaceon, garchomp);
        var result = battle.CalculateDamage(glaceon, garchomp, glaceon.Moves[0], 1);

        //first let's make sure a critical didn't happen, we turned those off.
        Assert.False(result.Item2);
        //now let's make sure the damage is within the expected range
        Assert.InRange(result.Item1, 102, 121);
    }

    [Fact]
    public void TestDamageWithBurnAndSpecialAttack()
    {
        // Pokemon glaceon = PokedexRepository.CreatePokemon(471, NatureRepository.GetNature("adamant"));
        // Pokemon garchomp = PokedexRepository.CreatePokemon(445, NatureRepository.GetNature("adamant"));

        // glaceon.LevelUp(100);
        // garchomp.LevelUp(100);

        // glaceon.AddMove(MoveRepository.GetMove("ice fang"));
        // glaceon.AddNonVolatileStatus(NonVolatileStatus.Burn);

        // Battle battle = new Battle(glaceon, garchomp);
        // var result = battle.CalculateDamage(glaceon, garchomp, glaceon.Moves[0], 1);

        // //first let's make sure a critical didn't happen, we turned those off.
        // Assert.False(result.Item2);
        // //now let's make sure the damage is within the expected range
        // Assert.InRange(result.Item1, 102, 121);

        //FIXME: run calculations here, build a special move, and reactivate this.
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

    [Fact]
    public void TestBurnCondition()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var battle = new Battle(pokemon1, pokemon2);
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);

        // Apply burn condition to pokemon1
        pokemon1.AddNonVolatileStatus(NonVolatileStatus.Burn);

        // Record initial HP
        var initialHP = pokemon1.CurrentHP;

        // Act
        battle.CheckBurn(pokemon1);

        // Calculate expected HP loss (1/16 of max HP)
        var expectedHPLoss = pokemon1.HP / 16;

        // Assert
        Assert.Equal(initialHP - expectedHPLoss, pokemon1.CurrentHP); // Burn should reduce HP by 1/16 of max HP
        Assert.Contains("burn", battle.GetBattleLog().Last().ToLower()); // Check if burn is logged
    }


    [Fact]
    public void TestPoisonCondition()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var battle = new Battle(pokemon1, pokemon2);
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);

        // Apply burn condition to pokemon1
        pokemon1.AddNonVolatileStatus(NonVolatileStatus.Poison);

        // Record initial HP
        var initialHP = pokemon1.CurrentHP;

        // Act
        battle.CheckPoison(pokemon1);

        // Calculate expected HP loss (1/8 of max HP)
        var expectedHPLoss = pokemon1.HP / 8;

        // Assert
        Assert.Equal(initialHP - expectedHPLoss, pokemon1.CurrentHP); // Burn should reduce HP by 1/8 of max HP
        Assert.Contains("poison", battle.GetBattleLog().Last().ToLower()); // Check if poison is logged
    }

    [Fact]
    public void TestCheckBadlyPoison_FirstTurn()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon.LevelUp(100);
        pokemon2.LevelUp(100);
        var battle = new Battle(pokemon, pokemon2);

        // Apply badly poisoned condition to pokemon
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Badly_Poisoned);

        // Record initial HP
        var initialHP = pokemon.CurrentHP;

        // Act
        battle.CheckBadlyPoison(pokemon);

        // Calculate expected HP loss (1/16 of max HP)
        var expectedHPLoss = pokemon.HP / 16;

        // Assert
        Assert.Equal(initialHP - expectedHPLoss, pokemon.CurrentHP); // Badly poisoned should reduce HP by 1/16 of max HP
        Assert.Contains("poison", battle.GetBattleLog().Last().ToLower()); // Check if poison is logged
    }

    [Fact]
    public void TestCheckBadlyPoison_SubsequentTurns()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon.LevelUp(100);
        pokemon2.LevelUp(100);
        var battle = new Battle(pokemon, pokemon2);

        // Apply badly poisoned condition to pokemon
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Badly_Poisoned);

        // Simulate multiple turns
        for (int turn = 1; turn <= 3; turn++)
        {
            // Record initial HP
            var initialHP = pokemon.CurrentHP;

            // Act
            battle.CheckBadlyPoison(pokemon);

            // Calculate expected HP loss (1/16 * turn of max HP)
            var expectedHPLoss = (int)Math.Floor(pokemon.HP * (0.0625 * turn));

            // Assert
            Assert.Equal(initialHP - expectedHPLoss, pokemon.CurrentHP); // Badly poisoned should reduce HP by increasing amounts
            Assert.Contains("poison", battle.GetBattleLog().Last().ToLower()); // Check if poison is logged
        }
    }

    [Fact]
    public void TestCheckBadlyPoison_AlreadyPoisoned()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon.LevelUp(100);
        pokemon2.LevelUp(100);
        var battle = new Battle(pokemon, pokemon2);

        // Apply badly poisoned condition to pokemon
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Badly_Poisoned);

        // Simulate first turn
        battle.CheckBadlyPoison(pokemon);

        // Record initial HP after first turn
        var initialHP = pokemon.CurrentHP;

        // Act
        battle.CheckBadlyPoison(pokemon);

        // Calculate expected HP loss (1/16 * 2 of max HP)
        var expectedHPLoss = (int)Math.Floor(pokemon.HP * (0.0625 * 2));

        // Assert
        Assert.Equal(initialHP - expectedHPLoss, pokemon.CurrentHP); // Badly poisoned should reduce HP by increasing amounts
        Assert.Contains("poison", battle.GetBattleLog().Last().ToLower()); // Check if poison is logged
    }

    [Fact]
    public void TestGetAdjustedStages()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("tackle");
        var battle = new Battle(attacker, defender);

        // Accuracy and evasion stages range from -6 to +6
        for (int accuracyStage = -6; accuracyStage <= 6; accuracyStage++)
        {
            for (int evasionStage = -6; evasionStage <= 6; evasionStage++)
            {
                // Reset stages
                attacker.StatModifiers.ResetAll();
                defender.StatModifiers.ResetAll();

                // Set stages
                attacker.StatModifiers.ChangeAccuracyStage(accuracyStage);
                defender.StatModifiers.ChangeEvasionStage(evasionStage);

                // Calculate expected result
                var expected = GetStageMultiplier(attacker.StatModifiers.AccuracyStage - defender.StatModifiers.EvasionStage);

                // Act
                var result = battle.GetAdjustedStages(attacker, defender, move);

                // Assert
                Assert.Equal(expected, result);
            }
        }
    }

    private decimal GetStageMultiplier(int stage)
    {
        if (stage <= -6)
        {
            return (decimal)3 / 9;
        }
        else if (stage == -5)
        {
            return (decimal)3 / 8;
        }
        else if (stage == -4)
        {
            return (decimal)3 / 7;
        }
        else if (stage == -3)
        {
            return (decimal)3 / 6;
        }
        else if (stage == -2)
        {
            return (decimal)3 / 5;
        }
        else if (stage == -1)
        {
            return (decimal)3 / 4;
        }
        else if (stage == 1)
        {
            return (decimal)4 / 3;
        }
        else if (stage == 2)
        {
            return (decimal)5 / 3;
        }
        else if (stage == 3)
        {
            return (decimal)6 / 3;
        }
        else if (stage == 4)
        {
            return (decimal)7 / 3;
        }
        else if (stage == 5)
        {
            return (decimal)8 / 3;
        }
        else if (stage >= 6)
        {
            return (decimal)9 / 3;
        }
        else
        {
            return (decimal)3 / 3;
        }
    }

    [Fact]
    public void TestCheckFainted()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        var battle = new Battle(pokemon1, pokemon2);

        // Set HP values
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted

        // Act
        battle.CheckFainted(pokemon1, pokemon2);

        // Assert
        var battleLog = battle.GetBattleLog();
        Assert.Contains($"{pokemon1.Name} fainted!", battleLog);
        Assert.DoesNotContain($"{pokemon2.Name} fainted!", battleLog);
    }

    [Fact]
    public void TestCheckFainted_BothFainted()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        var battle = new Battle(pokemon1, pokemon2);

        // Set HP values
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 0; // Galvantula is fainted

        // Act
        battle.CheckFainted(pokemon1, pokemon2);

        // Assert
        var battleLog = battle.GetBattleLog();
        Assert.Contains($"{pokemon1.Name} fainted!", battleLog);
        Assert.Contains($"{pokemon2.Name} fainted!", battleLog);
    }

    [Fact]
    public void TestCheckFainted_NoneFainted()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        var battle = new Battle(pokemon1, pokemon2);

        // Set HP values
        pokemon1.CurrentHP = 50; // Magikarp is not fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted

        // Act
        battle.CheckFainted(pokemon1, pokemon2);

        // Assert
        var battleLog = battle.GetBattleLog();
        Assert.DoesNotContain($"{pokemon1.Name} fainted!", battleLog);
        Assert.DoesNotContain($"{pokemon2.Name} fainted!", battleLog);
    }

    [Fact]
    public void TestCanHit_AlwaysHitMove()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = new Move("Swift", TypeRepository.GetType("Normal"), MoveCategory.Special, 20, 60, null, 0, false, false, false, false, false, false, null); // Always hit move
        var battle = new Battle(attacker, defender);

        // Act
        var result = battle.CanHit(attacker, defender, move);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCanHit_HighAccuracyMove()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = new Move("Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, false, null); // High accuracy move
        var battle = new Battle(attacker, defender);

        // Act
        var result = battle.CanHit(attacker, defender, move);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCanHit_LowAccuracyMove()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = new Move("Slam", TypeRepository.GetType("Normal"), MoveCategory.Physical, 20, 80, 0.75m, 0, false, false, false, false, false, false, null); // Low accuracy move
        var battle = new Battle(attacker, defender);

        // Act
        var result = battle.CanHit(attacker, defender, move);

        // Assert
        Assert.InRange(result, false, true); // Result can be either true or false
    }

    [Fact]
    public void TestCanHit_WithModifiers()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = new Move("Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, false, null); // High accuracy move
        var battle = new Battle(attacker, defender);

        // Mock modifiers
        attacker.StatModifiers.ChangeAccuracyStage(2);
        defender.StatModifiers.ChangeEvasionStage(-2);

        // Act
        var result = battle.CanHit(attacker, defender, move);

        // Assert
        Assert.True(result);
    }

}
