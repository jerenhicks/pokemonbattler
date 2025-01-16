﻿using System;

public class Program
{
    public static void Main()
    {
        var path = Directory.GetCurrentDirectory();
        Console.WriteLine(path);
        // Load natures from CSV file
        NatureRepository.LoadNaturesFromFile(path + "/PokemonBattler/data/natures.csv");
        TypeRepository.LoadTypesFromFile(path + "/PokemonBattler/data/types.csv");
        MoveRepository.LoadMovesFromFile(path + "/PokemonBattler/data/moves.csv");
        PokedexRepository.LoadPokedexFromFile(path + "/PokemonBattler/data/pokedex.csv");

        // Create a Magikarp Pokemon with level 1 and specified base stats
        Pokemon magikarp1 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"));
        Pokemon magikarp2 = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant"));
        Pokemon galvantula = PokedexRepository.CreatePokemon(596, NatureRepository.GetNature("adamant"));

        magikarp1.LevelUp(100);
        //magikarp2.LevelUp(80);


        magikarp1.AddMove(MoveRepository.GetMove("quick attack"));
        galvantula.LevelUp(100);
        galvantula.AddMove(MoveRepository.GetMove("pound"));
        galvantula.AddMove(MoveRepository.GetMove("tackle"));
        galvantula.AddMove(MoveRepository.GetMove("Slam"));
        galvantula.AddMove(MoveRepository.GetMove("Ice Punch"));

        //magikarp1.DisplayStatus();

        // Example usage
        // var fireType = TypeRepository.GetType("fire");
        // Console.WriteLine($"Type: {fireType.Name}");
        // Console.WriteLine("Super Effective Against:");
        // foreach (var type in fireType.SuperEffectiveAgainst)
        // {
        //     Console.WriteLine($"- {type.Name}");
        // }
        // Console.WriteLine("Not Effective Against:");
        // foreach (var type in fireType.NotEffectiveAgainst)
        // {
        //     Console.WriteLine($"- {type.Name}");
        // }
        // Console.WriteLine("No Effect Against:");
        // foreach (var type in fireType.NoEffectAgainst)
        // {
        //     Console.WriteLine($"- {type.Name}");
        // }

        Battle battle = new Battle(magikarp1, galvantula);
        battle.CommenceBattle();
        foreach (var logEntry in battle.GetBattleLog()) // Updated line
        {
            Console.WriteLine(logEntry); // Output each log entry to the console
        }

        // Display the status of Magikarp
        //magikarp.DisplayStatus();
    }
}