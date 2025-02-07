using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class PokedexRepository
{
    private static Dictionary<string, PokemonTemplate> Pokedex = new Dictionary<string, PokemonTemplate>();

    public static void LoadPokedexFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var pokemonTemplates = JsonConvert.DeserializeObject<List<PokemonTemplate>>(jsonData);


        foreach (var template in pokemonTemplates)
        {
            Pokedex[template.PokedexNumberString] = template;
        }
    }

    public static void SavePokedexToFile(string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(Pokedex.Values, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }

    public static bool PokemonExists(string pokedexNumber)
    {
        return Pokedex.ContainsKey(pokedexNumber);
    }

    public static List<string> PokemonIds() { return new List<string>(Pokedex.Keys); }

    public static Pokemon CreatePokemon(string pokedexNumber, Nature nature, Generation generation = Generation.NINE, int ivHp = 0, int ivAtk = 0, int ivDef = 0, int ivSpAtk = 0, int ivSpDef = 0, int ivSpeed = 0, int evHp = 0, int evAtk = 0, int evDef = 0, int evSpAtk = 0, int evSpDef = 0, int evSpeed = 0, int level = 1)
    {
        return PokemonExists(pokedexNumber) ? new Pokemon(Pokedex[pokedexNumber], nature, generation, ivHp, ivAtk, ivDef, ivSpAtk, ivSpDef, ivSpeed, evHp, evAtk, evDef, evSpAtk, evSpDef, evSpeed, level) : null;
    }

    public static IEnumerable<PokemonTemplate> GetAllPokemonTemplates()
    {
        return Pokedex.Values;
    }

    public static PokemonTemplate GetPokemonTemplateByName(string name)
    {
        return Pokedex.Values.FirstOrDefault(p => p.Name.ToLower().Replace(":", "").Replace("é", "e").Replace("-", "").Replace("'", "").Replace(".", "").Replace(" ", "") == name.ToLower().Replace(":", "").Replace("é", "e").Replace("-", "").Replace("'", "").Replace(".", "").Replace(" ", ""));
    }
}