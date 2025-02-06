using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class GenerationBattleData
{
    public abstract bool CanHit(Pokemon attacker, Pokemon defender, Move move, Random random);


}