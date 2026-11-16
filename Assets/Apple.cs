using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>(); 

            if (player != null)
            {             
                player.ActivateDoubleJumpFor10Seconds();
            }


          
        }
    }
}