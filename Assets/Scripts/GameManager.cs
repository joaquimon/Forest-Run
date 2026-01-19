using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace Starter.ThirdPersonCharacter
{
	/// <summary>
	/// Handles player connections (spawning of Player instances).
	/// </summary>
	public sealed class GameManager : MonoBehaviour, INetworkRunnerCallbacks
	{
        public static GameManager Instance { get; private set; }
	
		public NetworkObject PlayerPrefab;
		public float SpawnRadius = 3f;
		
		private NetworkRunner _runner;
		
        public static Track CurrentTrack { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void RegisterRunner(NetworkRunner runner)
        {
            _runner = runner;
            runner.AddCallbacks(this);
            var playerListUI = GetComponent<PlayerListUI>();
            if (playerListUI != null)
            {
                playerListUI.SetRunner(_runner);
            }
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            
        }
        
        public void OnSceneLoadDone(NetworkRunner runner)
        {
                RespawnPlayer(runner);
        }
        
        private Vector3 GetSpawnPosition()
        {
            
        
            // Si tenemos pista, aparecer cerca de punto de spawn
            if (CurrentTrack != null)
            {
                var offset = Random.insideUnitCircle * SpawnRadius;
                return CurrentTrack.spawnpoint.position + new Vector3(offset.x, 0f, offset.y);
            }

            // En caso de no tener: aparecer cerca de GameManager
            var randomOffset = Random.insideUnitCircle * SpawnRadius;
            return transform.position + new Vector3(randomOffset.x, transform.position.y, randomOffset.y);
        }
        
        private void RespawnPlayer(NetworkRunner runner)
        {
            PlayerRef localPlayer = runner.LocalPlayer;
        
            // Si el jugador YA tiene un PlayerObject, NO hacemos nada
            if (runner.TryGetPlayerObject(localPlayer, out _))
            {
                return;
            }
            
            Vector3 spawnPosition = GetSpawnPosition();

           runner.Spawn(
                PlayerPrefab,
                spawnPosition,
                Quaternion.identity,
                localPlayer
            );
        }
        
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            PlayerRegistry.Clear();
        }

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, SpawnRadius);
		}
		
        public static void SetTrack(Track track)
        {
            CurrentTrack = track;
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");

        }

        #region Unused Callbacks

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            ;
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



}
