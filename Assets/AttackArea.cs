using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("osui kohteeseen: " + collision.gameObject.name + " tag: " + collision.tag + ")");

        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("vihollinen tuhottu");
        }
    }
}