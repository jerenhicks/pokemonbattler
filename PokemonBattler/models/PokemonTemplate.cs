using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public class PokemonTemplate
{
    public string Name { get; private set; }
    public int PokedexNumber { get; private set; }
    public Type TypeOne { get; private set; }
    public Type TypeTwo { get; private set; }

    public int BaseHP { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseSpAtk { get; private set; }
    public int BaseSpDef { get; private set; }
    public int BaseSpeed { get; private set; }
    public int Level { get; private set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public PokemonTemplate(string name, int pokedexNumber, Type typeOne, Type typeTwo, int baseHP, int baseAtk, int baseDef, int baseSpAtk, int baseSpDef, int baseSpeed, List<Move> moves = null)
    {
        Name = name;
        PokedexNumber = pokedexNumber;
        TypeOne = typeOne;
        TypeTwo = typeTwo;

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
