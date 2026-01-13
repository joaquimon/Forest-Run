using System;
using Fusion;
using Fusion.Addons.Physics;
using Starter.ThirdPersonCharacter;
using UnityEngine;
using KCC = Fusion.Addons.KCC.KCC;
using UnityEngine.SceneManagement;

public class PlayerLapController : NetworkBehaviour
{
    [Networked] private int CheckpointIndex { get; set; } = 0;
    [SerializeField] private Canvas canvas;
    [SerializeField] private KCC _kcc;
    [SerializeField] private GameObject prefabUI;


    private void Awake()
    {
        _kcc = GetComponent<KCC>();
        canvas = FindObjectOfType<Canvas>();
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

    public void ProcessFinishLine(FinishLine finishLine) 
    {
        GameObject ui = Instantiate(prefabUI, canvas.transform);
        ui.transform.SetAsLastSibling(); // opcional: arriba del todo
        

    }
    
}