using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float damageRate = 1f;
    private Transform target;

    void Start()
    {
        GameObject campfire = GameObject.Find("Campfire");
        if (campfire != null)
        {
            target = campfire.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Campfire"))
        {
            Debug.Log("vihollinen osui nuotioon!");
        }
    }
}