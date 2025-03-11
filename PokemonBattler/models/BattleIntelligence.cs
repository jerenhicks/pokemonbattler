using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class BattleIntelligence
{
    private Random random = new Random();

    public void SetRandom(Random random)
    {
        this.random = random;
    }

    public Move PokemonDecideMove(Pokemon attacker, BattleTeam friendlyTeam, BattleTeam enemyTeam)
    {
        var moveToUse = MoveRepository.GetMove("struggle");

        if (attacker.Moves.Count != 0 && attacker.Moves.TrueForAll(move => move.PP != 0))
        {
            //for now, just randomly pick one of the moves listed there. We will implement this later
            //get a list of all the moves that have PP left
            List<Move> movesWithPP = attacker.Moves.FindAll(move => move.PP > 0);
            int moveIndex = random.Next(movesWithPP.Count);
            moveToUse = movesWithPP[moveIndex];
        }

        return moveToUse;
    }

    public Pokemon GetTarget(Pokemon attackingPokemon, Move move, BattleTeam activeTeam, BattleTeam opposingTeam)
    {
        int attackingPokemonPosition = activeTeam.GetPosition(attackingPokemon);
        Pokemon defendingPokemonInPosition = opposingTeam.GetPokemonInPosition(attackingPokemonPosition);
        if (defendingPokemonInPosition.CurrentHP > 0)
        {
            return defendingPokemonInPosition;
        }
        else
        {
            //check sides
            int leftPosition = attackingPokemonPosition - 1;
            int rightPosition = attackingPokemonPosition + 1;
            if (leftPosition > 0)
            {
                Pokemon leftPokemon = opposingTeam.GetPokemonInPosition(leftPosition);
                if (leftPokemon != null && leftPokemon.CurrentHP > 0)
                {
                    return leftPokemon;
                }
                else
                {
                    return opposingTeam.GetPokemonInPosition(rightPosition);
                }
            }
            else
            {
                Pokemon rightPokemon = opposingTeam.GetPokemonInPosition(rightPosition);
                if (rightPokemon != null && rightPokemon.CurrentHP > 0)
                {
                    return rightPokemon;
                }
                else
                {
                    Pokemon rightPos = opposingTeam.GetPokemonInPosition(rightPosition + 1);
                    if (rightPos == null)
                    {
                        return null;
                    }
                    if (rightPos.CurrentHP == 0)
                    {
                        return null;
                    }
                    return opposingTeam.GetPokemonInPosition(rightPosition + 1);
                }
            }
        }
    }
}
