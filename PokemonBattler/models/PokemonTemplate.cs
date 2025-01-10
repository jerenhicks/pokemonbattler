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
    public Nature Nature { get; private set; }

    public int BaseHP { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseSpAtk { get; private set; }
    public int BaseSpDef { get; private set; }
    public int BaseSpeed { get; private set; }
    public int Level { get; private set; }

    public int IVHP { get; private set; }
    public int IVAtk { get; private set; }
    public int IVDef { get; private set; }
    public int IVSpAtk { get; private set; }
    public int IVSpDef { get; private set; }
    public int IVSpeed { get; private set; }

    public int EVHP { get; private set; }
    public int EVAtk { get; private set; }
    public int EVDef { get; private set; }
    public int EVSpAtk { get; private set; }
    public int EVSpDef { get; private set; }
    public int EVSpeed { get; private set; }

    // New properties for calculated stats
    public int HP { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int SpAtk { get; private set; }
    public int SpDef { get; private set; }
    public int Speed { get; private set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public PokemonTemplate(string name, int pokedexNumber, Type typeOne, Type typeTwo, Nature nature, int baseHP, int baseAtk, int baseDef, int baseSpAtk, int baseSpDef, int baseSpeed, int ivHP, int ivAtk, int ivDef, int ivSpAtk, int ivSpDef, int ivSpeed, int evHP, int evAtk, int evDef, int evSpAtk, int evSpDef, int evSpeed, List<Move> moves = null)
    {
        // Check IVs
        if (ivHP < 0 || ivHP > 31 || ivAtk < 0 || ivAtk > 31 || ivDef < 0 || ivDef > 31 || ivSpAtk < 0 || ivSpAtk > 31 || ivSpDef < 0 || ivSpDef > 31 || ivSpeed < 0 || ivSpeed > 31)
        {
            throw new ArgumentException("IV stats must be between 0 and 31.");
        }

        // Check EVs
        if (evHP < 0 || evHP > 255 || evAtk < 0 || evAtk > 255 || evDef < 0 || evDef > 255 || evSpAtk < 0 || evSpAtk > 255 || evSpDef < 0 || evSpDef > 255 || evSpeed < 0 || evSpeed > 255)
        {
            throw new ArgumentException("EV stats must be between 0 and 255.");
        }

        // Check total EVs
        int totalEVs = evHP + evAtk + evDef + evSpAtk + evSpDef + evSpeed;
        if (totalEVs > 510)
        {
            throw new ArgumentException("Total EV stats must not exceed 510.");
        }

        Name = name;
        PokedexNumber = pokedexNumber;
        TypeOne = typeOne;
        TypeTwo = typeTwo;
        Nature = nature;
        BaseHP = baseHP;
        BaseAtk = baseAtk;
        BaseDef = baseDef;
        BaseSpAtk = baseSpAtk;
        BaseSpDef = baseSpDef;
        BaseSpeed = baseSpeed;
        Level = 1;
        IVHP = ivHP;
        IVAtk = ivAtk;
        IVDef = ivDef;
        IVSpAtk = ivSpAtk;
        IVSpDef = ivSpDef;
        IVSpeed = ivSpeed;
        EVHP = evHP;
        EVAtk = evAtk;
        EVDef = evDef;
        EVSpAtk = evSpAtk;
        EVSpDef = evSpDef;
        EVSpeed = evSpeed;
        if (moves != null)
        {
            Moves = moves;
        }
    }
}
