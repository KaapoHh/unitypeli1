using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator ani;
    private BoxCollider2D coll;
            
    [SerializeField ] private LayerMask jumpableGround;
   
    [Header("Tuplahyppy")]
    private bool canDoubleJump = false;
    private Coroutine doubleJumpTimerCoroutine;


    private float sivuttain = 0f;

    private enum movementstate {idle, walking, jumping, falling }

    
    private void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent <Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        sivuttain = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(sivuttain * 7f, rb.linearVelocity.y);


        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 7f);
            }
            else if (canDoubleJump)
            {
               
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 7f);
                canDoubleJump = false;
            }
        }
        UpdateAnimationUpdate();

    }
    private void UpdateAnimationUpdate()
    {
        movementstate state;
        if (sivuttain > 0f)
        {
            state = movementstate.walking;
            sprite.flipX = false;
        }
        else if (sivuttain < 0f)
        {
            state = movementstate.walking;
            sprite.flipX = true;
        }
        else
        {
            state = movementstate.idle;
        }
        if ( rb.linearVelocity.y > .1f)
        {
            state = movementstate.jumping;
        }
        else if ( rb.linearVelocity.y < -.1f)
        {
            state = movementstate.falling;
        }
        ani.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {   
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, jumpableGround);
    }
    public void ActivateDoubleJumpFor10Seconds()
    {
        canDoubleJump = true;

       
        if (doubleJumpTimerCoroutine != null)
        {
            StopCoroutine(doubleJumpTimerCoroutine);
        }
        doubleJumpTimerCoroutine = StartCoroutine(DoubleJumpCountdown());
    }
    private System.Collections.IEnumerator DoubleJumpCountdown()
    {
        yield return new WaitForSeconds(10f);

        canDoubleJump = false;
    }
}