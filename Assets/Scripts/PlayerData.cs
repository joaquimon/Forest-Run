using Fusion;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [Networked]
    public string PlayerName { get; private set; }

    public override void Spawned()
    { 
        if(Object.HasInputAuthority)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", $"Player {Object.InputAuthority.PlayerId}");
            RPC_RegisterPlayer(Object.InputAuthority, playerName);
        }
    }
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_RegisterPlayer(PlayerRef player, string playerName)
    {
        PlayerName = playerName;
        PlayerRegistry.Register(player, playerName);
    }
}
