using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


public class MoveSetRepository
{

    private static readonly Dictionary<int, MoveSet> MoveSets = new Dictionary<int, MoveSet>();
    public static void LoadMoveSetsFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var moveSets = JsonConvert.DeserializeObject<List<MoveSet>>(jsonData);

        foreach (var moveSet in moveSets)
        {
            MoveSets[moveSet.PokemonID] = moveSet;
        }
        foreach (var moveSet in moveSets)
        {
            moveSet.Unpack();
        }


    }

    public static Dictionary<int, MoveSet> GetMoveSets()
    {
        return MoveSets;
    }


    public static List<Move> BuildRandomMoveSet(int pokemonID, int generationToInclude = 9, int numMoves = 4, bool incldueTMs = false, bool includeEggMoves = false, bool includeTutorMoves = false, bool includeRestrictedMoves = false, bool includeDreamWorldMoves = false, bool includeEventMoves = false, bool includeVirtualConsoleMoves = false)
    {
        var moveSet = MoveSets[pokemonID];

        List<Move> moves = new List<Move>();

        var generation = 9;
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

    private static List<Move> LoadMovesForGeneration(int pokemonID, int generationToInclude = 9, bool incldueTMs = false, bool includeEggMoves = false, bool includeTutorMoves = false, bool includeRestrictedMoves = false, bool includeDreamWorldMoves = false, bool includeEventMoves = false, bool includeVirtualConsoleMoves = false)
    {
        var moveSet = MoveSets[pokemonID];

        List<Move> moves = new List<Move>();
        moves.AddRange(moveSet.LevelUpMoves[generationToInclude.ToString()]);
        if (incldueTMs)
        {
            moves.AddRange(moveSet.MachineMoves[generationToInclude.ToString()]);
        }
        if (includeEggMoves)
        {
            moves.AddRange(moveSet.EggMoves[generationToInclude.ToString()]);
        }
        if (includeTutorMoves)
        {
            moves.AddRange(moveSet.TutorMoves[generationToInclude.ToString()]);
        }
        if (includeRestrictedMoves)
        {
            moves.AddRange(moveSet.RestrictedMoves[generationToInclude.ToString()]);
        }
        if (includeDreamWorldMoves)
        {
            moves.AddRange(moveSet.DreamWorldMoves[generationToInclude.ToString()]);
        }
        if (includeEventMoves)
        {
            moves.AddRange(moveSet.EventMoves[generationToInclude.ToString()]);
        }
        if (includeVirtualConsoleMoves)
        {
            moves.AddRange(moveSet.VirtualConsoleMoves[generationToInclude.ToString()]);
        }

        return moves;
    }
}