using System;
using System.Collections.Generic;

public class Battle
{
    private Pokemon Pokemon1 { get; }
    private Pokemon Pokemon2 { get; }
    private int TurnLimit { get; }
    private List<string> battleLog = new List<string>();
    private DateTime startTime;
    private DateTime endTime;

    public Battle(Pokemon pokemon1, Pokemon pokemon2, int turnLimit = 100)
    {
        Pokemon1 = pokemon1;
        Pokemon2 = pokemon2;
        TurnLimit = turnLimit;
    }

    public void CommenceBattle()
    {
        Pokemon1.BattleReady();
        Pokemon2.BattleReady();

        startTime = DateTime.Now;
        var turn = 0;
        do
        {
            turn++;
            battleLog.Add($"Turn {turn}: Pokemon1: {Pokemon1.Name} HP: {Pokemon1.CurrentHP}/{Pokemon1.HP} Pokemon2: {Pokemon2.Name} HP: {Pokemon2.CurrentHP}/{Pokemon2.HP}");



            //have both pokemon pick their attacks
            var move1 = PokemonDecideMove(Pokemon1);
            var move2 = PokemonDecideMove(Pokemon2);

            //decide who goes first
            var whoGoesFirst = DecideWhoGoesFirst(Pokemon1, Pokemon2, move1, move2);

            if (whoGoesFirst == 1)
            {
                if (Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0)
                {
                    PokemonTurn(Pokemon1, move1, Pokemon2);
                    CheckFainted(Pokemon1, Pokemon2);
                }
                if (Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0)
                {
                    PokemonTurn(Pokemon2, move2, Pokemon1);
                    CheckFainted(Pokemon1, Pokemon2);
                }
            }
            else
            {
                if (Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0)
                {
                    PokemonTurn(Pokemon2, move2, Pokemon1);
                    CheckFainted(Pokemon1, Pokemon2);
                }
                if (Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0)
                {
                    PokemonTurn(Pokemon1, move1, Pokemon2);
                    CheckFainted(Pokemon1, Pokemon2);
                }
            }


        } while (turn <= TurnLimit && Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0);

        endTime = DateTime.Now;
        var duration = (endTime - startTime).TotalMilliseconds;
        battleLog.Add($"Battle Duration: {duration} milliseconds");
    }

    private void PokemonTurn(Pokemon attackingPokemon, Move attackerMove, Pokemon defendingPokemon)
    {
        battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) used {attackerMove.Name}");

