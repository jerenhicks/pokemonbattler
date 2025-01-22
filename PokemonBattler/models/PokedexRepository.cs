using System;
using System.Collections.Generic;
using System.IO;

public class PokedexRepository
{
    private static Dictionary<int, PokemonTemplate> Pokedex = new Dictionary<int, PokemonTemplate>();

    public static void LoadPokedexFromFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Trim().Split(',');
                var template = new PokemonTemplate(
                    name: values[0].Trim(),
                    pokedexNumber: int.Parse(values[1]),
                    typeOne: values[2].ToLower() == "null" ? null : TypeRepository.GetType(values[2]),
                    typeTwo: values[3].ToLower() == "null" ? null : TypeRepository.GetType(values[3]),
                    baseHP: int.Parse(values[4]),
                    baseAtk: int.Parse(values[5]),
                    baseDef: int.Parse(values[6]),
                    baseSpAtk: int.Parse(values[7]),
                    baseSpDef: int.Parse(values[8]),
                    baseSpeed: int.Parse(values[9])
                );
                Pokedex[template.PokedexNumber] = template;
            }
        }
    }

    public static bool PokemonExists(int pokedexNumber)
    {
        return Pokedex.ContainsKey(pokedexNumber);
    }

    public static List<int> PokemonIds() { return new List<int>(Pokedex.Keys); }

    public static Pokemon CreatePokemon(int pokedexNumber, Nature nature, int ivHp = 0, int ivAtk = 0, int ivDef = 0, int ivSpAtk = 0, int ivSpDef = 0, int ivSpeed = 0, int evHp = 0, int evAtk = 0, int evDef = 0, int evSpAtk = 0, int evSpDef = 0, int evSpeed = 0, int level = 1)
    {
        return PokemonExists(pokedexNumber) ? new Pokemon(Pokedex[pokedexNumber], nature, ivHp, ivAtk, ivDef, ivSpAtk, ivSpDef, ivSpeed, evHp, evAtk, evDef, evSpAtk, evSpDef, evSpeed, level) : null;
    }

    public static IEnumerable<PokemonTemplate> GetAllPokemonTemplates()
    {
        return Pokedex.Values;
    }
}