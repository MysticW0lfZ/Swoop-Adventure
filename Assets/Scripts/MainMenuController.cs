
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
        Debug.Log("Options menu not implemented yet.");
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed. This only closes the game in a build.");
        Application.Quit();
    }
}