        var canHit = CanHit(attackingPokemon, defendingPokemon, attackerMove);
        if (canHit)
        {
            var damage = CalculateDamage(attackingPokemon, defendingPokemon, attackerMove);
            defendingPokemon.CurrentHP -= damage.Item1;
            battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) attacks {defendingPokemon.Name}({defendingPokemon.ID}) for {damage.Item1} damage");
            if (damage.Item2)
            {
                battleLog.Add("A Critical Hit!");
            }

        }
        else
        {
            battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) missed {defendingPokemon.Name}({defendingPokemon.ID})");
        }
        attackerMove.MoveUsed();
    }

    public void CheckFainted(Pokemon pokemon1, Pokemon pokemon2)
    {
        if (pokemon1.CurrentHP <= 0)
        {
            battleLog.Add($"{pokemon1.Name} fainted!");
        }
        if (pokemon2.CurrentHP <= 0)
        {
            battleLog.Add($"{pokemon2.Name} fainted!");
        }
    }

    public Move PokemonDecideMove(Pokemon pokemon)
    {
        var moveToUse = MoveRepository.GetMove("struggle");

        if (pokemon.Moves.Count != 0 && pokemon.Moves.TrueForAll(move => move.PP != 0))
        {
            //for now, just randomly pick one of the moves listed there. We will implement this later
            //get a list of all the moves that have PP left
            List<Move> movesWithPP = pokemon.Moves.FindAll(move => move.PP > 0);
            Random random = new Random();
            int moveIndex = random.Next(movesWithPP.Count);
            moveToUse = movesWithPP[moveIndex];
        }

        return moveToUse;
    }

    public int DecideWhoGoesFirst(Pokemon pokemon1, Pokemon pokemon2, Move move1, Move move2)
    {
        //first thing is to check the priority of the moves. Highest priority goes first.
        if (move1.Priority > move2.Priority)
        {
            return 1;
        }
        else if (move1.Priority < move2.Priority)
        {
            return 2;
        }
        else
        {
            //if the moves have the same priority, then we check the speed of the pokemon
            if (pokemon1.CurrentSpeed > pokemon2.CurrentSpeed)
            {
                return 1;
            }
            else if (pokemon1.CurrentSpeed < pokemon2.CurrentSpeed)
            {
                return 2;
            }
            else
            {
                //if the pokemon have the same speed, then we randomly decide who goes first
                Random random = new Random();
                int randomValue = random.Next(0, 2);
                if (randomValue == 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
    }

    public List<string> GetBattleLog()
    {
        return battleLog;
    }

    public bool CanHit(Pokemon attacker, Pokemon defender, Move move)
    {
        //these are always hit moves.
        if (move.Accuracy == null)
        {
            return true;
        }
        //check if the move hits
        //for now, we will just return true
        var accuracyModified = move.Accuracy * GetAccuracyModifiers(attacker, defender, move) * GetAdjustedStages(attacker, defender, move) * GetMiracleBerry(attacker, defender, move) - GetAffection(attacker, defender, move);
        //FIXME: PROBABLY NOT RIGHT HERE, but maybe????
        var accuracy = accuracyModified * 100;
        //need a number between 1 and 100
        Random random = new Random();
        int randomNumber = random.Next(1, 101);

        if (randomNumber <= accuracy)
        {
            return true;
        }
        return false;
    }

    public int GetAccuracyModifiers(Pokemon attacker, Pokemon defender, Move move)
    {
        var startValue = 4096;
        //likely some things will change this.
        var finalValue = startValue / 4096;
        return finalValue;
    }

    public int GetAdjustedStages(Pokemon attack, Pokemon defender, Move move)
    {
        return 1;
    }

    public int GetMiracleBerry(Pokemon attack, Pokemon defender, Move move)
    {
        return 1;
    }

    public int GetAffection(Pokemon attack, Pokemon defender, Move move)
    {
        return 0;
    }

    public (int, Boolean) CalculateDamage(Pokemon attacker, Pokemon defender, Move move, int criticalOverride = 0)
    {
        double categoryDamage = 0;
        var BurnStatus = 1;

        var TargetsStatus = 1;
        var WeatherStatus = 1;

        var CriticalHitStatus = 1;


        var PBStatus = 1;
        var GlaiveRushStatus = 1;
        var OtherStatus = 1;
        var ZMoveStatus = 1;
        var TeraShieldStatus = 1;

        Random random = new Random();
        var RandomStatus = random.Next(85, 101) / 100.0;

        var criticalStage = 0;
        var randomNumber = 0;
        if (criticalStage == 0)
        {
            randomNumber = random.Next(1, 25);
        }
        else if (criticalStage == 1)
        {
            randomNumber = random.Next(1, 9);
        }
        else if (criticalStage == 2)
        {
            randomNumber = random.Next(1, 3);
        }
        else if (criticalStage >= 3)
        {
            randomNumber = random.Next(1, 1);
        }

        if (randomNumber == 1 && criticalOverride == 0) // 1/24 chance
        {
            CriticalHitStatus = 2; // Critical hit doubles the damage
        }

        var STABStatus = 1.0;
        if (move.Name != "Struggle")
        {
            if (attacker.TypeOne == move.Type || attacker.TypeTwo == move.Type)
            {
                STABStatus = 1.5;
            }

        }

        var Type1Status = 1.0;
        if (move.Name != "Struggle")
        {
            Type1Status = move.Type.GetEffectiveness(defender.TypeOne, defender.TypeTwo);
        }

        if (move.Category == MoveCategory.Physical)
        {
            categoryDamage = (double)attacker.CurrentAtk / defender.CurrentDef;
        }
        else if (move.Category == MoveCategory.Special)
        {
            categoryDamage = (double)attacker.CurrentSpAtk / defender.CurrentSpDef;
        }

        double initialDamage = (((2 * attacker.Level / 5 + 2) * move.Power * categoryDamage) / 50) + 2;
        var secondEffects = initialDamage * TargetsStatus * PBStatus * WeatherStatus * GlaiveRushStatus * CriticalHitStatus * RandomStatus * STABStatus * Type1Status * BurnStatus * OtherStatus * ZMoveStatus * TeraShieldStatus;


        if (secondEffects == 0)
        {
            secondEffects = 1;
        }
        //FIXME: THIS IS PROBABLY NOT RIGHT
        return ((int)secondEffects, CriticalHitStatus == 2);
    }
}