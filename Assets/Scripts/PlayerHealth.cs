using System.Collections;
using UnityEngine;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Lives")]
    public int maxLives = 3;
    public int currentLives = 3;
    public TextMeshProUGUI livesText;

    [Header("Invincibility")]
    public float invincibleTime = 0.5f;
    private bool isInvincible = false;

    [Header("Respawn Settings")]
    public Transform respawnPoint;

    [Header("Audio")]
    public AudioSource audioSource;    // plays hurt sound
    public AudioClip hurtSound;        // damage SFX

    [Header("References")]
    public HealthBarUI healthBar;
    public GameOverUI gameOverUI;

    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }

        UpdateLivesText();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        // Play hurt SFX
        if (audioSource != null && hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
        else
        {
            StartCoroutine(InvincibleCoroutine());
        }
    }

    void HandleDeath()
    {
        currentLives--;
        UpdateLivesText();

        if (currentLives <= 0)
        {
            TriggerFinalDeath();
            return;
        }

        StartCoroutine(RespawnPlayer());
    }

    void TriggerFinalDeath()
    {
        // Stop background music
        AudioSource bgm = GameObject.Find("BackgroundMusic")?.GetComponent<AudioSource>();
        if (bgm != null)
            bgm.Stop();

        // Disable player controls
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (gameOverUI != null)
            gameOverUI.ShowGameOver();
        else
            Debug.LogWarning("PlayerHealth has no GameOverUI assigned");
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(0.6f);

        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (respawnPoint != null)
            transform.position = respawnPoint.position;
        else
            Debug.LogWarning("PlayerHealth: No Respawn Point Assigned!");

        StartCoroutine(InvincibleCoroutine());
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    void UpdateLivesText()
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }

    public int CurrentHealth => currentHealth;
    public int CurrentLives => currentLives;
}
