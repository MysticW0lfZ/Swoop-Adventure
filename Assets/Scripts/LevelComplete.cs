using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelCompleteUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup panelGroup;
    public Button restartButton;
    public Button mainMenuButton;

    [Header("Settings")]
    public float fadeDuration = 0.6f;

    void Start()
    {
        // Hide panel at start
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;

        // Assign button events automatically
        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowLevelComplete()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        panelGroup.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panelGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            yield return null;
        }

        panelGroup.interactable = true;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Must match your menu scene name
    }
}

