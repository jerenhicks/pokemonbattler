using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Metric
{

    public BattleTeam Team { get; set; }
    public List<BattleTeam> WinsAgainst { get; set; }
    public List<BattleTeam> LossesAgainst { get; set; }
    public List<BattleTeam> TiesAgainst { get; set; }

    public int Wins => WinsAgainst.Count();
    public int Losses => LossesAgainst.Count();
    public int Ties => TiesAgainst.Count();

}
