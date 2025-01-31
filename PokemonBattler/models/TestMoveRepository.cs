using System.Collections.Generic;
using Newtonsoft.Json;

public static class TestMoveRepository
{

    private static readonly Dictionary<string, TestMove> Moves = new Dictionary<string, TestMove>();



    public static void LoadMovesFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var moves = JsonConvert.DeserializeObject<List<TestMove>>(jsonData);

        foreach (var move in moves)
        {
            Moves[move.Name.ToLower()] = move;
        }
        foreach (var move in moves)
        {
            move.Unpack();
        }
    }

    public static void SaveTestMovesToFile(string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(Moves.Values, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }


    public static IEnumerable<TestMove> GetAllTestMoves()
    {
        return Moves.Values;
    }
}