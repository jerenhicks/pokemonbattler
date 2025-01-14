using System.Collections.Generic;

public static class MoveRepository
{

    private static readonly Dictionary<string, Move> Moves = new Dictionary<string, Move>();

    public static void AddMove(Move move)
    {
        Moves.Add(move.Name.ToLower(), move);
    }

    public static Move GetMove(string name)
    {
        return Moves[name.ToLower()];
    }

    public static void LoadMovesFromFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                var move = new Move(
                    name: values[0],
                    type: TypeRepository.GetType(values[1]),
                    category: Enum.Parse<MoveCategory>(values[2], true),
                    pp: int.Parse(values[3]),
                    power: int.Parse(values[4]),
                    accuracy: values[5] == "null" ? (decimal?)null : decimal.Parse(values[5])
                );
                if (!Moves.ContainsKey(move.Name))
                {
                    Moves.Add(move.Name.ToLower(), move);
                }
            }
        }
    }
}