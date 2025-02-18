using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


public class OpponentDefenseEffect : BaseEffect
{
    public double Modifier { get; set; } = 0;
    public double Chance { get; set; } = 1;
    public Random Random = new Random();
    public OpponentDefenseEffect()
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
        List<string> messages = new List<string>();

        // Generate a random number between 0.0 and 1.0
        double randomValue = Random.NextDouble();

        // Check if the random value falls within the chance percentage
        if (randomValue <= this.Chance)
        {
            // Attempt to lower the defense stage
            var succeed = defender.StatModifiers.ChangeDefStage((int)this.Modifier);
            if (succeed)
            {
                if (this.Modifier > 0)
                {
                    if (this.Modifier > 1)
                    {
                        messages.Add($"{defender.Name}'s defense rose sharply!");
                    }
                    else
                    {
                        messages.Add($"{defender.Name}'s defense rose!");
                    }
                }
                else
                {
                    if (this.Modifier < -1)
                    {
                        messages.Add($"{defender.Name}'s defense fell sharply!");
                    }
                    else
                    {
                        messages.Add($"{defender.Name}'s defense fell!");
                    }
                }
            }
            else
            {
                if (this.Modifier > 0)
                {
                    messages.Add($"{defender.Name}'s defense can't go any higher!");
                }
                else
                {
                    messages.Add($"{defender.Name}'s defense can't go any lower!");
                }
            }
        }

        return messages;
    }

    public override void SetRandom(Random random)
    {
        this.Random = random;
    }
}

