using UnityEngine;

public class Marcador : MonoBehaviour
{
    [SerializeField] private GameObject prefabUI;
    [SerializeField] private Canvas canvas;


    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();

    }
    void Start()
    {
        GameObject ui = Instantiate(prefabUI, canvas.transform);
        ui.transform.SetAsLastSibling(); // opcional: arriba del todo
    }
}
