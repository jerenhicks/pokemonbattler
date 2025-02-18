using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class AbsorbEffect : BaseEffect
{
    public double Modifier { get; set; } = 0;
    public double Chance { get; set; } = 1;
    public Random Random = new Random();
    public AbsorbEffect()
    {
    }

    public override void SetChance(double chance)
    {
        Chance = chance;
    }

    public override List<string> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone)
    {
        var modifiedDamage = (int)(damageDone * Modifier);
        attacker.CurrentHP += modifiedDamage;
        if (attacker.CurrentHP > attacker.HP)
        {
            attacker.CurrentHP = attacker.HP;
        }
        return new List<String> { $"{defender.Name} has had it's energy drained!" };
    }

    public override List<string> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        return new List<string>();
    }

    public override void SetModifier(double amount)
    {
        this.Modifier = amount;
    }

    public override void SetRandom(Random random)
    {
        this.Random = random;
    }
}
