using Xunit;
using System.Collections.Generic;

public class PokeMetricsTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public PokeMetricsTest(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void TestPokeMetricsInitialization()
    {
        // Arrange & Act
        var pokeMetrics = new PokeMetrics();

        // Assert
        Assert.NotNull(pokeMetrics.Metrics);
        Assert.Empty(pokeMetrics.Metrics);
    }

    [Fact]
    public void TestAddMetrics_NewMetrics()
    {
        // Arrange
        var pokeMetrics = new PokeMetrics();
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var pokemon3 = PokedexRepository.CreatePokemon("" + 1, NatureRepository.GetNature("adamant")); // Bulbasaur
        var pokemon4 = PokedexRepository.CreatePokemon("" + 2, NatureRepository.GetNature("adamant")); // Ivysaur
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        pokemon3.LevelUp(100);
        pokemon4.LevelUp(100);
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted
        pokemon3.CurrentHP = 50; // Bulbasaur is not fainted
        pokemon4.CurrentHP = 0; // Ivysaur is not fainted
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        BattleTeam team3 = new BattleTeam(pokemon3);
        BattleTeam team4 = new BattleTeam(pokemon4);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());
        var battle2 = new Battle(team3, team1, new NinethGenerationBattleData());
        var battle3 = new Battle(team2, team4, new NinethGenerationBattleData());

        // Act
        pokeMetrics.AddMetrics(battle);

        // Assert
        Assert.Contains(team1, pokeMetrics.Metrics.Keys);
        Assert.Contains(team2, pokeMetrics.Metrics.Keys);

        pokeMetrics.AddMetrics(battle2);
        Assert.Contains(team3, pokeMetrics.Metrics.Keys);
        Assert.Contains(team1, pokeMetrics.Metrics.Keys);

        pokeMetrics.AddMetrics(battle3);
        Assert.Contains(team4, pokeMetrics.Metrics.Keys);
        Assert.Contains(team2, pokeMetrics.Metrics.Keys);
    }

    [Fact]
    public void TestAddMetrics_ExistingMetrics()
    {
        // Arrange
        var pokeMetrics = new PokeMetrics();
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula

        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);

        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Add initial metrics
        pokeMetrics.AddMetrics(battle);

        // Act
        pokeMetrics.AddMetrics(battle);

        // Assert
        Assert.Contains(team1, pokeMetrics.Metrics.Keys);
        Assert.Contains(team2, pokeMetrics.Metrics.Keys);
        Assert.Equal(1, pokeMetrics.Metrics[team1].Losses);
        Assert.Equal(1, pokeMetrics.Metrics[team2].Wins);
    }


    [Fact]
    public void TestAddMetrics_BothFainted()
    {
        // Arrange
        var pokeMetrics = new PokeMetrics();
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 0; // Galvantula is fainted

        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Act
        pokeMetrics.AddMetrics(battle);

        // Assert
        Assert.Contains(team1, pokeMetrics.Metrics.Keys);
        Assert.Contains(team2, pokeMetrics.Metrics.Keys);
        Assert.Equal(1, pokeMetrics.Metrics[team1].Ties);
        Assert.Equal(1, pokeMetrics.Metrics[team2].Ties);
    }

    [Fact]
    public void TestOutputResults()
    {
        // Arrange
        var pokeMetrics = new PokeMetrics();
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, new NinethGenerationBattleData());

        // Act
        pokeMetrics.AddMetrics(battle);

        // Redirect console output
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Call OutputResults method
            pokeMetrics.OutputResultsToConsole();

            // Assert
            var output = sw.ToString();
            Assert.Contains("Magikarp", output);
            Assert.Contains("1 losses", output);
            Assert.Contains("Galvantula", output);
            Assert.Contains("0 wins", output);
        }
    }

    [Fact]
    public void TestOutputResultsToFile()
    {
        // Arrange
        var pokeMetrics = new PokeMetrics();
        var pokemon1 = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var pokemon2 = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        pokemon1.LevelUp(100);
        pokemon2.LevelUp(100);
        pokemon1.CurrentHP = 0; // Magikarp is fainted
        pokemon2.CurrentHP = 50; // Galvantula is not fainted
        GenerationBattleData generationBattleData = new NinethGenerationBattleData();
        BattleTeam team1 = new BattleTeam(pokemon1);
        BattleTeam team2 = new BattleTeam(pokemon2);
        var battle = new Battle(team1, team2, generationBattleData);

        // Act
        pokeMetrics.AddMetrics(battle);

        // Define the output file path
        var outputFilePath = "output/metrics.txt";

        // Ensure the output directory exists
        Directory.CreateDirectory("output");

        // Call OutputResultsToFile method
        pokeMetrics.OutputResultsToFile(outputFilePath);

        // Assert
        Assert.True(File.Exists(outputFilePath));

        var output = File.ReadAllText(outputFilePath);
        Assert.Contains("Magikarp has 0 wins, 1 losses, and 0 ties.", output);
        Assert.Contains("Galvantula has 1 wins, 0 losses, and 0 ties.", output);

        // Clean up
        File.Delete(outputFilePath);
    }
}