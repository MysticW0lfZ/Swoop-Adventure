using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup panelGroup;
    public Button restartButton;
    public Button mainMenuButton;
    public Button highScoresButton; 

    [Header("Score Submission")]
    public TMP_InputField playerNameInput;

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

        if (highScoresButton != null)
            highScoresButton.onClick.AddListener(OpenHighScores); 
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

    public void SubmitScore()
    {
        string playerName = playerNameInput.text;

        if (string.IsNullOrEmpty(playerName))
            playerName = "Anonymous";

        int score = FindFirstObjectByType<ScoreManager>().GetScore();
        float time = Time.timeSinceLevelLoad;

        DatabaseManager.Instance.SaveHighScore(playerName, score, time);

        Debug.Log("Score Submitted!");
    }

    void RestartLevel()
    {
        SubmitScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReturnToMainMenu()
    {
        SubmitScore();
        SceneManager.LoadScene("MainMenu");
    }

    void OpenHighScores() 
    {
        SubmitScore();
        SceneManager.LoadScene("HighScores");
    }
}