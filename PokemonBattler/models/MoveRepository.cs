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

    public static Move GetMoveByID(int id)
    {
        return Moves.Values.FirstOrDefault(m => m.Id == id)?.Clone();
    }

    // public static void LoadMovesFromFileOLD(string filePath)
    // {
    //     using (var reader = new StreamReader(filePath))
    //     {
    //         string line;
    //         while ((line = reader.ReadLine()) != null)
    //         {
    //             var values = line.Split(',');

    //             List<BaseEffect> effects = new List<BaseEffect>();

    //             for (int i = 15; i < values.Length; i++)
    //             {
    //                 if (values[i] != "null")
    //                 {
    //                     var effectString = values[i].Trim();
    //                     BaseEffect effect = null;

    //                     if (effectString.Contains("(") && effectString.Contains(")"))
    //                     {
    //                         var effectName = effectString.Substring(0, effectString.IndexOf("("));
    //                         var parameterString = effectString.Substring(effectString.IndexOf("(") + 1, effectString.IndexOf(")") - effectString.IndexOf("(") - 1);
    //                         var parameter = double.Parse(parameterString);

    //                         effect = EffectRepository.GetEffect(effectName);
    //                         if (effect != null)
    //                         {
    //                             effect.SetModifier(parameter);
    //                         }
    //                     }
    //                     else
    //                     {
    //                         effect = EffectRepository.GetEffect(effectString);
    //                     }

    //                     if (effect != null)
    //                     {
    //                         effects.Add(effect);
    //                     }
    //                 }
    //             }

    //             var move = new Move(
    //                 id: int.Parse(values[0]),
    //                 name: values[1].Trim(),
    //                 type: TypeRepository.GetType(values[2]),
    //                 category: Enum.Parse<MoveCategory>(values[3], true),
    //                 pp: int.Parse(values[4]),
    //                 power: values[5] == "null" ? (int?)null : int.Parse(values[5]),
    //                 accuracy: values[6] == "null" ? (decimal?)null : decimal.Parse(values[6]),
    //                 priority: int.Parse(values[7]),
    //                 makesContact: bool.Parse(values[8]),
    //                 affectedByProtect: bool.Parse(values[9]),
    //                 affectedByMagicCoat: bool.Parse(values[10]),
    //                 affectedBySnatch: bool.Parse(values[11]),
    //                 affectedByMirrorMove: bool.Parse(values[12]),
    //                 affectedByKingsRock: bool.Parse(values[13]),
    //                 range: Enum.Parse<Range>(values[14], true),
    //                 effects: effects
    //             );

    //             move.TypeName = values[2];
    //             move.CategoryName = values[3];
    //             move.RangeName = values[14];
    //             move.EffectNames = values.Skip(15).Where(v => v != "null").Select(v => v.Trim()).ToList();

    //             if (!Moves.ContainsKey(move.Name.ToLower()))
    //             {
    //                 Moves.Add(move.Name.ToLower(), move);
    //             }
    //         }
    //     }
    // }

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