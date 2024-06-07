using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    public float aggroDistance = 5f;
    public bool isAggroed = false;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;
    public bool facingRight = true;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;
    public bool hasLedgeDetection = true;
    public bool flyingEnemy = false;
    private bool patrollingEnemy = false;
    public bool isPatrollingEnemy = false;


    [Header("Ledge Detection")]
    public Transform ledgeDetector;
    public LayerMask groundLayer;
    public float raycastDistance;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    private BoxCollider2D coll;
    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField] private LayerMask jumpableGround;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        GameObject player = GameObject.Find("Player");
        target = player.transform;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        
        //adjusting parameters for flying enemies
        if (flyingEnemy)
        {
            jumpEnabled = false;
            rb.gravityScale = 0f;
            rb.drag = 2f;
        }
    }
    public void Update()
    {
        if (patrollingEnemy)
        {
            RaycastHit2D ledgehit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);

            if (ledgehit.collider == null)
            {
                rotateEnemy();
            }
        }
        
    }
    

    public void FixedUpdate()
    {
      
        //making the enemy aggro to player if player enters the aggro range of the enemy
        if (Vector2.Distance(transform.position, target.transform.position) < aggroDistance)
        {
            isAggroed = true;
            patrollingEnemy = false;
            jumpEnabled = true;
        }
        //adjusting aggro is player escapes enemy follow range
        if (!TargetInDistance() && isPatrollingEnemy)
        {
            isAggroed=false;
            patrollingEnemy=true;
            jumpEnabled = false;
         
        }

        if (isAggroed)
        {
            if (TargetInDistance() && followEnabled)
            {
                        PathFollow();
            
            }

        }

        if (patrollingEnemy)
        {
            
            if (facingRight)
            {
                rb.velocity = new Vector2(1, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-1, rb.velocity.y);
            }
            
        }
       
    }

    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        //No path
        if (path == null)
        {
            return;
        }

        //Reached end of the path
        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //See if colliding with anything
        RaycastHit2D isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, jumpableGround);
        RaycastHit2D ledgehit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);


        //Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //cheking if player is above the enemy
        bool isPlayerAbove = target.position.x >= rb.position.x - 0.5f &&
                             target.position.x <= rb.position.x + 0.5f &&
                             target.position.y > rb.position.y + 1.0f;

        //Jump Logic
        if ((jumpEnabled && isGrounded && ledgehit.collider == null) || (jumpEnabled && isGrounded && isPlayerAbove) )
        {
            if(direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        //ledge Detection
        if (ledgehit.collider == null && isGrounded && direction.y < jumpNodeHeightRequirement && hasLedgeDetection)
        {
            force = direction * 0;
            rb.velocity = Vector2.zero;
            
        }

        //Enemy movement
        rb.AddForce(force);

        //Find Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        //Directional GFX handling
        if (directionLookEnabled)
        {
            if(rb.velocity.x > 0.05f && !facingRight)
            {
                rotateEnemy();
                
            }else if (rb.velocity.x < -0.05f && facingRight)
            {
                rotateEnemy();
            }
        }

    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void rotateEnemy()
    {
        transform.Rotate(0,180,0);
        facingRight = !facingRight;
    }
}
