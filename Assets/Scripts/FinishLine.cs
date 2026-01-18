using Starter.ThirdPersonCharacter;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if ( other.GetComponentInParent<PlayerLapController>()) {
            PlayerLapController playerLap = other.GetComponentInParent<PlayerLapController>();
            playerLap.ProcessFinishLine(this);
        }
    }
}
