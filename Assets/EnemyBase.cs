using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Vihollisen arvot")]
    public float moveSpeed = 3f;
    public int maxHealth = 20;
    public int damageToCampfire = 10;
    public int coinReward = 10;

    protected int currentHealth;
    protected Transform targetCampfire;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        GameObject campfireObj = GameObject.FindGameObjectWithTag("Campfire");
        if (campfireObj != null)
        {
            targetCampfire = campfireObj.transform;
        }
    }

    protected virtual void Update()
    {
        MoveTowardsCampfire();
    }

    protected virtual void MoveTowardsCampfire()
    {
        if (targetCampfire != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetCampfire.position, moveSpeed * Time.deltaTime);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null)
        {
            inventory.AddWood(coinReward);
        }

        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Campfire"))
        {
            CampfireHealth campfireHealth = other.GetComponent<CampfireHealth>();
            if (campfireHealth == null)
            {
                campfireHealth = other.GetComponentInParent<CampfireHealth>();
            }

            if (campfireHealth != null)
            {
                campfireHealth.TakeDamage(damageToCampfire);
            }

            Destroy(gameObject);
        }
    }
}