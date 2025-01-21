using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class GrowlEffect : BaseEffect
{
    public override void DoEffect(Pokemon attacker, Pokemon defender, Move move)
    {
        // Implement the effect logic here
        defender.StatModifiers.ChangeAtkStage(-1);
    }
}
