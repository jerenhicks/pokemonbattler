using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class MoveSet
{
    public string PokemonID { get; set; }
    [JsonProperty("name")]
    public string PokemonName { get; set; }
    [JsonProperty("learnset")]
    [JsonIgnore]
    public Dictionary<string, List<string>> Learnset { get; set; }
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


    public Dictionary<string, List<MoveSetMove>> LevelUpMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> EggMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> MachineMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> TutorMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> RestrictedMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> DreamWorldMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> EventMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();

    public Dictionary<string, List<MoveSetMove>> VirtualConsoleMovesRaw { get; set; } = new Dictionary<string, List<MoveSetMove>>();


    public MoveSet(string pokemonName, string pokemonID, Dictionary<string, List<string>> learnset)
    {
        PokemonName = pokemonName;
        PokemonID = pokemonID;
        Learnset = learnset;
    }

    public void UnpackTest()
    {
        foreach (string gen in LevelUpMovesRaw.Keys)
        {
            LevelUpMoves[gen] = new List<Move>();
            foreach (var move in LevelUpMovesRaw[gen])
            {
                LevelUpMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in EggMovesRaw.Keys)
        {
            EggMoves[gen] = new List<Move>();
            foreach (var move in EggMovesRaw[gen])
            {
                EggMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in MachineMovesRaw.Keys)
        {
            MachineMoves[gen] = new List<Move>();
            foreach (var move in MachineMovesRaw[gen])
            {
                MachineMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in TutorMovesRaw.Keys)
        {
            TutorMoves[gen] = new List<Move>();
            foreach (var move in TutorMovesRaw[gen])
            {
                TutorMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in RestrictedMovesRaw.Keys)
        {
            RestrictedMoves[gen] = new List<Move>();
            foreach (var move in RestrictedMovesRaw[gen])
            {
                RestrictedMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in DreamWorldMovesRaw.Keys)
        {
            DreamWorldMoves[gen] = new List<Move>();
            foreach (var move in DreamWorldMovesRaw[gen])
            {
                DreamWorldMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in EventMovesRaw.Keys)
        {
            EventMoves[gen] = new List<Move>();
            foreach (var move in EventMovesRaw[gen])
            {
                EventMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }
        foreach (string gen in VirtualConsoleMovesRaw.Keys)
        {
            VirtualConsoleMoves[gen] = new List<Move>();
            foreach (var move in VirtualConsoleMovesRaw[gen])
            {
                VirtualConsoleMoves[gen].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

    }

    public void Unpack()
    {
        foreach (var set in Learnset.Keys)
        {
            Move move = MoveRepository.GetMoveTrimmed(set);
            foreach (var genData in Learnset[set])
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
                            LevelUpMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        LevelUpMoves[generation].Add(move);
                        LevelUpMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'E':
                        //if generation doesn't exist in the keys, add one.
                        if (!EggMoves.ContainsKey(generation))
                        {
                            EggMoves.Add(generation, new List<Move>());
                            EggMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        EggMoves[generation].Add(move);
                        EggMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'M':
                        //if generation doesn't exist in the keys, add one.
                        if (!MachineMoves.ContainsKey(generation))
                        {
                            MachineMoves.Add(generation, new List<Move>());
                            MachineMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        MachineMoves[generation].Add(move);
                        MachineMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'T':
                        //if generation doesn't exist in the keys, add one.
                        if (!TutorMoves.ContainsKey(generation))
                        {
                            TutorMoves.Add(generation, new List<Move>());
                            TutorMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        TutorMoves[generation].Add(move);
                        TutorMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'R':
                        //if generation doesn't exist in the keys, add one.
                        if (!RestrictedMoves.ContainsKey(generation))
                        {
                            RestrictedMoves.Add(generation, new List<Move>());
                            RestrictedMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        RestrictedMoves[generation].Add(move);
                        RestrictedMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'D':
                        //if generation doesn't exist in the keys, add one.
                        if (!DreamWorldMoves.ContainsKey(generation))
                        {
                            DreamWorldMoves.Add(generation, new List<Move>());
                            DreamWorldMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        DreamWorldMoves[generation].Add(move);
                        DreamWorldMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'S':
                        //if generation doesn't exist in the keys, add one.
                        if (!EventMoves.ContainsKey(generation))
                        {
                            EventMoves.Add(generation, new List<Move>());
                            EventMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        EventMoves[generation].Add(move);
                        EventMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                    case 'V':
                        //if generation doesn't exist in the keys, add one.
                        if (!VirtualConsoleMoves.ContainsKey(generation))
                        {
                            VirtualConsoleMoves.Add(generation, new List<Move>());
                            VirtualConsoleMovesRaw.Add(generation, new List<MoveSetMove>());
                        }
                        VirtualConsoleMoves[generation].Add(move);
                        VirtualConsoleMovesRaw[generation].Add(new MoveSetMove(move.Id, move.Name));
                        break;
                }

            }

        }
    }


}
