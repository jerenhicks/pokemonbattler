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

        var effectTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseEffect)))
            .ToList();

        if (effectTypes.Count == 0)
        {
            Console.WriteLine(typeof(GrowlEffect).ToString());
            Console.WriteLine("No effect types found in the assembly.");
        }

        foreach (var type in effectTypes)
        {
            try
            {
                var effect = (BaseEffect)Activator.CreateInstance(type);
                AddEffect(effect);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load effect: {type.FullName}, Exception: {ex.Message}");
            }
        }
    }
}
