using System;
using System.Collections.Generic;
using System.IO;

public class PokedexRepository
{
    private static Dictionary<int, Pokemon> Pokedexes = new Dictionary<int, Pokemon>();

    public static void LoadPokedexFromFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                var pokemon = new Pokemon(
                    name: values[0],
                    pokedexNumber: int.Parse(values[1]),
                    typeOne: values[2].ToLower() == "null" ? null : TypeRepository.GetType(values[2]),
                    typeTwo: values[3].ToLower() == "null" ? null : TypeRepository.GetType(values[3]),
                    nature: NatureRepository.GetNature(values[4]),
                    baseHP: int.Parse(values[5]),
                    baseAtk: int.Parse(values[6]),
                    baseDef: int.Parse(values[7]),
                    baseSpAtk: int.Parse(values[8]),
                    baseSpDef: int.Parse(values[9]),
                    baseSpeed: int.Parse(values[10]),
                    ivHP: int.Parse(values[11]),
                    ivAtk: int.Parse(values[12]),
                    ivDef: int.Parse(values[13]),
                    ivSpAtk: int.Parse(values[14]),
                    ivSpDef: int.Parse(values[15]),
                    ivSpeed: int.Parse(values[16]),
                    evHP: int.Parse(values[17]),
                    evAtk: int.Parse(values[18]),
                    evDef: int.Parse(values[19]),
                    evSpAtk: int.Parse(values[20]),
                    evSpDef: int.Parse(values[21]),
                    evSpeed: int.Parse(values[22])
                );
                Pokedexes[pokemon.PokedexNumber] = pokemon;
            }
        }
    }

    public static Pokemon GetPokemon(int pokedexNumber)
    {
        return Pokedexes.ContainsKey(pokedexNumber) ? Pokedexes[pokedexNumber] : null;
    }
}