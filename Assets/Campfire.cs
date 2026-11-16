using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Campfire : MonoBehaviour
{
    public float maxFireTimer = 30f;
    public float currentFireTimer;

    [Header("UI Asetukset")]
    public Text campfireTimerText; 

    private bool isGameOver = false;

    void Start()
    {
        currentFireTimer = maxFireTimer;
    }

    void Update()
    {
        if (isGameOver) return;

        if (currentFireTimer > 0)
        {
            currentFireTimer -= Time.deltaTime;
        }
        else
        {
            GameOver();
        }

        UpdateCampfireUI();
    }

    void UpdateCampfireUI()
    {
        if (campfireTimerText != null)
        {
            campfireTimerText.text = "Nuotio: " + Mathf.Ceil(currentFireTimer) + "s";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("vihollinen p‰‰si nuotiolle");
            GameOver();
            return;
        }

        Inventory playerInventory = other.GetComponent<Inventory>();

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

                Debug.Log("k‰ytetty puita: " + woodToUse + " kpl Lis‰tty aikaa: " + (woodToUse * 10f));
            }
            else
            {
                Debug.Log("ei puita");
            }
        }
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("h‰visit pelin!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}