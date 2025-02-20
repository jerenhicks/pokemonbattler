using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class BattlePositions
{
    public Pokemon? Side1Position1 { get; private set; }
    public Pokemon? Side1Position2 { get; private set; }
    public Pokemon? Side1Position3 { get; private set; }
    public Pokemon? Side2Position1 { get; private set; }
    public Pokemon? Side2Position2 { get; private set; }
    public Pokemon? Side2Position3 { get; private set; }

    //3v3
    public BattlePositions(Pokemon side1Position1, Pokemon side1Position2, Pokemon side1Position3, Pokemon side2Position1, Pokemon side2Position2, Pokemon side2Position3)
    {
        Side1Position1 = side1Position1;
        Side1Position2 = side1Position2;
        Side1Position3 = side1Position3;
        Side2Position1 = side2Position1;
        Side2Position2 = side2Position2;
        Side2Position3 = side2Position3;
    }

    //1v1
    public BattlePositions(Pokemon side1Position1, Pokemon side2Position1)
    {
        Side1Position1 = side1Position1;
        Side2Position1 = side2Position1;
    }

    //2v2
    public BattlePositions(Pokemon side1Position1, Pokemon side1Position2, Pokemon side2Position1, Pokemon side2Position2)
    {
        Side1Position1 = side1Position1;
        Side1Position2 = side1Position2;
        Side2Position1 = side2Position1;
        Side2Position2 = side2Position2;
    }

    public List<Pokemon> GetParticipants()
    {
        List<Pokemon> participants = new List<Pokemon>();
        if (Side1Position1 != null)
        {
            participants.Add(Side1Position1);
        }
        if (Side1Position2 != null)
        {
            participants.Add(Side1Position2);
        }
        if (Side1Position3 != null)
        {
            participants.Add(Side1Position3);
        }
        if (Side2Position1 != null)
        {
            participants.Add(Side2Position1);
        }
        if (Side2Position2 != null)
        {
            participants.Add(Side2Position2);
        }
        if (Side2Position3 != null)
        {
            participants.Add(Side2Position3);
        }
        return participants;
    }

    public Pokemon GetOpposingPokemon(Pokemon initialPokemon)
    {
        if (Side1Position1 == initialPokemon)
        {
            return Side2Position1;
        }
        if (Side1Position2 == initialPokemon)
        {
            return Side2Position2;
        }
        if (Side1Position3 == initialPokemon)
        {
            return Side2Position3;
        }
        if (Side2Position1 == initialPokemon)
        {
            return Side1Position1;
        }
        if (Side2Position2 == initialPokemon)
        {
            return Side1Position2;
        }
        if (Side2Position3 == initialPokemon)
        {
            return Side1Position3;
        }
        return null;
    }


}
