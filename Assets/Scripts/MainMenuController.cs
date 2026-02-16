using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1_ForestRuins");
    }

    public void OpenOptions()
    {
        UnityEngine.Debug.Log("Options menu not implemented yet.");
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("Quit pressed. This only closes the game in a build.");
        UnityEngine.Application.Quit();
    }
}
