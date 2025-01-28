using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class GrowlEffect : BaseEffect
{
    public GrowlEffect()
    {
    }

    public override void SetModifier(double amount)
    {
        // No modifier needed for growl
    }

    public override List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        //TODO: Implement the BurnEffect logic
        return new List<String>();
    }

    public override List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        List<String> messages = new List<String>();
        // Implement the effect logic here
        var succeed = defender.StatModifiers.ChangeAtkStage(-1);
        if (succeed)
        {
            messages.Add($"{defender.Name}'s attack fell!");
        }
        else
        {
            messages.Add($"{defender.Name}'s attack can't go any lower!");
        }
        return messages;
    }
}

