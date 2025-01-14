using System;

public class Program
{
    public static void Main()
    {
        // Load natures from CSV file
        NatureRepository.LoadNaturesFromFile("PokemonBattler/data/natures.csv");
        TypeRepository.LoadTypesFromFile("PokemonBattler/data/types.csv");
        MoveRepository.LoadMovesFromFile("PokemonBattler/data/moves.csv");
        PokedexRepository.LoadPokedexFromFile("PokemonBattler/data/pokedex.csv");

        // Create a Magikarp Pokemon with level 1 and specified base stats
        Pokemon magikarp1 = PokedexRepository.CreatePokemon(129);
        Pokemon magikarp2 = PokedexRepository.CreatePokemon(129);

        magikarp1.LevelUp(100);
        magikarp2.LevelUp(100);

        //magikarp1.DisplayStatus();

        // Example usage
        var fireType = TypeRepository.GetType("fire");
        Console.WriteLine($"Type: {fireType.Name}");
        Console.WriteLine("Super Effective Against:");
        foreach (var type in fireType.SuperEffectiveAgainst)
        {
            Console.WriteLine($"- {type.Name}");
        }
        Console.WriteLine("Not Effective Against:");
        foreach (var type in fireType.NotEffectiveAgainst)
        {
            Console.WriteLine($"- {type.Name}");
        }
        Console.WriteLine("No Effect Against:");
        foreach (var type in fireType.NoEffectAgainst)
        {
            Console.WriteLine($"- {type.Name}");
        }

        Battle battle = new Battle(magikarp1, magikarp2);
        battle.CommenceBattle();
        foreach (var logEntry in battle.GetBattleLog()) // Updated line
        {
            Console.WriteLine(logEntry); // Output each log entry to the console
        }

        // Display the status of Magikarp
        //magikarp.DisplayStatus();
    }
}