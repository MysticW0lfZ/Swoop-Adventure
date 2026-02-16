using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public AudioClip collectSound;      // <-- new

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play coin sound
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // Add score
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddScore(value);

            Destroy(gameObject);
        }
    }
}
