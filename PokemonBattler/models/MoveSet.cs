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
                        LevelUpMoves[generation].Add(move);
                        //LevelUpMoves.Add("" + generation, move);
                        break;
                    case 'E':
                        EggMoves[generation].Add(move);
                        //EggMoves.Add("" + generation, move);
                        break;
                    case 'M':
                        MachineMoves[generation].Add(move);
                        //MachineMoves.Add("" + generation, move);
                        break;
                    case 'T':
                        TutorMoves[generation].Add(move);
                        //TutorMoves.Add("" + generation, move);
                        break;
                    case 'R':
                        RestrictedMoves[generation].Add(move);
                        //RestrictedMoves.Add("" + generation, move);
                        break;
                    case 'D':
                        DreamWorldMoves[generation].Add(move);
                        //DreamWorldMoves.Add("" + generation, move);
                        break;
                    case 'S':
                        EventMoves[generation].Add(move);
                        //EventMoves.Add("" + generation, move);
                        break;
                    case 'V':
                        VirtualConsoleMoves[generation].Add(move);
                        //VirtualConsoleMoves.Add("" + generation, move);
                        break;
                }

            }

        }
        Console.WriteLine("Done");
    }


}
