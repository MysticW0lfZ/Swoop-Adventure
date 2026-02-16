using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup panelGroup;
    public Button restartButton;
    public Button mainMenuButton;

    [Header("Settings")]
    public float fadeDuration = 0.6f;

    void Start()
    {
        // Ensure invisible at start
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;

        // Hook buttons
        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowGameOver()
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
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");   // <-- must match your scene name
    }
}
