using System;
using System.Collections.Generic;
using System.Linq;

public static class IDGenerator
{
    private static readonly List<string> GeneratedIDs = new List<string>();
    private static readonly Random Random = new Random();

    public static string GenerateID()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string newID;

        do
        {
            newID = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        } while (GeneratedIDs.Contains(newID));

        GeneratedIDs.Add(newID);
        return newID;
    }
}