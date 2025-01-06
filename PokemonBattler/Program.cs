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
        Pokemon magikarp = PokedexRepository.GetPokemon(129);

        magikarp.LevelUp(100);

        magikarp.DisplayStatus();

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

        // Display the status of Magikarp
        //magikarp.DisplayStatus();
    }
}