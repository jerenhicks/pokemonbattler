using System.Collections.Generic;

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
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                var nature = new Nature(
                    name: values[0].Trim(),
                    attackModifier: double.Parse(values[1]),
                    defenseModifier: double.Parse(values[2]),
                    specialAttackModifier: double.Parse(values[3]),
                    specialDefenseModifier: double.Parse(values[4]),
                    speedModifier: double.Parse(values[5])
                );
                if (!Natures.ContainsKey(nature.Name.ToLower()))
                {
                    Natures.Add(nature.Name.ToLower(), nature);
                }
            }
        }
    }
}