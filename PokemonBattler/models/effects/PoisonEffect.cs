using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class PoisonEffect : BaseEffect
{
    public PoisonEffect()
    {
    }

    public override void SetModifier(double amount)
    {
        // No modifier needed for poison
    }
    public override List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }
    public override List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }
}
