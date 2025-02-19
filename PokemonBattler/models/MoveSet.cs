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
    public Dictionary<string, List<MoveAbbreviated>> LevelUpMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> EggMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> MachineMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> TutorMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> RestrictedMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> DreamWorldMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> EventMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

    public Dictionary<string, List<MoveAbbreviated>> VirtualConsoleMovesAbbreviated { get; set; } = new Dictionary<string, List<MoveAbbreviated>>();

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


    public MoveSet(string pokemonName, string pokemonID, Dictionary<string, List<MoveAbbreviated>> LevelUpMoves, Dictionary<string, List<MoveAbbreviated>> EggMoves, Dictionary<string, List<MoveAbbreviated>> MachineMoves, Dictionary<string, List<MoveAbbreviated>> TutorMoves, Dictionary<string, List<MoveAbbreviated>> RestrictedMoves, Dictionary<string, List<MoveAbbreviated>> DreamWorldMoves, Dictionary<string, List<MoveAbbreviated>> EventMoves, Dictionary<string, List<MoveAbbreviated>> VirtualConsoleMoves)
    {
        PokemonName = pokemonName;
        PokemonID = pokemonID;
        this.LevelUpMovesAbbreviated = LevelUpMoves;
        this.EggMovesAbbreviated = EggMoves;
        this.MachineMovesAbbreviated = MachineMoves;
        this.TutorMovesAbbreviated = TutorMoves;
        this.RestrictedMovesAbbreviated = RestrictedMoves;
        this.DreamWorldMovesAbbreviated = DreamWorldMoves;
        this.EventMovesAbbreviated = EventMoves;
        this.VirtualConsoleMovesAbbreviated = VirtualConsoleMoves;
    }

    public void Unpack()
    {
        foreach (var key in LevelUpMovesAbbreviated.Keys)
        {
            LevelUpMoves[key] = new List<Move>();
            foreach (var move in LevelUpMovesAbbreviated[key])
            {
                LevelUpMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in EggMovesAbbreviated.Keys)
        {
            EggMoves[key] = new List<Move>();
            foreach (var move in EggMovesAbbreviated[key])
            {
                EggMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in MachineMovesAbbreviated.Keys)
        {
            MachineMoves[key] = new List<Move>();
            foreach (var move in MachineMovesAbbreviated[key])
            {
                MachineMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in TutorMovesAbbreviated.Keys)
        {
            TutorMoves[key] = new List<Move>();
            foreach (var move in TutorMovesAbbreviated[key])
            {
                TutorMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in RestrictedMovesAbbreviated.Keys)
        {
            RestrictedMoves[key] = new List<Move>();
            foreach (var move in RestrictedMovesAbbreviated[key])
            {
                RestrictedMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in DreamWorldMovesAbbreviated.Keys)
        {
            DreamWorldMoves[key] = new List<Move>();
            foreach (var move in DreamWorldMovesAbbreviated[key])
            {
                DreamWorldMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in EventMovesAbbreviated.Keys)
        {
            EventMoves[key] = new List<Move>();
            foreach (var move in EventMovesAbbreviated[key])
            {
                EventMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

        foreach (var key in VirtualConsoleMovesAbbreviated.Keys)
        {
            VirtualConsoleMoves[key] = new List<Move>();
            foreach (var move in VirtualConsoleMovesAbbreviated[key])
            {
                VirtualConsoleMoves[key].Add(MoveRepository.GetMoveByID(move.Id));
            }
        }

    }


}
