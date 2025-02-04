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
}