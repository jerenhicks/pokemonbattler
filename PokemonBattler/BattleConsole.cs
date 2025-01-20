using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBattler
{
    internal class BattleConsole
    {
        int battleMode;
        public BattleConsole(int battleMode)
        {
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
            var path = Directory.GetCurrentDirectory();
            // Load natures from CSV file
            NatureRepository.LoadNaturesFromFile(path + "/PokemonBattler/data/natures.csv");
            Console.WriteLine("Natures loaded!");
            TypeRepository.LoadTypesFromFile(path + "/PokemonBattler/data/types.csv");
            Console.WriteLine("Types loaded!");
            MoveRepository.LoadMovesFromFile(path + "/PokemonBattler/data/moves.csv");
            Console.WriteLine("Moves loaded!");
            PokedexRepository.LoadPokedexFromFile(path + "/PokemonBattler/data/pokedex.csv");
            Console.WriteLine("Pokedex loaded!");

            BattleCountdown();

            Pokemon magikarp1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"));
            Pokemon galvantula = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant"));

            magikarp1.LevelUp(100);

            magikarp1.AddMove(MoveRepository.GetMove("pound"));

            galvantula.LevelUp(100);
            galvantula.AddNonVolatileStatus(NonVolatileStatus.Burn);

            Battle battle = new Battle(magikarp1, galvantula);
            battle.CommenceBattle();
            foreach (var logEntry in battle.GetBattleLog()) // Updated line
            {
                Console.WriteLine(logEntry); // Output each log entry to the console
            }
        }

        // TODO: basic structure/loop logic in place for Choose Your Pokemon mode;
        //      however, ideally there would be flexibility in allowing lookup
        //      by either pokemon name OR pokedex id

        // TODO: currently, just defaulting to battling magikarp and galvantula;
        //      need to actually do something with the user input
        private void ChooseYourPokemon()
        {
            var path = Directory.GetCurrentDirectory();
            // Load natures from CSV file
            NatureRepository.LoadNaturesFromFile(path + "/PokemonBattler/data/natures.csv");
            Console.WriteLine("Natures loaded!");
            TypeRepository.LoadTypesFromFile(path + "/PokemonBattler/data/types.csv");
            Console.WriteLine("Types loaded!");
            MoveRepository.LoadMovesFromFile(path + "/PokemonBattler/data/moves.csv");
            Console.WriteLine("Moves loaded!");
            PokedexRepository.LoadPokedexFromFile(path + "/PokemonBattler/data/pokedex.csv");
            Console.WriteLine("Pokedex loaded!");

            string pokemon1Name = "";
            string pokemon2Name = "";
            string restartResponse = "";
            bool validRestartResponse = false;
            bool keepBattling = true;

            do
            {
                Console.WriteLine("Select the first pokemon you want to battle:");
                pokemon1Name = Console.ReadLine().Trim();
                Console.WriteLine("Select the second pokemon you want to battle:");
                pokemon2Name = Console.ReadLine().Trim();

                Console.WriteLine("Generating pokemon {0} and {1}!", pokemon1Name, pokemon2Name);

                BattleCountdown();

                // Create a Magikarp Pokemon with level 1 and specified base stats
                Pokemon magikarp1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"));
                Pokemon galvantula = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant"));

                magikarp1.LevelUp(100);

                magikarp1.AddMove(MoveRepository.GetMove("pound"));

                galvantula.LevelUp(100);
                galvantula.AddNonVolatileStatus(NonVolatileStatus.Burn);

                Battle battle = new Battle(magikarp1, galvantula);
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
            } else
            {
                Console.WriteLine("Invalid battle mode selected!!!");
            }
        }
    }
}
