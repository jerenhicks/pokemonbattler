using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public abstract class BaseEffect
{
    public abstract void SetModifier(double amount);
    public abstract List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move);
    public abstract List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone);
}
