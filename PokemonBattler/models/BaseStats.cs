using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class BaseStats
{

    public List<Generation> ValidForGeneration { get; private set; }
    public int BaseHP { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseSpAtk { get; private set; }
    public int BaseSpDef { get; private set; }
    public int BaseSpeed { get; private set; }

    public BaseStats(List<Generation> validForGeneration, int baseHP, int baseAtk, int baseDef, int baseSpAtk, int baseSpDef, int baseSpeed)
    {
        ValidForGeneration = validForGeneration;
        BaseHP = baseHP;
        BaseAtk = baseAtk;
        BaseDef = baseDef;
        BaseSpAtk = baseSpAtk;
        BaseSpDef = baseSpDef;
        BaseSpeed = baseSpeed;
    }
}
