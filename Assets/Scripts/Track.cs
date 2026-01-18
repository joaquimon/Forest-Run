using Fusion;
using Starter.ThirdPersonCharacter;
using UnityEngine;

public class Track : NetworkBehaviour
{
    public static Track Current { get; private set; }

    public Checkpoint[] checkpoints;
    public Transform spawnpoint;
    public Transform finishLine;
    
    private void Awake()
    {
        Current = this;
        InitCheckpoints();

        GameManager.SetTrack(this);
    }
    
    private void OnDestroy()
    {
        GameManager.SetTrack(null);
    }
    
    private void InitCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].index = i;
        }
    }
}
