using System;
using Fusion;
using Fusion.Addons.Physics;
using Starter.ThirdPersonCharacter;
using UnityEngine;
using KCC = Fusion.Addons.KCC.KCC;

public class PlayerLapController : NetworkBehaviour
{
    [Networked] private int CheckpointIndex { get; set; } = 0;

    [SerializeField] private KCC _kcc;

    private void Awake()
    {
        _kcc = GetComponent<KCC>();
    }
    
    public Transform ResetToCheckpoint() {
        Debug.Log("Reset to checkpoint " + CheckpointIndex);
    
        Transform lastCheckpoint = GameManager.CurrentTrack.checkpoints[CheckpointIndex-1].transform;
        return lastCheckpoint;
        
    }
    
    public void ProcessCheckpoint(Checkpoint checkpoint) {
        if ( CheckpointIndex == checkpoint.index ) {
            CheckpointIndex++;
        }
    }

    public void ProcessFinishLine(FinishLine finishLine) {
        
    }
}