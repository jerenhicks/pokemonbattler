using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class MoveSetRepository
{

    private static readonly Dictionary<int, MoveSet> MoveSets = new Dictionary<int, MoveSet>();
    public static void LoadMoveSetsFromFile(string filePath)
    {


        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');

                List<Move> levelUpMoves = new List<Move>();
                foreach (var levelIds in values[1].Split('/'))
                {
                    levelUpMoves.Add(MoveRepository.GetMoveByID(int.Parse(levelIds)));
                }

                List<Move> machineMoves = new List<Move>();
                foreach (var machineIds in values[2].Split('/'))
                {
                    machineMoves.Add(MoveRepository.GetMoveByID(int.Parse(machineIds)));
                }

                List<Move> eggMoves = new List<Move>();
                foreach (var eggIds in values[3].Split('/'))
                {
                    eggMoves.Add(MoveRepository.GetMoveByID(int.Parse(eggIds)));
                }

                var moveSet = new MoveSet
                {
                    PokemonID = int.Parse(values[0]),
                    LevelUpMoves = levelUpMoves,
                    MachineMoves = machineMoves,
                    EggMoves = eggMoves
                };

                MoveSets.Add(moveSet.PokemonID, moveSet);
            }
        }
    }

    public static Dictionary<int, MoveSet> GetMoveSets()
    {
        return MoveSets;
    }
}