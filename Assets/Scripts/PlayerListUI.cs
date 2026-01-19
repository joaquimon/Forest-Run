using System;
using Fusion;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Fusion.Sockets;

public class PlayerListUI : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("UI")]
    public GameObject Panel;
    public TMP_Text PlayerEntryPrefab;
    public Transform ContentRoot;

    private NetworkRunner _runner;
    private readonly List<TMP_Text> _entries = new();

    private void Awake()
    {
        PlayerRegistry.OnRegistryChanged += Refresh;
    }
    
    private void OnDestroy()
    {
        PlayerRegistry.OnRegistryChanged -= Refresh;
    }

    private void Start()
    {
        Panel.SetActive(false);
    }

    private void Update()
    {
        Panel.SetActive(Input.GetKey(KeyCode.Tab));
    }
    
    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Refresh();
    }

    private void Refresh()
    {
        Clear();

        if (_runner == null)
            return;

        foreach (var playerRef in _runner.ActivePlayers)
        {
            if(!PlayerRegistry.GetName(playerRef, out var playerName))
                continue;

            var entry = Instantiate(PlayerEntryPrefab, ContentRoot);
            entry.text = playerName;
            _entries.Add(entry);
        }
    }

    private void Clear()
    {
        foreach (var e in _entries)
            Destroy(e.gameObject);

        _entries.Clear();
    }
    
    public void SetRunner(NetworkRunner runner)
    {
        _runner = runner;
        _runner.AddCallbacks(this);
    }

    #region NetworkRunnerCallbacks

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    #endregion
    
}
