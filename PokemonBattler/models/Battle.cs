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
            if (Pokemon1.CurrentSpeed > Pokemon2.CurrentSpeed)
            {
                // Pokemon1 attacks first
                PokemonTurn(Pokemon1, Pokemon2);
                PokemonTurn(Pokemon2, Pokemon1);
            }
            else if (Pokemon1.CurrentSpeed < Pokemon2.CurrentSpeed)
            {
                // Pokemon2 attacks first
                PokemonTurn(Pokemon2, Pokemon1);
                PokemonTurn(Pokemon1, Pokemon2);
            }
            else
            {
                // If both Pokemon have the same speed, we will randomly decide who attacks first
                var random = new Random();
                var randomValue = random.Next(0, 2);
                if (randomValue == 0)
                {
                    // Pokemon1 attacks first
                    PokemonTurn(Pokemon1, Pokemon2);
                    PokemonTurn(Pokemon2, Pokemon1);
                }
                else
                {
                    // Pokemon2 attacks first
                    PokemonTurn(Pokemon2, Pokemon1);
                    PokemonTurn(Pokemon1, Pokemon2);
                }
            }
            // Battle logic will be implemented here
            // For now, we will just display the status of both Pokemon
            //Pokemon1.DisplayStatus();
            //Pokemon2.DisplayStatus();

        } while (turn <= TurnLimit && Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0);

        endTime = DateTime.Now;
        var duration = (endTime - startTime).TotalMilliseconds;
        battleLog.Add($"Battle Duration: {duration} milliseconds");
    }

    private void PokemonTurn(Pokemon attackingPokemon, Pokemon defendingPokemon)
    {
        //if the attacking pokemon has no HP left, they can't attack
        if (attackingPokemon.CurrentHP <= 0)
        {
            return;
        }

        //decide what move to pick
        Move moveToUse;
        //if the pokemon has no moves left, struggle. Or if the pokemon has no PP left for any moves, struggle
        if (attackingPokemon.Moves.Count == 0 || attackingPokemon.Moves.TrueForAll(move => move.PP == 0))
        {
            //struggle
            moveToUse = MoveRepository.GetMove("struggle");

            //var damage = attackingPokemon.Attack(defendingPokemon);
            //battleLog.Add($"{attackingPokemon.Name} attacks {defendingPokemon.Name} for {damage} damage");
        }
        else
        {
            //for now, just randomly pick one of the moves listed there. We will implement this later
            Random random = new Random();
            int moveIndex = random.Next(attackingPokemon.Moves.Count);
            moveToUse = attackingPokemon.Moves[moveIndex];
        }
        battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) used {moveToUse.Name}");

        var canHit = CanHit(attackingPokemon, defendingPokemon, moveToUse);
        if (canHit)
        {
            //FIXME: obviously come up with the right calculation for damage
            var damage = CalculateDamage(attackingPokemon, defendingPokemon, moveToUse);
            defendingPokemon.CurrentHP -= damage.Item1;
            battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) attacks {defendingPokemon.Name}({defendingPokemon.ID}) for {damage.Item1} damage");
            if (damage.Item2)
            {
                battleLog.Add("A Critical Hit!");
            }
            if (defendingPokemon.CurrentHP <= 0)
            {
                battleLog.Add($"{defendingPokemon.Name} fainted");
            }
        }
        else
        {
            battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) missed {defendingPokemon.Name}({defendingPokemon.ID})");
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
        //need a number between 1 and 100
        Random random = new Random();
        int randomNumber = random.Next(1, 101);

        if (randomNumber <= accuracyModified)
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

    public (int, Boolean) CalculateDamage(Pokemon attacker, Pokemon defender, Move move)
    {
        var categoryDamage = 0;
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

        if (randomNumber == 1) // 1/24 chance
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

        var Type1Status = 1;
        if (move.Name != "Struggle")
        {
            var effectivenessType1 = move.Type.GetEffectiveness(defender.TypeOne, defender.TypeTwo);
            //var effectivenessType2 = move.Type.GetEffectiveness(defender.TypeTwo);
            //FIXME: obviously come up with the right calculation for damage
        }

        if (move.Category == MoveCategory.Physical)
        {
            categoryDamage = (attacker.Atk / defender.Def);
        }
        else if (move.Category == MoveCategory.Special)
        {
            categoryDamage = (attacker.SpAtk / defender.SpDef);
        }

        var initialDamage = (((2 * attacker.Level / 5 + 2) * move.Power * categoryDamage) / 50) + 2;
        var secondEffects = initialDamage * TargetsStatus * PBStatus * WeatherStatus * GlaiveRushStatus * CriticalHitStatus * RandomStatus * STABStatus * Type1Status * BurnStatus * OtherStatus * ZMoveStatus * TeraShieldStatus;


        if (secondEffects == 0)
        {
            secondEffects = 1;
        }
        //FIXME: THIS IS PROBABLY NOT RIGHT
        return ((int)secondEffects, CriticalHitStatus == 2);
    }
}