using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;


    void Update()
    {
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
