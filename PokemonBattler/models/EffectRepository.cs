using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


public static class EffectRepository
{
    private static readonly Dictionary<string, BaseEffect> Effects = new Dictionary<string, BaseEffect>();

    static EffectRepository()
    {
        LoadEffectsFromAssembly();
    }

    public static void AddEffect(BaseEffect effect)
    {
        Effects.Add(effect.GetType().Name.ToLower(), effect);
    }

    public static BaseEffect GetEffect(string name)
    {
        return Effects[name.ToLower()];
    }

    private static void LoadEffectsFromAssembly()
    {
        var assembly = Assembly.GetExecutingAssembly();
        Console.WriteLine($"Loading effects from assembly: {assembly.FullName}");

        var effectTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseEffect)))
            .ToList();

        if (effectTypes.Count == 0)
        {
            Console.WriteLine("No effect types found in the assembly.");
        }

        foreach (var type in effectTypes)
        {
            Console.WriteLine($"Loading effect: {type.FullName}");
            try
            {
                var effect = (BaseEffect)Activator.CreateInstance(type);
                AddEffect(effect);
                Console.WriteLine($"Successfully loaded effect: {type.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load effect: {type.FullName}, Exception: {ex.Message}");
            }
        }

        Console.WriteLine("All effects loaded!");
    }
}
