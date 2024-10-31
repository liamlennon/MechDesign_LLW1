using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Testing Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
