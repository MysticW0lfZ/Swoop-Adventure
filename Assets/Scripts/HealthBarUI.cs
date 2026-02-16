using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthFillImage;

    private int maxHealth;

    void Start()
    {
        if (playerHealth != null)
            maxHealth = playerHealth.maxHealth;
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        SetHealth(value);
    }

    public void SetHealth(int value)
    {
        if (healthFillImage == null || maxHealth <= 0) return;

        float percent = (float)value / maxHealth;
        percent = Mathf.Clamp01(percent);
        healthFillImage.fillAmount = percent;
    }

    void Update()
    {
        if (playerHealth != null)
        {
            SetHealth(playerHealth.CurrentHealth);
        }
    }
}
