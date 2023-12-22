using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Vector2 direction;
    Rigidbody2D rb;
    private Transform originalParent;

    [Header("Physics")]
    public float speedMultiplier = 10f;
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float fallMultiplier = 5f;
    public float gravity = 1f;
    public float maxGravity = 50f;

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

        originalParent = transform.parent;
    }

    // Update is called once per frame
    void Update()

    {  
        direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, mask);

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        
        animations();

        

        //Debug.Log(rb.velocity.y);
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
        //Debug.Log(rb.velocity.x);
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

        if ((movespeed > 0 && !facingRight) || (movespeed < 0 && facingRight))
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
            if (rb.velocity.y < 0)
            {
                
                rb.gravityScale += fallMultiplier;
                rb.gravityScale = Mathf.Clamp(rb.gravityScale, gravity, maxGravity);

            }else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale += fallMultiplier;
                rb.gravityScale = Mathf.Clamp(rb.gravityScale, gravity, maxGravity);
            }
            else
            {
                rb.gravityScale = gravity;
                rb.drag = linearDrag * 0.15f;
            }

        
        }
    }


    public void SetParent(Transform newParent)
    {
        originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = originalParent;
    }




}
