using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
    public float maxFireTimer = 30f;
    public float currentFireTimer;

    [Header("Viittaukset")]
    public CampfireHealth campfireHealth;

    [Header("UI Asetukset")]
    public Text campfireTimerText;

    private bool isGameOver = false;

    void Awake()
    {
        if (campfireHealth == null)
        {
            campfireHealth = GetComponent<CampfireHealth>();
        }
    }

    void Start()
    {
        currentFireTimer = maxFireTimer;
        UpdateCampfireUI();
    }

    void Update()
    {
        if (isGameOver) return;

        if (campfireHealth != null && campfireHealth.currentHealth <= 0)
        {
            GameOver();
            return;
        }

        if (currentFireTimer > 0f)
        {
            currentFireTimer -= Time.deltaTime;
        }
        else
        {
            currentFireTimer = 0f;
            UpdateCampfireUI();
            GameOver();
            return;
        }

        UpdateCampfireUI();
    }

    void UpdateCampfireUI()
    {
        if (campfireTimerText != null)
        {
            campfireTimerText.text = "Nuotio: " + Mathf.CeilToInt(currentFireTimer) + "s";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver) return;

        Inventory playerInventory = other.GetComponent<Inventory>();
        if (playerInventory == null)
        {
            playerInventory = other.GetComponentInParent<Inventory>();
        }

        if (playerInventory != null)
        {
            if (playerInventory.woodCount > 0)
            {
                int woodToUse = playerInventory.woodCount;

                currentFireTimer += woodToUse * 10f;
                if (currentFireTimer > maxFireTimer)
                {
                    currentFireTimer = maxFireTimer;
                }

                playerInventory.woodCount = 0;
                Debug.Log("Käytetty puita: " + woodToUse + " kpl. Lisätty aikaa: " + (woodToUse * 10f));
            }
            else
            {
                Debug.Log("Ei puita");
            }
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("Hävisit pelin!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}