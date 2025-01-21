using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public static class EffectRepository
{
    private static readonly Dictionary<string, BaseEffect> Effects = new Dictionary<string, BaseEffect>();

    static EffectRepository()
    {
        LoadEffectsFromFolder("effects");
    }

    public static void AddEffect(BaseEffect effect)
    {
        Effects.Add(effect.GetType().Name.ToLower(), effect);
    }

    public static BaseEffect GetEffect(string name)
    {
        return Effects[name.ToLower()];
    }

    public static void LoadEffectsFromFolder(string folderPath)
    {
        var effectTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseEffect)))
            .ToList();

        foreach (var type in effectTypes)
        {
            var effect = (BaseEffect)Activator.CreateInstance(type);
            if (!Effects.ContainsKey(effect.GetType().Name.ToLower()))
            {
                AddEffect(effect);
            }
        }
    }
}