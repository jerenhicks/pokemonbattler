using System;
using System.Collections.Generic;

public class Battle
{
    private int TurnLimit { get; }
    private List<string> battleLog = new List<string>();
    private DateTime startTime;
    private DateTime endTime;

    private Random random = new Random();
    public GenerationBattleData BattleData { get; set; }

    private Dictionary<Pokemon, int> badlyPoisonedTracker = new Dictionary<Pokemon, int>();

    public BattleTeam Team1 { get; set; }
    public BattleTeam Team2 { get; set; }
    private List<Pokemon> PokemonParticipants { get; set; }
    private List<Pokemon> PokemonFainted = new List<Pokemon>();

    public Battle(BattleTeam team1, BattleTeam team2, GenerationBattleData battleData, int turnLimit = 100)
    {
        Team1 = team1;
        Team2 = team2;
        PokemonParticipants = new List<Pokemon>();
        PokemonParticipants.AddRange(Team1.GetTeam());
        PokemonParticipants.AddRange(Team2.GetTeam());
        TurnLimit = turnLimit;
        BattleData = battleData;
    }

    public void CommenceBattle()
    {
        foreach (var pokemon in PokemonParticipants)
        {
            pokemon.Reset();
        }


        startTime = DateTime.Now;
        var turn = 0;
        do
        {
            turn++;
            //change this to have all the pokemon's stats shown instead of 2.
            var outputString = $"Turn {turn}: ";

            outputString += $"Team 1: ";
            foreach (var pokemon in Team1.GetTeam())
            {
                outputString += $"{pokemon.Name} HP: {pokemon.CurrentHP}/{pokemon.HP} ";
            }
            outputString += $"Team 2: ";
            foreach (var pokemon in Team2.GetTeam())
            {
                outputString += $"{pokemon.Name} HP: {pokemon.CurrentHP}/{pokemon.HP} ";
            }
            battleLog.Add(outputString);



            //have both pokemon pick their attacks
            Dictionary<Pokemon, Move> decidedMoves = new Dictionary<Pokemon, Move>();
            foreach (var pokemon in PokemonParticipants)
            {
                decidedMoves.Add(pokemon, PokemonDecideMove(pokemon));
            }


            //decide who goes first
            var turnOrder = GetTurnOrder(decidedMoves);

            foreach (var pokemon in turnOrder)
            {
                BattleTeam activeTeam = Team1.GetTeam().Contains(pokemon) ? Team1 : Team2;
                BattleTeam opposingTeam = Team1.GetTeam().Contains(pokemon) ? Team2 : Team1;
                if (pokemon.CurrentHP > 0)
                {
                    var target = GetTarget(pokemon, decidedMoves[pokemon], activeTeam, opposingTeam);
                    PokemonTurn(pokemon, decidedMoves[pokemon], target);
                    CheckFaintedAll();
                }
            }

            if (PokemonParticipants.TrueForAll(pokemon => pokemon.CurrentHP > 0))
            {
                foreach (var pokemon in PokemonParticipants)
                {
                    CheckPoison(pokemon);
                }
                foreach (var pokemon in PokemonParticipants)
                {
                    CheckBurn(pokemon);
                }
                CheckFaintedAll();
            }

            //if both pokemon are still alive, check burn condition
            // if (Pokemon1.CurrentHP > 0 && Pokemon2.CurrentHP > 0)
            // {
            //     CheckPoison(Pokemon1);
            //     CheckPoison(Pokemon2);
            //     CheckBurn(Pokemon1);
            //     CheckBurn(Pokemon2);
            //     //only check for faints after both pokemon have taken their burn damage
            //     CheckFainted(Pokemon1, Pokemon2);
            // }


        } while (turn <= TurnLimit && !(Team1.KnockedOut() || Team2.KnockedOut()));

        endTime = DateTime.Now;
        var duration = (endTime - startTime).TotalMilliseconds;

        var endresultString = $"Results {turn}: ";

        endresultString += $"Team 1: ";
        foreach (var pokemon in Team1.GetTeam())
        {
            endresultString += $"{pokemon.Name} HP: {pokemon.CurrentHP}/{pokemon.HP} ";
        }
        endresultString += $"Team 2: ";
        foreach (var pokemon in Team2.GetTeam())
        {
            endresultString += $"{pokemon.Name} HP: {pokemon.CurrentHP}/{pokemon.HP} ";
        }
        battleLog.Add(endresultString);

        battleLog.Add($"Battle Duration: {duration} milliseconds");
    }

