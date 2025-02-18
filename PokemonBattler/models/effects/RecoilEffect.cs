using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class RecoilEffect : BaseEffect
{

    public double Modifier { get; set; } = 0;
    public double Chance { get; set; } = 1;
    public Random Random = new Random();
    public RecoilEffect()
    {
    }

    public override void SetChance(double chance)
    {
        this.Chance = chance;
    }

    public override void SetModifier(double amount)
    {
        Modifier = amount;
    }
    public override List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }
    public override List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone)
    {
        if (Modifier == 0)
        {
            return new List<string>();
        }
        var damage = (int)(attacker.HP * Modifier);
        attacker.CurrentHP -= damage;
        return new List<String> { $"{attacker.Name} is hit with recoil!" };
    }

    public override void SetRandom(Random random)
    {
        this.Random = random;
    }
}

