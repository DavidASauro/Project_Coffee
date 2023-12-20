using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speedMultiplier = 10f;
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    Vector2 direction;
    Rigidbody2D rb;

    public Animator animator;
    public bool facingRight = true;

    public float jump = 2f;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jump);
        }
        direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }



     void FixedUpdate()
    {
        Debug.Log(rb.velocity.x);
        Move(direction.x);
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
        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));

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
        if(Math.Abs(direction.x) < 0.4f)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

}
