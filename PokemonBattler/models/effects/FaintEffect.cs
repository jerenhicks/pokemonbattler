using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FaintEffect : BaseEffect
{
    public FaintEffect()
    {
    }

    public override void SetModifier(double amount)
    {
        // No modifier needed for burn
    }

    public override List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }

    public override List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone)
    {
        attacker.CurrentHP = 0;
        return new List<String>();
    }
}
