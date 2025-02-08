using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleConsole
{
    int battleMode;
    private bool outputBattleLogs = true;
    private string basePath;
    private string currentDirectory;
    public BattleConsole(int battleMode)
    {

        currentDirectory = Directory.GetCurrentDirectory();
        basePath = currentDirectory;

        // Check if the data files exist in the current directory
        if (!File.Exists(Path.Combine(currentDirectory, "data", "natures.csv")))
        {
            // If not, assume we are running from the solution level and adjust the path
            basePath = Path.Combine(currentDirectory, "PokemonBattler");
        }
        this.battleMode = battleMode;
    }
    private void BattleCountdown()
    {
        Console.WriteLine("Battle starting in:");
        for (int i = 3; i > 0; i--)
        {
            Console.Write("{0}...", i);
            Thread.Sleep(1000);
        }
        Console.WriteLine("Go!\n");
        Thread.Sleep(300);
    }

    // TODO: if it's decided that the format of this battle console
    //      is desirable for the long-term, it may be worth creating
    //      a wrapper so that the repositories can be injected as
    //      depenencies to the BattleConsole constructor
    private void BattleAllMonsters()
    {
        LoadData();
        ClearOutputFile();

        BattleCountdown();

        var pokedexIds = PokedexRepository.PokemonIds();
        DateTime startTime;
        DateTime endTime;
        double totalTime = 0;
        PokeMetrics pokeMetrics = new PokeMetrics();

        //TODO: let the user pick which generation they want to use
        GenerationBattleData generationBattleData = new NinethGenerationBattleData();

        for (int id1 = 0; id1 < pokedexIds.Count; id1++)
        {
            Pokemon monster1 = PokedexRepository.CreatePokemon(pokedexIds[id1], NatureRepository.GetNature("adamant"), level: 100);
            List<Move> monster1Moves = MoveSetRepository.BuildRandomMoveSet(monster1.PokedexNumber, Generation.NINE, 4);
            monster1.AddMoves(monster1Moves);
            startTime = DateTime.Now;
            for (int id2 = id1 + 1; id2 < pokedexIds.Count; id2++)
            {
                Pokemon monster2 = PokedexRepository.CreatePokemon(pokedexIds[id2], NatureRepository.GetNature("adamant"), level: 100);
                List<Move> monster2Moves = MoveSetRepository.BuildRandomMoveSet(monster2.PokedexNumber, Generation.NINE, 4);
                monster2.AddMoves(monster2Moves);
                //monster2.AddNonVolatileStatus(NonVolatileStatus.Burn);

                Battle battle = new Battle(monster1, monster2, generationBattleData);
                battle.CommenceBattle();

                pokeMetrics.AddMetrics(battle);
                if (outputBattleLogs)
                {
                    OutputBattleLog(battle);
                }

                monster1.Reset();
                monster1.ResetNonVolatileStatuses();
            }

            endTime = DateTime.Now;
            var duration = (endTime - startTime).TotalMilliseconds;
            totalTime += duration;
            Console.WriteLine($"{monster1.Name}'s battles have completed in {duration} milliseconds.");
        }
        double totalSeconds = totalTime / 1000;

        var metricsFilePath = Path.Combine(basePath, "output", "metrics.txt");

        // Check if the metrics file exists, and create it if it doesn't
        if (!File.Exists(metricsFilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(metricsFilePath)); // Ensure the directory exists
            File.Create(metricsFilePath).Dispose(); // Create the file and close it immediately
        }

        pokeMetrics.OutputResultsToFile(metricsFilePath);
        Console.WriteLine($"Total time to run Battle Simulator: {(int)(totalSeconds / 60)} minutes and {(int)(totalSeconds % 60)} seconds.");
    }

    // TODO: basic structure/loop logic in place for Choose Your Pokemon mode;
    //      however, ideally there would be flexibility in allowing lookup
    //      by either pokemon name OR pokedex id

    // TODO: currently, just defaulting to battling magikarp and galvantula;
    //      need to actually do something with the user input
    private void ChooseYourPokemon()
    {
        LoadData();

        string pokemon1Name = "";
        string pokemon2Name = "";
        string restartResponse = "";
        bool validRestartResponse = false;
        bool keepBattling = true;
        //TODO: let the user pick which generation they want to use
        GenerationBattleData generationBattleData = new NinethGenerationBattleData();

        do
        {
            Console.WriteLine("Select the first pokemon you want to battle:");
            pokemon1Name = Console.ReadLine().Trim();
            Console.WriteLine("Select the second pokemon you want to battle:");
            pokemon2Name = Console.ReadLine().Trim();

            Console.WriteLine("Generating pokemon {0} and {1}!", pokemon1Name, pokemon2Name);

            BattleCountdown();

            // Create a Magikarp Pokemon with level 1 and specified base stats
            Pokemon magikarp1 = PokedexRepository.CreatePokemon("129", NatureRepository.GetNature("adamant"), level: 100);
            Pokemon galvantula = PokedexRepository.CreatePokemon("596", NatureRepository.GetNature("adamant"), level: 100);

            magikarp1.AddMove(MoveRepository.GetMove("Explosion"));
            galvantula.AddMove(MoveRepository.GetMove("Struggle"));

            //galvantula.AddNonVolatileStatus(NonVolatileStatus.Paralysis);

            Battle battle = new Battle(magikarp1, galvantula, generationBattleData);
            battle.CommenceBattle();
            foreach (var logEntry in battle.GetBattleLog()) // Updated line
            {
                Console.WriteLine(logEntry); // Output each log entry to the console
            }

            Console.WriteLine("Would you like to start a new battle? (Y/n): ");
            do
            {
                restartResponse = Console.ReadLine().Trim();
                if (restartResponse.ToLower() == "y")
                {
                    validRestartResponse = true;
                }
                else if (restartResponse.ToLower() == "n")
                {
                    keepBattling = false;
                    validRestartResponse = true;
                }

                if (!validRestartResponse)
                {
                    Console.WriteLine("Sorry, that was an invalid response. Please enter (Y/n): ");
                }
            } while (!validRestartResponse);
            validRestartResponse = false;
        } while (keepBattling);
    }

    public void Run()
    {
        if (battleMode == 1)
        {
            BattleAllMonsters();
        }
        else if (battleMode == 2)
        {
            ChooseYourPokemon();
        }
        else
        {
            Console.WriteLine("Invalid battle mode selected!!!");
        }
    }

    public void LoadData(bool testing = false)
    {

        if (testing)
        {
            basePath = Path.Combine(currentDirectory, "../../../..");
        }

        Console.WriteLine("Effects loaded!");
        NatureRepository.LoadNaturesFromFile(Path.Combine(basePath, "data", "natures.json"));
        Console.WriteLine("Natures loaded!");
        TypeRepository.LoadTypesFromFile(Path.Combine(basePath, "data", "types.json"));
        Console.WriteLine("Types loaded!");
        MoveRepository.LoadMovesFromFile(Path.Combine(basePath, "data", "moves.json"));
        Console.WriteLine("Moves loaded!");
        PokedexRepository.LoadPokedexFromFile(Path.Combine(basePath, "data", "pokedex.json"));
        Console.WriteLine("Pokedex loaded!");
        MoveSetRepository.LoadMoveSetsFromFile(Path.Combine(basePath, "data", "learnsets.json"));
        Console.WriteLine("MoveSets loaded!");

        //MoveSetRepository.SaveMoveSetsToFile(Path.Combine(basePath, "data", "learnsets-test1.json"));
        //PokedexRepository.SavePokedexToFile(Path.Combine(basePath, "data", "pokedex-test2.json"));
    }

    public void ClearOutputFile()
    {
        File.WriteAllText(Path.Combine(basePath, "output", "battleoutput.txt"), string.Empty);
    }

    public void OutputBattleLog(Battle battle)
    {
        //add a blank line before adding new battle log
        File.AppendAllText(Path.Combine(basePath, "output", "battleoutput.txt"), "\n");
        File.AppendAllLines(Path.Combine(basePath, "output", "battleoutput.txt"), battle.GetBattleLog());
    }
}

