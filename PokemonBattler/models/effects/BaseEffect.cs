using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class BaseEffect
{
    public abstract void DoEffect(Pokemon attacker, Pokemon defender, Move move);
}