using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Metric
{

    public Pokemon Pokemon { get; set; }
    public List<Pokemon> WinsAgainst { get; set; }
    public List<Pokemon> LossesAgainst { get; set; }
    public List<Pokemon> TiesAgainst { get; set; }

    public int Wins => WinsAgainst.Count();
    public int Losses => LossesAgainst.Count();
    public int Ties => TiesAgainst.Count();

}
