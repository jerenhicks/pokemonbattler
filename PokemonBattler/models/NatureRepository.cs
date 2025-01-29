using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class NatureRepository
{

    private static readonly Dictionary<string, Nature> Natures = new Dictionary<string, Nature>();

    public static void AddNature(Nature nature)
    {
        Natures.Add(nature.Name.ToLower(), nature);
    }

    public static IEnumerable<Nature> GetAllNatures()
    {
        return Natures.Values;
    }

    public static Nature GetNature(string name)
    {
        return Natures[name.ToLower()];
    }

    public static void LoadNaturesFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var natures = JsonConvert.DeserializeObject<List<Nature>>(jsonData);

        foreach (var nature in natures)
        {
            if (!Natures.ContainsKey(nature.Name.ToLower()))
            {
                Natures.Add(nature.Name.ToLower(), nature);
            }
        }
    }

    public static void SaveNaturesToFile(string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(Natures.Values, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}