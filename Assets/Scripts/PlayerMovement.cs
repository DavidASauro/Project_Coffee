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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

     void FixedUpdate()
    {
        Move(direction.x);
        modifyPhysics();

        //rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y);
    }

     void Move(float movespeed)
    {
       
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        { 
            
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            Debug.Log(rb.velocity.x);
        } 
        rb.AddForce(Vector2.right * movespeed * speedMultiplier);
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
