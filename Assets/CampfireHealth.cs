using UnityEngine;
using UnityEngine.UI;

public class CampfireHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Text healthText;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public bool IsFullHealth()
    {
        return currentHealth >= maxHealth;
    }

    public void TakeDamage(int amount)
    {
        int dmg = Mathf.Max(0, amount);
        currentHealth = Mathf.Max(0, currentHealth - dmg);
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        int healAmount = Mathf.Max(0, amount);
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth} / {maxHealth}";
        }
    }
}