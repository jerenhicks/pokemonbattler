using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class OpponentAttackEffect : BaseEffect
{
    public double Modifier { get; set; } = 0;
    public double Chance { get; set; } = 1;
    public Random Random = new Random();

    public OpponentAttackEffect()
    {
    }

    public override void SetChance(double chance)
    {
        this.Chance = chance;
    }

    public override void SetModifier(double amount)
    {
        this.Modifier = amount;
    }

    public override List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }

    public override List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone)
    {
        // Implement the effect logic here
        {
            List<String> messages = new List<String>();
            // Generate a random number between 0.0 and 1.0
            double randomValue = Random.NextDouble();

            // Check if the random value falls within the chance percentage
            if (randomValue <= this.Chance)
            {
                var succeed = defender.StatModifiers.ChangeAtkStage((int)this.Modifier);
                if (succeed)
                {
                    if (this.Modifier > 0)
                    {
                        if (this.Modifier > 1)
                        {
                            messages.Add($"{defender.Name}'s attack rose sharply!");
                        }
                        else
                        {
                            messages.Add($"{defender.Name}'s attack rose!");
                        }
                    }
                    else
                    {
                        if (this.Modifier < -1)
                        {
                            messages.Add($"{defender.Name}'s attack fell sharply!");
                        }
                        else
                        {
                            messages.Add($"{defender.Name}'s attack fell!");
                        }
                    }
                }
                else
                {
                    if (this.Modifier > 0)
                    {
                        messages.Add($"{defender.Name}'s attack can't go any higher!");
                    }
                    else
                    {
                        messages.Add($"{defender.Name}'s attack can't go any lower!");
                    }
                }
            }
            return messages;
        }
    }
    public override void SetRandom(Random random)
    {
        this.Random = random;
    }
}

