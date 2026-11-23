using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CampfireHealth : MonoBehaviour
{
    [Header("Nuotion elämäpisteet")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Valinnainen UI-näyttö")]
    public TMP_Text healthText;

    [Header("Tapahtumat kun HP muuttuu tai loppuu")]
    public UnityEvent onHealthChanged;
    public UnityEvent onCampfireExtinguished;

    private bool isExtinguished = false;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public bool IsFullHealth()
    {
        return currentHealth >= maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isExtinguished || damage <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        Debug.Log("Nuotion HP: " + currentHealth + "/" + maxHealth);

        UpdateUI();
        onHealthChanged?.Invoke();

        if (currentHealth <= 0 && !isExtinguished)
        {
            isExtinguished = true;
            Debug.Log("Nuotio sammui!");
            onCampfireExtinguished?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (isExtinguished || amount <= 0) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        Debug.Log("Nuotiota parannettiin! HP nyt: " + currentHealth + "/" + maxHealth);
        UpdateUI();
        onHealthChanged?.Invoke();
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "Nuotio: " + currentHealth + " / " + maxHealth;
        }
    }
}