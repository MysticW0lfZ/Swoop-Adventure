using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoresDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        List<HighScore> scores = DatabaseManager.Instance.GetTopHighScores(5);

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < scores.Count)
            {
                var s = scores[i];
                scoreTexts[i].text = (i + 1) + ". " + s.PlayerName +
                    " - " + s.Score + " pts - " +
                    s.CompletionTime.ToString("F1") + "s";
            }
            else
            {
                scoreTexts[i].text = (i + 1) + ". ---";
            }
        }
    }
}