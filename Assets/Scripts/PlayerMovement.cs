using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Collections;
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


    [Header("Movement")]
    public bool movingLeft = false;
    public bool movingRight = false;
    public float moveDirection;
    public float speedMultiplier = 10f;
    public float maxSpeed = 7f;

    [Header("Animations")]
    public Animator animator;
    public bool facingRight = true;
    public SpriteRenderer sprite;
    public float flashInterval;
    private Coroutine flashCoroutine;

    [Header("Jumping")]
    public float jumpSpeed = 2f;
    public bool jumped = false;

    [Header("Collisions")]
    public bool onGround;
    public bool hitDeathBox;
    public LayerMask mask;
    public LayerMask deathMask;
    public BoxCollider2D coll;

    [Header("Player Stats")]
    public float currentHealth = 10f;
    public float maxHealth = 10f;
    public bool isDead = false;
    private bool isInvincible = false;
    public float invincibilityDurationSeconds = 2f;
    
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
        hitDeathBox = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, deathMask);
 

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
            takeDamage(1);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deathBox")
        {

            currentHealth = currentHealth - maxHealth;

        }
    }

    public void takeDamage(float dmg)
    {
        //if the player is invincible do not take dmg
        if (isInvincible)
        {
            return;
        }
        currentHealth = currentHealth - dmg;

        //If player takes dmg and still has health start the i-frame coroutine
        if (currentHealth > 0)
        {
            StartCoroutine(startInvicibility());
        } 

    }

    private IEnumerator startInvicibility()
    {
        isInvincible = true;

        if (sprite != null)
        {
            flashCoroutine = StartCoroutine(flashSprite());
        }

        sprite.color = Color.red;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        isInvincible = false;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            sprite.color = Color.white;
        }

        
    }

    private IEnumerator flashSprite()
    {
        Color originalColor = sprite.color;
        Color flashColor = Color.red;

        while (isInvincible)
        {
            sprite.color = flashColor;
            yield return new WaitForSeconds(flashInterval);
            sprite.color = originalColor;
            yield return new WaitForSeconds(flashInterval);
        }

        sprite.color = originalColor;
    }



}