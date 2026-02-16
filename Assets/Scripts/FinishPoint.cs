
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private LevelCompleteUI completeUI;

    void Start()
    {
        completeUI = FindFirstObjectByType<LevelCompleteUI>();
        if (completeUI == null)
            Debug.LogError("FinishPoint: No LevelCompleteUI found in scene!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            completeUI.ShowLevelComplete();
        }
    }
}