    public void PokemonTurn(Pokemon attackingPokemon, Move attackerMove, Pokemon defendingPokemon)
    {
        //check if frozen
        if (CheckFrozen(attackingPokemon))
        {
            return;
        }
        if (CheckParalysis(attackingPokemon))
        {
            return;
        }

        battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) used {attackerMove.Name}");
        if (defendingPokemon == null)
        {
            battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) used {attackerMove.Name} but there was no target");
            return;
        }



        var canHit = BattleData.CanHit(attackingPokemon, defendingPokemon, attackerMove, random);
        if (canHit)
        {
            var damage = 0;
            //Do pre damage effects
            if (attackerMove.Effects.Count > 0)
            {
                foreach (BaseEffect effect in attackerMove.Effects)
                {
                    foreach (var log in effect.PreDamageEffect(attackingPokemon, defendingPokemon, attackerMove))
                    {
                        battleLog.Add(log);
                    }
                }
            }

            if (attackerMove.IsNonDamage)
            {
                //maybe there is something else we need to do here? for now just kick it out. 
                battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) used {attackerMove.Name}");
            }
            else
            {
                var damageResult = CalculateDamage(attackingPokemon, defendingPokemon, attackerMove);
                damage = damageResult.Item1;
                defendingPokemon.CurrentHP -= damageResult.Item1;
                if (defendingPokemon.CurrentHP < 0)
                {
                    defendingPokemon.CurrentHP = 0;
                }
                battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) attacks {defendingPokemon.Name}({defendingPokemon.ID}) for {damageResult.Item1} damage");
                if (damageResult.Item2)
                {
                    battleLog.Add("A Critical Hit!");
                }
                CheckTypeEffectiveness(damageResult.Item3);

            }

            //Do post damage effects
            if (attackerMove.Effects.Count > 0)
            {
                foreach (BaseEffect effect in attackerMove.Effects)
                {
                    foreach (var log in effect.PostDamageEffect(attackingPokemon, defendingPokemon, attackerMove, damage))
                    {
                        battleLog.Add(log);
                    }
                }
            }
        }
        else
        {
            battleLog.Add($"{attackingPokemon.Name}({attackingPokemon.ID}) missed {defendingPokemon.Name}({defendingPokemon.ID})");
        }
        attackerMove.MoveUsed();
    }

    public void CheckTypeEffectiveness(double effectiveness)
    {

        if (effectiveness == 2 || effectiveness == 4)
        {
            battleLog.Add("It's super effective!");
        }
        else if (effectiveness == 0.25 || effectiveness == .5)
        {
            battleLog.Add("It's not very effective...");
        }
        else if (effectiveness == 0)
        {
            battleLog.Add("It has no effect!");
        }
    }

    public void CheckBurn(Pokemon pokemon)
    {
        if (pokemon.NonVolatileStatus == NonVolatileStatus.Burn)
        {
            pokemon.CurrentHP -= (int)Math.Floor(pokemon.HP * 0.0625);
            battleLog.Add($"{pokemon.Name} is hurt by burn!");
        }
    }

    public bool CheckFrozen(Pokemon pokemon)
    {
        if (pokemon.NonVolatileStatus == NonVolatileStatus.Freeze)
        {
            int randomNumber = random.Next(1, 101);
            if (randomNumber <= 20)
            {
                pokemon.ResetNonVolatileStatuses();
                battleLog.Add($"{pokemon.Name} thawed out!");
                return false;
            }
            else
            {
                battleLog.Add($"{pokemon.Name} is frozen solid!");
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckParalysis(Pokemon pokemon)
    {
        if (pokemon.NonVolatileStatus == NonVolatileStatus.Paralysis)
        {
            int randomNumber = random.Next(1, 101);
            if (randomNumber <= 25)
            {
                battleLog.Add($"{pokemon.Name} couldn't move because it's paralyzed");
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void CheckPoison(Pokemon pokemon)
    {
        if (pokemon.NonVolatileStatus == NonVolatileStatus.Poison)
        {
            pokemon.CurrentHP -= (int)Math.Floor(pokemon.HP * 0.125);
            battleLog.Add($"{pokemon.Name} is hurt by poison!");
        }
    }

    public void CheckBadlyPoison(Pokemon pokemon)
    {
        if (pokemon.NonVolatileStatus == NonVolatileStatus.Badly_Poisoned)
        {
            if (!badlyPoisonedTracker.ContainsKey(pokemon))
            {
                badlyPoisonedTracker.Add(pokemon, 1);
            }
            else
            {
                badlyPoisonedTracker[pokemon]++;
            }
            pokemon.CurrentHP -= (int)Math.Floor(pokemon.HP * (0.0625 * badlyPoisonedTracker[pokemon]));
            battleLog.Add($"{pokemon.Name} is hurt by poison!");
        }
    }

    public void CheckFaintedAll() //check if all pokemon have fainted
    {
        var allFainted = true;
        foreach (var pokemon in PokemonParticipants)
        {
            if (pokemon.CurrentHP <= 0)
            {
                if (!PokemonFainted.Contains(pokemon))
                {
                    battleLog.Add($"{pokemon.Name} fainted!");
                    PokemonFainted.Add(pokemon);
                }
            }
            else
            {
                allFainted = false;
            }
        }
        if (allFainted)
        {
            battleLog.Add("All Pokemon have fainted!");
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

    public List<Pokemon> GetTurnOrder(Dictionary<Pokemon, Move> moves)
    {
        List<Pokemon> turnOrder = new List<Pokemon>();
        //check to see move priority, highest gets added first. If the same, then check speed. If the same, then randomly decide
        foreach (var pokemon in moves.Keys)
        {
            if (turnOrder.Count == 0)
            {
                turnOrder.Add(pokemon);
            }
            else
            {
                var move = moves[pokemon];
                var added = false;
                for (int i = 0; i < turnOrder.Count; i++)
                {
                    if (move.Priority > moves[turnOrder[i]].Priority)
                    {
                        turnOrder.Insert(i, pokemon);
                        added = true;
                        break;
                    }
                    else if (move.Priority == moves[turnOrder[i]].Priority)
                    {
                        if (pokemon.CurrentSpeed > turnOrder[i].CurrentSpeed)
                        {
                            turnOrder.Insert(i, pokemon);
                            added = true;
                            break;
                        }
                        else if (pokemon.CurrentSpeed == turnOrder[i].CurrentSpeed)
                        {
                            int randomValue = random.Next(0, 2);
                            if (randomValue == 0)
                            {
                                turnOrder.Insert(i, pokemon);
                                added = true;
                                break;
                            }
                        }
                    }
                }
                if (!added)
                {
                    turnOrder.Add(pokemon);
                }
            }
        }

        return turnOrder;
    }

    public List<string> GetBattleLog()
    {
        return battleLog;
    }



    public (int, Boolean, double) CalculateDamage(Pokemon attacker, Pokemon defender, Move move, int criticalOverride = 0)
    {
        double categoryDamage = 0;
        var BurnStatus = 1.0;
        if (attacker.NonVolatileStatus == NonVolatileStatus.Burn && move.Category == MoveCategory.Physical)
        {
            BurnStatus = 0.5;
        }

        var TargetsStatus = 1;
        var WeatherStatus = 1;

        var CriticalHitStatus = 1;


        var PBStatus = 1;
        var GlaiveRushStatus = 1;
        var OtherStatus = 1;
        var ZMoveStatus = 1;
        var TeraShieldStatus = 1;

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
        if (attacker.TypeOne == move.Type || attacker.TypeTwo == move.Type)
        {
            STABStatus = 1.5;
        }

        var Type1Status = 1.0;
        Type1Status = move.Type.GetEffectiveness(defender.TypeOne, defender.TypeTwo);


        if (move.Category == MoveCategory.Physical)
        {
            categoryDamage = (double)attacker.CurrentAtk / defender.CurrentDef;
        }
        else if (move.Category == MoveCategory.Special)
        {
            categoryDamage = (double)attacker.CurrentSpAtk / defender.CurrentSpDef;
        }

        double initialDamage = (((2 * attacker.Level / 5 + 2) * (move.Power ?? 0) * categoryDamage) / 50) + 2;
        var secondEffects = initialDamage * TargetsStatus * PBStatus * WeatherStatus * GlaiveRushStatus * CriticalHitStatus * RandomStatus * STABStatus * Type1Status * BurnStatus * OtherStatus * ZMoveStatus * TeraShieldStatus;


        if (secondEffects == 0 && Type1Status != 0)
        {
            secondEffects = 1;
        }
        //FIXME: THIS IS PROBABLY NOT RIGHT
        return ((int)secondEffects, CriticalHitStatus == 2, Type1Status);
    }

    public void SetRandom(Random random)
    {
        this.random = random;
    }

    public void SetBattleLog(List<string> log)
    {
        battleLog = log;
    }

}