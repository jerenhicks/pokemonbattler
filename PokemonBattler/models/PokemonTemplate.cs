using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PokemonTemplate
{
    public string Name { get; private set; }
    public int PokedexNumber { get; private set; }

    [JsonIgnore]
    public Type TypeOne { get; private set; }

    [JsonIgnore]
    public Type TypeTwo { get; private set; }
    public string TypeOneName { get; set; }
    public string TypeTwoName { get; set; }

    public int BaseHP { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseSpAtk { get; private set; }
    public int BaseSpDef { get; private set; }
    public int BaseSpeed { get; private set; }
    public int Level { get; private set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public PokemonTemplate(string name, int pokedexNumber, string typeOneName, string typeTwoName, int baseHP, int baseAtk, int baseDef, int baseSpAtk, int baseSpDef, int baseSpeed, List<Move> moves = null)
    {
        Name = name;
        PokedexNumber = pokedexNumber;
        TypeOneName = typeOneName;
        TypeTwoName = typeTwoName;
        if (TypeOneName != null)
        {
            TypeOne = TypeRepository.GetType(TypeOneName);
        }
        else
        {
            TypeOne = null;
        }
        if (TypeTwoName != null)
        {
            TypeTwo = TypeRepository.GetType(TypeTwoName);
        }
        else
        {
            TypeTwo = null;
        }

        BaseHP = baseHP;
        BaseAtk = baseAtk;
        BaseDef = baseDef;
        BaseSpAtk = baseSpAtk;
        BaseSpDef = baseSpDef;
        BaseSpeed = baseSpeed;
        Level = 1;

        if (moves != null)
        {
            Moves = moves;
        }
    }
}
