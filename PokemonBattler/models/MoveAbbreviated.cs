using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MoveAbbreviated
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public MoveAbbreviated(int id, string name)
    {
        Id = id;
        Name = name;
    }
}