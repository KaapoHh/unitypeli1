using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Vihollisen arvot")]
    public float moveSpeed = 3f;
    public int damageToCampfire = 1;
    public int coinReward = 10;

    protected Transform targetCampfire;
        
    protected virtual void Start()
    {
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Campfire"))
        {
            Destroy(gameObject);
        }
    }
}