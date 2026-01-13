using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Starter.ThirdPersonCharacter
{
	/// <summary>
	/// Handles player connections (spawning of Player instances).
	/// </summary>
	public sealed class GameManager : NetworkBehaviour
	{
		public NetworkObject PlayerPrefab;
		public float SpawnRadius = 3f;
		
		
        public static Track CurrentTrack { get; private set; }

		public override void Spawned()
		{
			var randomPositionOffset = Random.insideUnitCircle * SpawnRadius;
			var spawnPosition = transform.position + new Vector3(randomPositionOffset.x, transform.position.y, randomPositionOffset.y);

			Runner.Spawn(PlayerPrefab, spawnPosition, Quaternion.identity, Runner.LocalPlayer);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, SpawnRadius);
		}
		
        public static void SetTrack(Track track)
        {
            CurrentTrack = track;
        }

        public void NextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No more levels to load.");
            }
        }
    }



}
