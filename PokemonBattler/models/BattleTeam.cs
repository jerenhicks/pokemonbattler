using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class BattleTeam
{
    public Pokemon Position1 { get; private set; }
    public Pokemon Position2 { get; private set; }
    public Pokemon Position3 { get; private set; }
    public BattleTeam(Pokemon position1, Pokemon position2 = null, Pokemon position3 = null)
    {
        Position1 = position1;
        Position2 = position2;
        Position3 = position3;
    }

    public List<Pokemon> GetTeam()
    {
        List<Pokemon> team = new List<Pokemon>();
        if (Position1 != null)
        {
            team.Add(Position1);
        }
        if (Position2 != null)
        {
            team.Add(Position2);
        }
        if (Position3 != null)
        {
            team.Add(Position3);
        }
        return team;
    }

    public int GetPosition(Pokemon pokemon)
    {
        if (Position1 == pokemon)
        {
            return 1;
        }
        if (Position2 == pokemon)
        {
            return 2;
        }
        if (Position3 == pokemon)
        {
            return 3;
        }
        return -1;
    }

    public Pokemon GetPokemonInPosition(int position)
    {
        if (position == 1)
        {
            return Position1;
        }
        if (position == 2)
        {
            return Position2;
        }
        if (position == 3)
        {
            return Position3;
        }
        return null;
    }

    public bool KnockedOut()
    {
        return (Position1.CurrentHP <= 0) && (Position2 != null ? Position2.CurrentHP <= 0 : true) && (Position3 != null ? Position3.CurrentHP <= 0 : true);
    }

    public bool Equals(BattleTeam other)
    {
        if (other == null)
        {
            return false;
        }

        return Equals(Position1, other.Position1) &&
               Equals(Position2, other.Position2) &&
               Equals(Position3, other.Position3);
    }

    public override bool Equals(object obj)
    {
        if (obj is BattleTeam other)
        {
            return Equals(other);
        }
        return false;
    }

    public override String ToString()
    {
        String outputString = $"{Position1.Name}";
        if (Position2 != null)
        {
            outputString += $"/{Position2.Name}";
        }
        if (Position3 != null)
        {
            outputString += $"/{Position3.Name}";
        }
        return outputString;
    }
}
