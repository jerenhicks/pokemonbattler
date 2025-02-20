using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleConsole
{
    public int BattleMode { get; set; }
    public bool OutputBattleLogs { get; set; } = false;
    private string basePath;
    private string currentDirectory;

    private StreamWriter writer;
    private static string OUTPUT_FILE_PATH = "battleoutput.txt";
    private string OutputPath = null;
    public BattleConsole(int battleMode = 1)
    {

        currentDirectory = Directory.GetCurrentDirectory();
        basePath = currentDirectory;

        // Check if the data files exist in the current directory
        if (!File.Exists(Path.Combine(currentDirectory, "data", "natures.csv")))
        {
            // If not, assume we are running from the solution level and adjust the path
            basePath = Path.Combine(currentDirectory, "PokemonBattler");
        }
        OutputPath = Path.Combine(basePath, "output", OUTPUT_FILE_PATH);

        this.BattleMode = battleMode;
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
                BattleTeam team1 = new BattleTeam(monster1);
                BattleTeam team2 = new BattleTeam(monster2);

                Battle battle = new Battle(team1, team2, generationBattleData);
                battle.CommenceBattle();

                pokeMetrics.AddMetrics(battle);
                if (OutputBattleLogs)
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
        CloseOutputFile();
        Console.WriteLine($"Total time to run Battle Simulator: {(int)(totalSeconds / 60)} minutes and {(int)(totalSeconds % 60)} seconds.");
    }

    // TODO: basic structure/loop logic in place for Choose Your Pokemon mode;
    //      however, ideally there would be flexibility in allowing lookup
    //      by either pokemon name OR pokedex id

    // TODO: currently, just defaulting to battling magikarp and galvantula;
    //      need to actually do something with the user input
    private void ChooseYourPokemon()
    {
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

            magikarp1.AddMove(MoveRepository.GetMove("acid"));
            galvantula.AddMove(MoveRepository.GetMove("Struggle"));

            //galvantula.AddNonVolatileStatus(NonVolatileStatus.Paralysis);
            BattleTeam team1 = new BattleTeam(magikarp1);
            BattleTeam team2 = new BattleTeam(galvantula);

            Battle battle = new Battle(team1, team2, generationBattleData);
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
        if (BattleMode == 1)
        {
            BattleAllMonsters();
        }
        else if (BattleMode == 2)
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

        Console.Write("Loading Effects...");
        DateTime startTime = DateTime.Now;
        EffectRepository.LoadEffectsFromAssembly();
        DateTime endTime = DateTime.Now;
        Console.WriteLine("Effects loaded -- Time To Load: " + (endTime - startTime).TotalMilliseconds + "ms");
        Console.Write("Loading Natures...");
        startTime = DateTime.Now;
        NatureRepository.LoadNaturesFromFile(Path.Combine(basePath, "data", "natures.json"));
        endTime = DateTime.Now;
        Console.WriteLine("Natures loaded! -- Time To Load: " + (endTime - startTime).TotalMilliseconds + "ms");
        Console.Write("Loading Types...");
        startTime = DateTime.Now;
        TypeRepository.LoadTypesFromFile(Path.Combine(basePath, "data", "types.json"));
        endTime = DateTime.Now;
        Console.WriteLine("Types loaded! -- Time To Load: " + (endTime - startTime).TotalMilliseconds + "ms");
        Console.Write("Loading Moves...");
        startTime = DateTime.Now;
        MoveRepository.LoadMovesFromFile(Path.Combine(basePath, "data", "moves.json"));
        endTime = DateTime.Now;
        Console.WriteLine("Moves loaded! -- Time To Load: " + (endTime - startTime).TotalMilliseconds + "ms");
        Console.Write("Loading Pokedex...");
        startTime = DateTime.Now;
        PokedexRepository.LoadPokedexFromFile(Path.Combine(basePath, "data", "pokedex.json"));
        endTime = DateTime.Now;
        Console.WriteLine("Pokedex loaded! -- Time To Load: " + (endTime - startTime).TotalMilliseconds + "ms");
        Console.Write("Loading MoveSets...");
        startTime = DateTime.Now;
        MoveSetRepository.LoadMoveSetsFromFile(Path.Combine(basePath, "data", "movesets.json"));
        endTime = DateTime.Now;
        Console.WriteLine("MoveSets loaded! -- Time To Load: " + (endTime - startTime).TotalMilliseconds + "ms");

        //MoveSetRepository.SaveMockMoveSets(Path.Combine(basePath, "data", "learnsets-test1.json"));
        //PokedexRepository.SavePokedexToFile(Path.Combine(basePath, "data", "pokedex-test2.json"));
    }

    public void ClearOutputFile()
    {
        File.WriteAllText(OutputPath, string.Empty);
    }

    public void OutputBattleLog(Battle battle)
    {
        if (writer == null)
        {
            writer = new StreamWriter(OutputPath, true);
        }


        writer.WriteLine("\n");
        foreach (var row in battle.GetBattleLog())
        {
            writer.WriteLine(row);
        }
    }

    public void CloseOutputFile()
    {
        if (writer != null)
        {
            writer.Flush();
            writer.Close();
        }
    }

}

