using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class RecoilEffect : BaseEffect
{
    public override List<String> DoEffect(Pokemon attacker, Pokemon defender, Move move)
    {

        if (move.Name.ToLower() == "struggle")
        {
            var damage = (int)(attacker.HP * 0.25);
            attacker.CurrentHP -= damage;
            return new List<String> { $"{attacker.Name} is hit with recoil!" };
        }
        else if (move.Name.ToLower() == "brave bird")
        {
            var damage = (int)(attacker.HP * 0.33);
            attacker.CurrentHP -= damage;
            return new List<String> { $"{attacker.Name} is hit with recoil!" };
        }

        return new List<String>();
    }
}

