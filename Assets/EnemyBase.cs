using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Vihollisen arvot")]
    public float moveSpeed = 3f;
    public int maxHealth = 20;
    public int damageToCampfire = 10;
    public float attackInterval = 1.5f;
    public float stopDistanceFromCampfire = 0.8f;
    public int coinReward = 10;

    [Header("Aseen pudotus (esim. 0.1 = 10%)")]
    [Range(0f, 1f)]
    public float weaponDropChance = 0.10f;

    protected int currentHealth;
    protected Transform targetCampfire;
    private CampfireHealth targetCampfireHealth;
    private float nextAttackTime = 0f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        GameObject campfireObj = GameObject.FindGameObjectWithTag("Campfire");
        if (campfireObj != null)
        {
            targetCampfire = campfireObj.transform;
            targetCampfireHealth = campfireObj.GetComponent<CampfireHealth>();
            if (targetCampfireHealth == null)
            {
                targetCampfireHealth = campfireObj.GetComponentInParent<CampfireHealth>();
            }
        }
    }

    protected virtual void Update()
    {
        if (targetCampfire == null) return;

        float distance = Vector2.Distance(transform.position, targetCampfire.position);

        if (distance > stopDistanceFromCampfire)
        {
            MoveTowardsCampfire();
        }
        else
        {
            TryAttackCampfire();
        }
    }

    protected virtual void MoveTowardsCampfire()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetCampfire.position,
            moveSpeed * Time.deltaTime
        );
    }

    protected virtual void TryAttackCampfire()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackInterval;

        if (targetCampfireHealth != null)
        {
            targetCampfireHealth.TakeDamage(damageToCampfire);
        }
    }
    //
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
        if (Random.value <= weaponDropChance)
        {
            WeaponSystem weaponSystem = FindFirstObjectByType<WeaponSystem>();
            if (weaponSystem != null)
            {
                weaponSystem.UnlockPistol(true);
            }
            else
            {
                Debug.LogWarning("Ase tippui, mutta scenestä ei löytynyt WeaponSystem-komponenttia!");
            }
        }
        else
        {
            Debug.Log("Vihollinen kaatui (ei asepudotusta).");
        }

        Destroy(gameObject);
    }
}