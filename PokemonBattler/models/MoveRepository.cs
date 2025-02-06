using System.Collections.Generic;
using Newtonsoft.Json;

public static class MoveRepository
{

    private static readonly Dictionary<string, Move> Moves = new Dictionary<string, Move>();

    public static void AddMove(Move move)
    {
        Moves.Add(move.Name.ToLower(), move);
    }

    public static Move GetMove(string name)
    {
        return Moves[name.ToLower()].Clone();
    }

    public static Move GetMoveTrimmed(string name)
    {
        foreach (var move in Moves.Values)
        {
            if (move.Name.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", ""))
            {
                return move.Clone();
            }
        }
        return null;
    }

    public static Move GetMoveByID(int id)
    {
        return Moves.Values.FirstOrDefault(m => m.Id == id)?.Clone();
    }

    public static void LoadMovesFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var moves = JsonConvert.DeserializeObject<List<Move>>(jsonData);

        foreach (var move in moves)
        {
            Moves[move.Name.ToLower()] = move;
        }
        foreach (var move in moves)
        {
            move.Unpack();
        }
    }

    public static void SaveMovesToFile(string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(Moves.Values, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }


    public static IEnumerable<Move> GetAllMoves()
    {
        return Moves.Values;
    }
}