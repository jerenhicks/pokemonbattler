using System;

public class Program
{
    public static void Main()
    {
        // Create a Magikarp Pokemon with level 1 and specified base stats
        Pokemon magikarp = new Pokemon(
            name: "Magikarp",
            typeOne: "Water",
            typeTwo: null,
            baseHP: 20,
            baseAtk: 10,
            baseDef: 55,
            baseSpAtk: 15,
            baseSpDef: 20,
            baseSpeed: 80,
            ivHP: 0,
            ivAtk: 0,
            ivDef: 0,
            ivSpAtk: 0,
            ivSpDef: 0,
            ivSpeed: 0,
            evHP: 0,
            evAtk: 0,
            evDef: 0,
            evSpAtk: 0,
            evSpDef: 0,
            evSpeed: 0
        );

        magikarp.LevelUp(100);

        // Display the status of Magikarp
        //magikarp.DisplayStatus();
    }
}