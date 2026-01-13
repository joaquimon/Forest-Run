using Starter.ThirdPersonCharacter;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index = 0;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Player>()) {
            Player player = other.GetComponentInParent<Player>();
            player.PlayerLapController.ProcessCheckpoint(this);
        }
    }
}
