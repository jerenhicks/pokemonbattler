using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class TypeRepository
{
    private static readonly Dictionary<string, Type> Types = new Dictionary<string, Type>();

    public static void LoadTypesFromFile(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var types = JsonConvert.DeserializeObject<List<Type>>(jsonData);


        foreach (var type in types)
        {
            Types[type.Name.ToLower()] = type;
        }

        foreach (var type in types)
        {
            type.ConnectTypes();
        }
    }

    public static Type GetType(string name)
    {
        return Types[name.ToLower()];
    }

    public static IEnumerable<Type> GetAllTypes()
    {
        return Types.Values;
    }

    public static void SavePokedexToFile(string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(Types.Values, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}