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
    private int lastWallJumpDirection;
    

    private Rigidbody2D rb;

    [Header("Player States")]
    public bool isFacingRight = true;
    public bool isGrounded;
    public bool canNormalJump;
    public bool canWallJump;
    public bool canWallHop;
    public bool isTouchingWall;
    public bool isWallSliding;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    public bool hasWallJumped;
    private int facingDirection = 1;

    [Header("PlayerControlStats")]
    public float playerMovementSpeed = 10.0f;
    public float jumpForce = 16f;
    public float numberOfJumps = 1f;
    private float numberOfJumpsRemaining;
    public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeighMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    private float jumpTimer;
    public float jumpTimerSet = 0.15f;
    private float wallJumpTimer;
    public float wallJumpTimerSet = 0.5f;

    [Header("WallJumpComponents")]
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    public bool canMove;
    public bool canFlip;
    private float flipTimer;
    public float flipTimerSet = 0.1f;

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
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        checkInput();
        checkToFlipSprite();
        checkIfPlayerCanJump();
        checkIfWallSliding();
        checkPlayerJumpType();
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
            if(isGrounded || (numberOfJumpsRemaining > 0 && isTouchingWall))
            {
                normalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                flipTimer = flipTimerSet;
            }
        }

        if (!canMove)
        {
            flipTimer -= Time.deltaTime;
            if(flipTimer <= 0)
            {
                canMove=true;
                canFlip=true;
            }
        }
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
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

        if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(movementInputDirection * playerMovementSpeed, rb.velocity.y);
        }
        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlidingSpeed || rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(0, -wallSlidingSpeed);
            }
        }
    }

    private void checkIfPlayerCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            numberOfJumpsRemaining = numberOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
            canWallHop = true;
        }

        if (numberOfJumpsRemaining <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
       
    }

    private void checkPlayerJumpType() 
    {
        if (jumpTimer > 0)
        {          
            if (!isGrounded && isTouchingWall & movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                wallJump();

            }else if (!isGrounded && isTouchingWall & movementInputDirection == 0)
            {
                wallHopOff();

            }else if (isGrounded)
            {
                normalJump();
            }     
        }
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if(wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
       
    }

    private void normalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            numberOfJumpsRemaining--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }

    }
    private void wallJump()
    {
        //if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump)
        if(canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isWallSliding = false;
            numberOfJumpsRemaining = numberOfJumps;
            numberOfJumpsRemaining--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            flipTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;

        }
    }

    private void wallHopOff()
    {    
        //if (isWallSliding && movementInputDirection == 0 && canJump) 
        if(canWallHop)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isWallSliding = false;
            numberOfJumpsRemaining--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            flipTimer = 0;
            canMove = true;
            canFlip = true;
        }
    }

    private void checkIfWallSliding()
    {
        if(isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y <= 0)
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
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
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