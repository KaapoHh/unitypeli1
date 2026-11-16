using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthAndLife playerHealth = collision.GetComponent<PlayerHealthAndLife>();

            if (playerHealth != null)
            {
                if (playerHealth.health < 3)
                {
                    playerHealth.health += healAmount;

                  
                    playerHealth.TakeDamage(-healAmount);

                    Destroy(gameObject);
                }
            }
        }
    }
}