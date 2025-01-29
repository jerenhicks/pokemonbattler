using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class TypeRepository
{
    private static readonly Dictionary<string, Type> Types = new Dictionary<string, Type>();

    public static void LoadTypesFromFileOLD(string filePath)
    {
        // First pass: Create all types
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                var typeName = values[0].Trim().ToLower();
                if (!Types.ContainsKey(typeName))
                {
                    Types[typeName] = new Type(typeName);
                }
            }
        }

        // Second pass: Populate effectiveness lists
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                var typeName = values[0].Trim().ToLower();
                var type = Types[typeName];

                foreach (var effectiveness in values[1..])
                {
                    var parts = effectiveness.Split(':');
                    var multiplier = parts[0];
                    var affectedTypes = parts[1].Split('/');

                    foreach (var affectedTypeName in affectedTypes)
                    {
                        var affectedType = Types[affectedTypeName];
                        switch (multiplier)
                        {
                            case "2":
                                type.SuperEffectiveAgainst.Add(affectedType);
                                type.SuperEffectiveAgainstNames.Add(affectedTypeName);
                                break;
                            case ".5":
                                type.NotEffectiveAgainst.Add(affectedType);
                                type.NotEffectiveAgainstNames.Add(affectedTypeName);
                                break;
                            case "0":
                                type.NoEffectAgainst.Add(affectedType);
                                type.NoEffectAgainstNames.Add(affectedTypeName);
                                break;
                        }
                    }
                }
            }
        }
    }

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
        Console.WriteLine("Types loaded successfully.");
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