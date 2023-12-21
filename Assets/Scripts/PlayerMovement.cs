using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Vector2 direction;
    Rigidbody2D rb;

    [Header("Physics")]
    public float speedMultiplier = 10f;
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float fallMultiplier = 5f;
    public float gravity = 1f;

    [Header("Animations")]
    public Animator animator;
    public bool facingRight = true;

    [Header("Jumping")]
    public float jumpSpeed = 2f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Collisions")]
    public bool onGround = false;
    public LayerMask mask;
    public float groundLength = 0.6f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, mask);

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        
        animations();

        direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

    void animations()
    {
        if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void jump()

    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed,ForceMode2D.Impulse);
        jumpTimer = 0;

    }



     void FixedUpdate()
    {
        Debug.Log(rb.velocity.x);
        Move(direction.x);
        if (jumpTimer > Time.time && onGround)
        {
            jump();
        }
        modifyPhysics();

        //rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y);
    }

     void Move(float movespeed)
    {
        
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        { 
            
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            
        } 
        rb.AddForce(Vector2.right * movespeed * speedMultiplier);
        //animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));

        if((movespeed > 0 && !facingRight) || (movespeed < 0 && facingRight))
        {
            flipCharacter();
        }
    }

   public void flipCharacter()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0f, facingRight ? 0 : 180, 0f);
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if (Math.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y<0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }else if (rb.velocity.y>0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

}
