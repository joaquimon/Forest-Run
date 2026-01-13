using Fusion.Addons.KCC;
using Starter.ThirdPersonCharacter;
using UnityEngine;

public class RespawnTrigger : KCCProcessor
{
    public override void OnEnter(KCC kcc, KCCData kccData)
    {
        // Teleport only in fixed update to not introduce glitches caused by incorrect render prediction.
        if (kcc.IsInFixedUpdate == false)
            return;
        
        Transform target = kcc.GetComponent<PlayerLapController>().ResetToCheckpoint();
        kcc.SetPosition(target.position);
        /*Debug.Log("Trigger enter");
        if (other.GetComponentInParent<Player>())
        {
            Player player = other.GetComponentInParent<Player>();
            Debug.Log("Respawn triggered");
            if ( player.Object.HasStateAuthority ) player.PlayerLapController.ResetToCheckpoint();
        }*/
    }
}
