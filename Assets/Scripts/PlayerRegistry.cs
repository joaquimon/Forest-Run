using System;
using System.Collections.Generic;
using Fusion;

public static class PlayerRegistry
{
    private static readonly Dictionary<PlayerRef, string> Players = new();
    
    public static event Action OnRegistryChanged;

    public static void Register(PlayerRef player, string name)
    {
        Players[player] = name;
        OnRegistryChanged?.Invoke();
    }

    public static bool GetName(PlayerRef player, out string name)
    {
        return Players.TryGetValue(player, out name);
    }
    
    public static void Clear()
    {
        Players.Clear();
    }
}
