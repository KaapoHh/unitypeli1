using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    private int direction = 1;
    public float distance = 1.5f;
    public int damageToGive = 1;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        Vector2 raycastOrigin = (Vector2)transform.position + new Vector2(direction * 0.6f, 0f);
        RaycastHit2D groundInfo = Physics2D.Raycast(raycastOrigin, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            direction *= -1;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Vihollinen osui: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Osuma pelaajaan havaittu!");
            PlayerHealthAndLife playerHealth = collision.gameObject.GetComponent<PlayerHealthAndLife>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToGive);
            }
            else
            {
                Debug.LogWarning("Pelaajalta puuttuu PlayerHealthAndLife-skripti!");
            }
        }
    }
}