using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


public class MoveSetRepository
{

    private static readonly Dictionary<string, MoveSet> MoveSets = new Dictionary<string, MoveSet>();
    public static void LoadMoveSetsFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var moveSets = JsonConvert.DeserializeObject<List<MoveSet>>(jsonData);

        foreach (var moveSet in moveSets)
        {
            moveSet.Unpack();
        }
        foreach (var moveSet in moveSets)
        {
            MoveSets[moveSet.PokemonID] = moveSet;
        }
    }

    public static void SaveMoveSetsToFile(string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(MoveSets.Values, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }

    public static Dictionary<string, MoveSet> GetMoveSets()
    {
        return MoveSets;
    }


    public static List<Move> BuildRandomMoveSet(string pokemonID, Generation generationToInclude = Generation.NINE, int numMoves = 4, bool incldueTMs = false, bool includeEggMoves = false, bool includeTutorMoves = false, bool includeRestrictedMoves = false, bool includeDreamWorldMoves = false, bool includeEventMoves = false, bool includeVirtualConsoleMoves = false)
    {
        var moveSet = MoveSets[pokemonID];

        List<Move> moves = new List<Move>();

        var generation = generationToInclude;
        do
        {
            moves.AddRange(LoadMovesForGeneration(pokemonID, generation, incldueTMs, includeEggMoves, includeTutorMoves, includeRestrictedMoves, includeDreamWorldMoves, includeEventMoves, includeVirtualConsoleMoves));
            generation--;
        } while (moves.Count == 0 && generation > 0);



        var random = new Random();
        //come up with 4 random ids that start from 0 and cap at moves.Count, they all have to be unique
        var uniqueIndices = new HashSet<int>();
        while (uniqueIndices.Count < numMoves && uniqueIndices.Count < moves.Count)
        {
            uniqueIndices.Add(random.Next(0, moves.Count));
        }

        var selectedMoves = new List<Move>();
        foreach (var index in uniqueIndices)
        {
            selectedMoves.Add(moves[index]);
        }

        return selectedMoves;
    }

    private static List<Move> LoadMovesForGeneration(string pokemonID, Generation generationToInclude = Generation.NINE, bool incldueTMs = false, bool includeEggMoves = false, bool includeTutorMoves = false, bool includeRestrictedMoves = false, bool includeDreamWorldMoves = false, bool includeEventMoves = false, bool includeVirtualConsoleMoves = false)
    {
        var moveSet = MoveSets[pokemonID];

        List<Move> moves = new List<Move>();
        var genToUse = "" + (int)generationToInclude;
        if (moveSet.LevelUpMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.LevelUpMoves[genToUse]);
        }
        if (incldueTMs && moveSet.MachineMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.MachineMoves[genToUse]);
        }
        if (includeEggMoves && moveSet.EggMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.EggMoves[genToUse]);
        }
        if (includeTutorMoves && moveSet.TutorMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.TutorMoves[genToUse]);
        }
        if (includeRestrictedMoves && moveSet.RestrictedMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.RestrictedMoves[genToUse]);
        }
        if (includeDreamWorldMoves && moveSet.DreamWorldMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.DreamWorldMoves[genToUse]);
        }
        if (includeEventMoves && moveSet.EventMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.EventMoves[genToUse]);
        }
        if (includeVirtualConsoleMoves && moveSet.VirtualConsoleMoves.ContainsKey(genToUse))
        {
            moves.AddRange(moveSet.VirtualConsoleMoves[genToUse]);
        }

        return moves;
    }
}