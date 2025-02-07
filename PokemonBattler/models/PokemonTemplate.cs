using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PokemonTemplate
{
    public string Name { get; private set; }
    public string PokedexNumber { get; set; }

    [JsonIgnore]
    public Type TypeOne { get; private set; }

    [JsonIgnore]
    public Type TypeTwo { get; private set; }
    public string TypeOneName { get; set; }
    public string TypeTwoName { get; set; }
    public List<BaseStats> BaseStats { get; set; }
    public int Level { get; private set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public PokemonTemplate(string name, string pokedexNumber, string typeOneName, string typeTwoName, List<BaseStats> baseStats, List<Move> moves = null)
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


        BaseStats = baseStats;
        Level = 1;

        if (moves != null)
        {
            Moves = moves;
        }
    }

}
