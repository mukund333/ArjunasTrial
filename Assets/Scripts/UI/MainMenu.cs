using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void QuitGame()
    { 
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Scene2");
    }
}
