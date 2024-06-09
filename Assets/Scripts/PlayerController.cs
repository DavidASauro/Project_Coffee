using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    

    private Rigidbody2D rb;

    [Header("Player States")]
    public bool isFacingRight = true;
    public bool isGrounded;
    public bool canJump;
    public bool isTouchingWall;
    public bool isWallSliding;


    [Header("PlayerControlStats")]
    public float playerMovementSpeed = 10.0f;
    public float jumpForce = 16f;
    public float numberOfJumps = 1f;
    private float numberOfJumpsRemaining;
    public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeighMultiplier = 0.5f;

    //[Header("Components")]

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundCheckLayerMask;

    [Header("WallCheck")]
    public Transform wallCheck;
    public float wallCheckCastDistance = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        numberOfJumpsRemaining = numberOfJumps;
    }

    private void Update()
    {
        checkInput();
        checkToFlipSprite();
        checkIfPlayerCanJump();
        checkIfWallSliding();
    }

    private void FixedUpdate()
    {
        applyMovement();
        checkSurroundings();
    }

    private void checkInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            playerJump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeighMultiplier);
        }

    }

    private void checkSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundCheckLayerMask);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckCastDistance, groundCheckLayerMask);
    }

    private void applyMovement()
    {
        if (isGrounded)
        {  
            rb.velocity = new Vector2(movementInputDirection * playerMovementSpeed, rb.velocity.y);

        }else if (!isGrounded && !isWallSliding && movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > playerMovementSpeed)
            {
                rb.velocity = new Vector2(playerMovementSpeed* movementInputDirection, rb.velocity.y);
            }
        }else if(!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        
        if (isWallSliding)
        {
            //adjust this if statement if there is wall jumping or not
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void checkIfPlayerCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            numberOfJumpsRemaining = numberOfJumps;
        }

        if (numberOfJumpsRemaining <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
       
    }

    private void playerJump() 
    {
        if (canJump) { 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            numberOfJumpsRemaining--;
        }
        
    }

    private void checkIfWallSliding()
    {
        if(isTouchingWall && !isGrounded && rb.velocity.y <= 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void checkToFlipSprite()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            flipSprite();

        }else if(!isFacingRight && movementInputDirection > 0)
        {
            flipSprite();
        }
    }

    private void flipSprite()
    {
        if (!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0,180,0);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckCastDistance, wallCheck.position.y, wallCheck.position.z));
    }

}