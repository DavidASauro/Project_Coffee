using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine fsm;

    public D_Entity entityData;
    public Rigidbody2D rb {  get; private set; }
    //public Animator animator { get; private set; }

    public int facingDirection { get; private set; }
    private Vector2 VelocityWorkspace;

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    public virtual void Start()
    {

        facingDirection = 1;
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        fsm = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        fsm.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        VelocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = VelocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual void Flip() 
    {
        
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    }

    
}
