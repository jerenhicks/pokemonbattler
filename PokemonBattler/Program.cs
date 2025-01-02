using System;

public class Program
{
    public static void Main()
    {
        // Load natures from CSV file
        NatureRepository.LoadNaturesFromFile("PokemonBattler/data/natures.csv");
        TypeRepository.LoadTypesFromFile("PokemonBattler/data/types.csv");

        // Create a Magikarp Pokemon with level 1 and specified base stats
        Pokemon magikarp = new Pokemon(
            name: "Magikarp",
            typeOne: "Water",
            typeTwo: null,
            nature: NatureRepository.GetNature("Adamant"),
            baseHP: 20,
            baseAtk: 10,
            baseDef: 55,
            baseSpAtk: 15,
            baseSpDef: 20,
            baseSpeed: 80,
            ivHP: 0,
            ivAtk: 0,
            ivDef: 0,
            ivSpAtk: 0,
            ivSpDef: 0,
            ivSpeed: 0,
            evHP: 0,
            evAtk: 0,
            evDef: 0,
            evSpAtk: 0,
            evSpDef: 0,
            evSpeed: 0
        );

        magikarp.LevelUp(100);

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