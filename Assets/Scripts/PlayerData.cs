using Fusion;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [Networked]
    public string PlayerName { get; set; }

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
        }
    }
}
