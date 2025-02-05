using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class MoveSet
{
    public int PokemonID { get; set; }
    public string PokemonName { get; set; }
    [JsonProperty("learnset")]
    public Dictionary<string, List<string>> LearnsetsDictionary { get; set; }
    [JsonIgnore]
    public Dictionary<string, List<Move>> LevelUpMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> EggMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> MachineMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> TutorMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> RestrictedMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> DreamWorldMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> EventMoves { get; set; } = new Dictionary<string, List<Move>>();
    [JsonIgnore]
    public Dictionary<string, List<Move>> VirtualConsoleMoves { get; set; } = new Dictionary<string, List<Move>>();


    public MoveSet(int pokemonID, string pokemonName, Dictionary<string, List<string>> learnsetDictionary)
    {
        PokemonID = pokemonID;
        PokemonName = pokemonName;
        LearnsetsDictionary = learnsetDictionary;
    }

    public void Unpack()
    {
        foreach (var set in LearnsetsDictionary.Keys)
        {
            Move move = MoveRepository.GetMoveTrimmed(set);
            foreach (var genData in LearnsetsDictionary[set])
            {
                //pull the first character from the string, this will be the generation identifier.
                string generation = genData[0].ToString();
                //the second character is the method for learning, M = Machine, L = Level Up, T = tutor, R = restricted, E = egg, D = dream world, S = event, V = virtual console
                var method = genData[1];
                //ignore the rest for now.
                switch (method)
                {
                    case 'L':
                        //if generation doesn't exist in the keys, add one.
                        if (!LevelUpMoves.ContainsKey(generation))
                        {
                            LevelUpMoves.Add(generation, new List<Move>());
                        }
                        LevelUpMoves[generation].Add(move);
                        break;
                    case 'E':
                        //if generation doesn't exist in the keys, add one.
                        if (!EggMoves.ContainsKey(generation))
                        {
                            EggMoves.Add(generation, new List<Move>());
                        }
                        EggMoves[generation].Add(move);
                        break;
                    case 'M':
                        //if generation doesn't exist in the keys, add one.
                        if (!MachineMoves.ContainsKey(generation))
                        {
                            MachineMoves.Add(generation, new List<Move>());
                        }
                        MachineMoves[generation].Add(move);
                        break;
                    case 'T':
                        //if generation doesn't exist in the keys, add one.
                        if (!TutorMoves.ContainsKey(generation))
                        {
                            TutorMoves.Add(generation, new List<Move>());
                        }
                        TutorMoves[generation].Add(move);
                        break;
                    case 'R':
                        //if generation doesn't exist in the keys, add one.
                        if (!RestrictedMoves.ContainsKey(generation))
                        {
                            RestrictedMoves.Add(generation, new List<Move>());
                        }
                        RestrictedMoves[generation].Add(move);
                        break;
                    case 'D':
                        //if generation doesn't exist in the keys, add one.
                        if (!DreamWorldMoves.ContainsKey(generation))
                        {
                            DreamWorldMoves.Add(generation, new List<Move>());
                        }
                        DreamWorldMoves[generation].Add(move);
                        break;
                    case 'S':
                        //if generation doesn't exist in the keys, add one.
                        if (!EventMoves.ContainsKey(generation))
                        {
                            EventMoves.Add(generation, new List<Move>());
                        }
                        EventMoves[generation].Add(move);
                        break;
                    case 'V':
                        //if generation doesn't exist in the keys, add one.
                        if (!VirtualConsoleMoves.ContainsKey(generation))
                        {
                            VirtualConsoleMoves.Add(generation, new List<Move>());
                        }
                        VirtualConsoleMoves[generation].Add(move);
                        break;
                }

            }

        }
    }


}
