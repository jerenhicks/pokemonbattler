using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class RecoilEffect : BaseEffect
{

    public double Modifier { get; set; } = 0;
    public RecoilEffect()
    {
    }

    public override void SetModifier(double amount)
    {
        Modifier = amount;
    }
    public override List<String> DoEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        if (Modifier == 0)
        {
            return new List<string>();
        }
        var damage = (int)(attacker.HP * Modifier);
        attacker.CurrentHP -= damage;
        return new List<String> { $"{attacker.Name} is hit with recoil!" };
    }
}

