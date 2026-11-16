using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthAndLife playerHealth = collision.gameObject.GetComponent<PlayerHealthAndLife>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}