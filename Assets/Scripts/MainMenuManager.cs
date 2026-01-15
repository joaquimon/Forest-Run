using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void StartGame()
    {

        SceneManager.LoadScene("Bosque");

    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
