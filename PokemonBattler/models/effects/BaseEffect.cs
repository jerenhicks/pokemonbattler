using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public abstract class BaseEffect
{
    public abstract void SetModifier(double amount);
    public abstract void SetChance(double chance);
    public abstract void SetRandom(Random random);
    public abstract List<String> PreDamageEffect(Pokemon attacker, Pokemon defender, Move move);
    public abstract List<String> PostDamageEffect(Pokemon attacker, Pokemon defender, Move move, int damageDone);
}
