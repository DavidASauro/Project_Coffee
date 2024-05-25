using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    internal PlayerInput inputScript;

    public Transform launchOffset;

    public Vector2 direction;
    Rigidbody2D rb;
    private Transform originalParent;

    public Transform SpawnPoint;

    [Header("Movement")]
    public bool movingLeft = false;
    public bool movingRight = false;
    public float moveDirection;
    public float speedMultiplier = 10f;
    public float maxSpeed = 7f;

    [Header("Animations")]
    public Animator animator;
    public bool facingRight = true;

    [Header("Jumping")]
    public float jumpSpeed = 2f;
    public bool jumped = false;

    [Header("Collisions")]
    public bool onGround;
    public LayerMask mask;
    public BoxCollider2D coll;

    [Header("Player Stats")]
    public float currentHealth = 10f;
    public float maxHealth = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        originalParent = transform.parent;
    }

    // Update is called once per frame
    void Update()

    {
        onGround = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, mask);
        
        //play the annimations
        modifyPhysics();
        animations();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
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

    public void jump()
    {

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

    }


    public void movement()
    {
        rb.velocity = new Vector2(moveDirection * speedMultiplier, rb.velocity.y);

        if ((moveDirection > 0 && !facingRight) || (moveDirection < 0 && facingRight))
        {
            flipCharacter();
        }

    }

    public void flipCharacter()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0f, facingRight ? 0 : 180, 0f);
    }

    private void modifyPhysics()
    {
        if (rb.velocity.y > 1 || rb.velocity.y < -1 || rb.velocity.y == 0)
        {
            rb.drag = 0f;

        }
        else
        {
            rb.drag = 5f;
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
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            currentHealth = currentHealth - 1;
        }
    }

    public void resetToSpawnPoint()
    {
        transform.position = SpawnPoint.position;
        transform.rotation = SpawnPoint.rotation;
    }

}