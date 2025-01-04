using System;
using System.Collections.Generic;
using System.IO;

public static class TypeRepository
{
    private static readonly Dictionary<string, Type> Types = new Dictionary<string, Type>();

    public static void LoadTypesFromFile(string filePath)
    {
        // First pass: Create all types
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                var typeName = values[0].ToLower();
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
                var typeName = values[0];
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
                                break;
                            case ".5":
                                type.NotEffectiveAgainst.Add(affectedType);
                                break;
                            case "0":
                                type.NoEffectAgainst.Add(affectedType);
                                break;
                        }
                    }
                }
            }
        }
    }

    public static Type GetType(string name)
    {
        return Types[name.ToLower()];
    }
}