using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerHealthAndLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;

    [Header("Health Settings")]
    public int health = 3;
    public Text healthText; 


    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        UpdateHealthUI();
    }

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            TakeDamage(3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(1); 
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
        else
        {
          
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }

    private void Die()
    {
        if (rb != null) rb.bodyType = RigidbodyType2D.Static;
        if (ani != null) ani.SetTrigger("death");

        Invoke("RestartLevel", 0.5f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}