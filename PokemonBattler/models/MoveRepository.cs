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
        return Moves[name.ToLower()].Clone();
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
                    name: values[0].Trim(),
                    type: TypeRepository.GetType(values[1]),
                    category: Enum.Parse<MoveCategory>(values[2], true),
                    pp: int.Parse(values[3]),
                    power: int.Parse(values[4]),
                    accuracy: values[5] == "null" ? (decimal?)null : decimal.Parse(values[5]),
                    priority: int.Parse(values[6]),
                    makesContact: bool.Parse(values[7]),
                    affectedByProtect: bool.Parse(values[8]),
                    affectedByMagicCoat: bool.Parse(values[9]),
                    affectedBySnatch: bool.Parse(values[10]),
                    affectedByMirrorMove: bool.Parse(values[11]),
                    affectedByKingsRock: bool.Parse(values[12])
                );
                if (!Moves.ContainsKey(move.Name.ToLower()))
                {
                    Moves.Add(move.Name.ToLower(), move);
                }
            }
        }
    }
}