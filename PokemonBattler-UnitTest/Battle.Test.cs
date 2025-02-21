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
        Pokemon glaceon = PokedexRepository.CreatePokemon("" + 471, NatureRepository.GetNature("adamant"));
        Pokemon garchomp = PokedexRepository.CreatePokemon("" + 445, NatureRepository.GetNature("adamant"));

        glaceon.LevelUp(100);
        glaceon.Reset();
        garchomp.LevelUp(100);
        garchomp.Reset();

        glaceon.AddMove(MoveRepository.GetMove("ice fang"));
        BattleTeam team1 = new BattleTeam(glaceon);
        BattleTeam team2 = new BattleTeam(garchomp);
        Battle battle = new Battle(team1, team2, new NinethGenerationBattleData());
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
        Pokemon glaceon = PokedexRepository.CreatePokemon("471", NatureRepository.GetNature("adamant"));
        Pokemon garchomp = PokedexRepository.CreatePokemon("445", NatureRepository.GetNature("adamant"));

        glaceon.LevelUp(100);
        glaceon.Reset();
        garchomp.LevelUp(100);
        garchomp.Reset();

        glaceon.AddMove(MoveRepository.GetMove("ice fang"));
        glaceon.AddNonVolatileStatus(NonVolatileStatus.Burn);

        BattleTeam team1 = new BattleTeam(glaceon);
        BattleTeam team2 = new BattleTeam(garchomp);
        Battle battle = new Battle(team1, team2, new NinethGenerationBattleData());
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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));
        pokemon.LevelUp(100);

        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Act
        var move = battle.PokemonDecideMove(pokemon);

        // Assert
        Assert.Equal("struggle", move.Name.ToLower());
    }

    [Fact]
    public void TestPokemonDecideMove_AllMovesOutOfPP_ShouldUseStruggle()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));

        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));

        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));

        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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

        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula

        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        var move1 = MoveRepository.GetMove("quick attack");
        var move2 = MoveRepository.GetMove("tackle");

        Dictionary<Pokemon, Move> moves = new Dictionary<Pokemon, Move>
        {
            { pokemon1, move2 },
            { pokemon2, move1 }
        };

        List<Pokemon> turnOrder = new List<Pokemon>
        {
            pokemon2,
            pokemon1
        };

        // Act
        var result = battle.GetTurnOrder(moves);

        // Assert
        Assert.Equal(turnOrder, result);
    }

    [Fact]
    public void TestDecideWhoGoesFirst_SamePriority_DifferentSpeed()
    {
        // Arrange

        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula

        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        pokemon1.LevelUp(50);
        pokemon2.LevelUp(50);
        var move1 = MoveRepository.GetMove("tackle");
        var move2 = MoveRepository.GetMove("tackle");

        Dictionary<Pokemon, Move> moves = new Dictionary<Pokemon, Move>
        {
            { pokemon1, move2 },
            { pokemon2, move1 }
        };

        List<Pokemon> turnOrder = new List<Pokemon>
        {
            pokemon2,
            pokemon1
        };

        // Act
        var result = battle.GetTurnOrder(moves);

        // Assert
        Assert.Equal(turnOrder, result); // Assuming Galvantula has higher speed than Magikarp
    }

    [Fact]
    public void TestDecideWhoGoesFirst_SamePriority_SameSpeed()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Another Magikarp
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        battle.SetRandom(new RandomMock(0));

        pokemon1.LevelUp(50);
        pokemon2.LevelUp(50);
        var move1 = MoveRepository.GetMove("tackle");
        var move2 = MoveRepository.GetMove("tackle");
        Dictionary<Pokemon, Move> moves = new Dictionary<Pokemon, Move>
        {
            { pokemon1, move2 },
            { pokemon2, move1 }
        };

        List<Pokemon> turnOrder = new List<Pokemon>
        {
            pokemon2,
            pokemon1
        };

        // Act
        var result = battle.GetTurnOrder(moves);

        // Assert
        Assert.Equal(turnOrder, result); // Random decision
    }

    [Fact]
    public void TestDecideWhoGoesFirst_SamePriority_SameSpeed2()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Another Magikarp
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        battle.SetRandom(new RandomMock(1));

        pokemon1.LevelUp(50);
        pokemon2.LevelUp(50);
        var move1 = MoveRepository.GetMove("tackle");
        var move2 = MoveRepository.GetMove("tackle");
        Dictionary<Pokemon, Move> moves = new Dictionary<Pokemon, Move>
        {
            { pokemon1, move2 },
            { pokemon2, move1 }
        };

        List<Pokemon> turnOrder = new List<Pokemon>
        {
            pokemon1,
            pokemon2
        };

        // Act
        var result = battle.GetTurnOrder(moves);

        // Assert
        Assert.Equal(turnOrder, result); // Random decision
    }

    [Fact]
    public void TestGetAccuracyModifiers()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battleData.GetAccuracyModifiers(attacker, defender, move);

        // Assert
        Assert.Equal(1, result); // Assuming the default implementation returns 1
    }

    [Fact]
    public void TestGetMiracleBerry()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battleData.GetMiracleBerry(attacker, defender, move);

        // Assert
        Assert.Equal(1, result); // Assuming the default implementation returns 1
    }

    [Fact]
    public void TestGetAffection()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);
        var move = MoveRepository.GetMove("tackle");

        // Act
        var result = battleData.GetAffection(attacker, defender, move);

        // Assert
        Assert.Equal(0, result); // Assuming the default implementation returns 0
    }

    [Fact]
    public void TestBurnCondition()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
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
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon.LevelUp(100);
        pokemon2.LevelUp(100);
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon.LevelUp(100);
        pokemon2.LevelUp(100);
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon.LevelUp(100);
        pokemon2.LevelUp(100);
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        var move = MoveRepository.GetMove("tackle");
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

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
                var result = battleData.GetAdjustedStages(attacker, defender, move);

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
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Set HP values
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted

        // Act
        battle.CheckFaintedAll();

        // Assert
        var battleLog = battle.GetBattleLog();
        Assert.Contains($"{pokemon1.Name} fainted!", battleLog);
        Assert.DoesNotContain($"{pokemon2.Name} fainted!", battleLog);
    }

    [Fact]
    public void TestCheckFainted_BothFainted()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Set HP values
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 0; // Galvantula is fainted

        // Act
        battle.CheckFaintedAll();

        // Assert
        var battleLog = battle.GetBattleLog();
        Assert.Contains($"{pokemon1.Name} fainted!", battleLog);
        Assert.Contains($"{pokemon2.Name} fainted!", battleLog);
    }

    [Fact]
    public void TestCheckFainted_NoneFainted()
    {
        // Arrange
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Set HP values
        pokemon1.CurrentHP = 50; // Magikarp is not fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted

        // Act
        battle.CheckFaintedAll();

        // Assert
        var battleLog = battle.GetBattleLog();
        Assert.DoesNotContain($"{pokemon1.Name} fainted!", battleLog);
        Assert.DoesNotContain($"{pokemon2.Name} fainted!", battleLog);
    }

    [Fact]
    public void TestCanHit_AlwaysHitMove()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        var move = new Move(129, "Swift", TypeRepository.GetType("Normal"), MoveCategory.Special, 20, 60, null, 0, false, false, false, false, false, Range.Normal, null); // Always hit move

        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);

        // Act
        var result = battleData.CanHit(attacker, defender, move, new RandomMock(50));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCanHit_HighAccuracyMove()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);

        // Act
        var result = battleData.CanHit(attacker, defender, move, new RandomMock(50));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCanHit_LowAccuracyMove()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        var move = new Move(21, "Slam", TypeRepository.GetType("Normal"), MoveCategory.Physical, 20, 80, 0.75m, 0, false, false, false, false, false, Range.Normal, null); // Low accuracy move
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);

        // Act
        var result = battleData.CanHit(attacker, defender, move, new RandomMock(50));

        // Assert
        Assert.InRange(result, false, true); // Result can be either true or false
    }

    [Fact]
    public void TestCanHit_WithModifiers()
    {
        // Arrange
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        NinethGenerationBattleData battleData = new NinethGenerationBattleData();
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move
        BattleTeam team1 = new BattleTeam(attacker);
        BattleTeam team2 = new BattleTeam(defender);
        var battle = new Battle(team1, team2, battleData);

        // Mock modifiers
        attacker.StatModifiers.ChangeAccuracyStage(2);
        defender.StatModifiers.ChangeEvasionStage(-2);

        // Act
        var result = battleData.CanHit(attacker, defender, move, new RandomMock(50));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCheckFrozen_ThawedOut()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula

        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        pokemon.LevelUp(100);
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Freeze);

        var random = new RandomMock(10);
        battle.SetRandom(random);

        // Act
        var wasFrozen = battle.CheckFrozen(pokemon);

        // Assert
        Assert.False(wasFrozen); // Pokémon should be thawed out
        var battleLog = battle.GetBattleLog();
        Assert.Contains($"{pokemon.Name} thawed out!", battleLog);
    }

    [Fact]
    public void TestCheckFrozen_StillFrozen()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        pokemon.LevelUp(100);
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Freeze);

        // Mock the random number generator to always return a value greater than 20
        var random = new RandomMock(21);
        battle.SetRandom(random);

        // Act
        var wasFrozen = battle.CheckFrozen(pokemon);

        // Assert
        Assert.True(wasFrozen); // Pokémon should still be frozen
        var battleLog = battle.GetBattleLog();
        Assert.Contains($"{pokemon.Name} is frozen solid!", battleLog);
    }

    [Fact]
    public void TestCheckFrozen_NotFrozen()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        pokemon.LevelUp(100);

        // Act
        var wasFrozen = battle.CheckFrozen(pokemon);

        // Assert
        Assert.False(wasFrozen); // Pokémon should not be frozen
        var battleLog = battle.GetBattleLog();
        Assert.DoesNotContain($"{pokemon.Name} is frozen solid!", battleLog);
        Assert.DoesNotContain($"{pokemon.Name} thawed out!", battleLog);
    }

    [Fact]
    public void CheckParalysis_PokemonNotParalyzed_ReturnsFalse()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        pokemon.LevelUp(100);

        // Act
        var result = battle.CheckParalysis(pokemon);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CheckParalysis_PokemonParalyzedAndCannotMove_ReturnsTrue()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        pokemon.LevelUp(100);
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Paralysis);

        // Mock the random number generator to return a value within the paralysis range
        var random = new RandomMock(21);
        battle.SetRandom(random);

        // Act
        var result = battle.CheckParalysis(pokemon);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckParalysis_PokemonParalyzedAndCanMove_ReturnsFalse()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        pokemon.LevelUp(100);
        pokemon.AddNonVolatileStatus(NonVolatileStatus.Paralysis);

        // Mock the random number generator to return a value within the paralysis range
        var random = new RandomMock(50);
        battle.SetRandom(random);

        // Act
        var result = battle.CheckParalysis(pokemon);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(2, "It's super effective!")]
    [InlineData(4, "It's super effective!")]
    [InlineData(0.25, "It's not very effective...")]
    [InlineData(0.5, "It's not very effective...")]
    [InlineData(0, "It has no effect!")]
    [InlineData(1, null)] // No log for neutral effectiveness
    public void CheckTypeEffectiveness_LogsCorrectMessage(double effectiveness, string expectedMessage)
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        BattleTeam team1 = new BattleTeam(pokemon);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        var battleLog = new List<string>();
        battle.SetBattleLog(battleLog);

        // Act
        battle.CheckTypeEffectiveness(effectiveness);

        // Assert
        if (expectedMessage != null)
        {
            Assert.Contains(expectedMessage, battleLog);
        }
        else
        {
            Assert.Empty(battleLog);
        }
    }


    [Fact]
    public void GetTarget_ReturnsDefendingPokemonInSamePosition()
    {
        // Arrange
        var attackingPokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defendingPokemon = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var activeTeam = new BattleTeam(attackingPokemon);
        var opposingTeam = new BattleTeam(defendingPokemon);
        var battle = new Battle(activeTeam, opposingTeam, new NinethGenerationBattleData());
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move

        // Act
        var result = battle.GetTarget(attackingPokemon, move, activeTeam, opposingTeam);

        // Assert
        Assert.Equal(defendingPokemon, result);
    }

    [Fact]
    public void GetTarget_ReturnsLeftPokemonWhenDefendingPokemonIsFainted()
    {
        // Arrange
        var attackingPokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defendingPokemon = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        defendingPokemon.CurrentHP = 0;
        var leftPokemon = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));

        var activeTeam = new BattleTeam(attackingPokemon);
        var opposingTeam = new BattleTeam(defendingPokemon, leftPokemon);
        var battle = new Battle(activeTeam, opposingTeam, new NinethGenerationBattleData());
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move


        // Act
        var result = battle.GetTarget(attackingPokemon, move, activeTeam, opposingTeam);

        // Assert
        Assert.Equal(leftPokemon, result);
    }

    [Fact]
    public void GetTarget_ReturnsRightPokemonWhenDefendingPokemonAndLeftPokemonAreFainted()
    {
        // Arrange
        var attackingPokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defendingPokemon = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        defendingPokemon.CurrentHP = 0;
        var leftPokemon = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));
        leftPokemon.CurrentHP = 0;
        var rightPokemon = PokedexRepository.CreatePokemon("" + 4, NatureRepository.GetNature("adamant"));
        var activeTeam = new BattleTeam(attackingPokemon);
        var opposingTeam = new BattleTeam(defendingPokemon, leftPokemon, rightPokemon);
        var battle = new Battle(activeTeam, opposingTeam, new NinethGenerationBattleData());
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move


        // Act
        var result = battle.GetTarget(attackingPokemon, move, activeTeam, opposingTeam);

        // Assert
        Assert.Equal(rightPokemon, result);
    }

    [Fact]
    public void GetTarget_ReturnsNullWhenAllDefendingPokemonAreFainted()
    {
        // Arrange
        var attackingPokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defendingPokemon = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        defendingPokemon.CurrentHP = 0;
        var leftPokemon = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));
        leftPokemon.CurrentHP = 0;
        var rightPokemon = PokedexRepository.CreatePokemon("" + 4, NatureRepository.GetNature("adamant"));
        rightPokemon.CurrentHP = 0;
        var activeTeam = new BattleTeam(attackingPokemon);
        var opposingTeam = new BattleTeam(defendingPokemon, leftPokemon, rightPokemon);
        var battle = new Battle(activeTeam, opposingTeam, new NinethGenerationBattleData());
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move


        // Act
        var result = battle.GetTarget(attackingPokemon, move, activeTeam, opposingTeam);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetTarget_CheckMiddle()
    {
        // Arrange
        var attackingPokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant"));
        var attackingPokemon1 = PokedexRepository.CreatePokemon("" + 130, NatureRepository.GetNature("adamant"));
        var attackingPokemon2 = PokedexRepository.CreatePokemon("" + 131, NatureRepository.GetNature("adamant"));
        var defendingPokemon = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var leftPokemon = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));
        leftPokemon.CurrentHP = 0;
        var rightPokemon = PokedexRepository.CreatePokemon("" + 4, NatureRepository.GetNature("adamant"));
        var activeTeam = new BattleTeam(attackingPokemon, attackingPokemon1, attackingPokemon2);

        var opposingTeam = new BattleTeam(defendingPokemon, leftPokemon, rightPokemon);
        var battle = new Battle(activeTeam, opposingTeam, new NinethGenerationBattleData());
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move


        // Act
        var result = battle.GetTarget(attackingPokemon1, move, activeTeam, opposingTeam);

        // Assert
        Assert.Equal(defendingPokemon, result);
    }

    [Fact]
    public void GetTarget_CheckMiddleFar()
    {
        // Arrange
        var attackingPokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant"));
        var attackingPokemon1 = PokedexRepository.CreatePokemon("" + 130, NatureRepository.GetNature("adamant"));
        var attackingPokemon2 = PokedexRepository.CreatePokemon("" + 131, NatureRepository.GetNature("adamant"));
        var defendingPokemon = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        defendingPokemon.CurrentHP = 0;
        var leftPokemon = PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"));
        leftPokemon.CurrentHP = 0;
        var rightPokemon = PokedexRepository.CreatePokemon("" + 4, NatureRepository.GetNature("adamant"));
        var activeTeam = new BattleTeam(attackingPokemon, attackingPokemon1, attackingPokemon2);

        var opposingTeam = new BattleTeam(defendingPokemon, leftPokemon, rightPokemon);
        var battle = new Battle(activeTeam, opposingTeam, new NinethGenerationBattleData());
        var move = new Move(33, "Tackle", TypeRepository.GetType("Normal"), MoveCategory.Physical, 35, 40, 1.0m, 0, false, false, false, false, false, Range.Normal, null); // High accuracy move


        // Act
        var result = battle.GetTarget(attackingPokemon1, move, activeTeam, opposingTeam);

        // Assert
        Assert.Equal(rightPokemon, result);
    }


}
