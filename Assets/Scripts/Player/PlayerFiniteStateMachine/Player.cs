using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    //Player States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAbilityState AbilityState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set;}
    public PlayerDashState DashState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerInteractNextLevelState InteractNextLevelState { get; private set; }
    public PlayerRangedWeaponAttackState RangedWeaponAttackState { get; set; }
    public PlayerMeleeWeaponAttackState MeleeWeaponAttackState { get; set;}

    [SerializeField]
    private PlayerData playerData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }

    [SerializeField]
    public Weapon currentWeapon;
    [SerializeField]
    private Entity currentEnemy;

    private Transform originalParent;

    [SerializeField]
    private Health playerHealth;

    
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;
    public bool AtEndOfLevel { get; private set; }
    public bool isChangingLevel;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallslide");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        InteractNextLevelState = new PlayerInteractNextLevelState(this, StateMachine, playerData, "nextLevel");
        RangedWeaponAttackState = new PlayerRangedWeaponAttackState(this, StateMachine, playerData, "wallslide");
        MeleeWeaponAttackState = new PlayerMeleeWeaponAttackState(this, StateMachine, playerData, "wallslide");

    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        
        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
        originalParent = transform.parent;

    }

    private void Update()
    {   
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions

    public void SetVelocity(float veloctiy, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * veloctiy * direction, angle.y * veloctiy);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float vel)
    {  
        workspace.Set(vel, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
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


    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
       
        return Physics2D.BoxCast(groundCheck.position, new Vector2(playerData.groundCheckX, playerData.groundCheckY), 0f, Vector2.down, playerData.groundCastDistance, playerData.groundMask);
        
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.groundMask);
    }

    public bool CheckIfTouchingWallBehind()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.groundMask);
    }
    public void CheckIfShouldFlip(float Xinput)
    {
        if(Xinput != 0 && Xinput != FacingDirection)
        {
            Flip();
            //Debug.Log("Flip");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("NextLevel"))
        { 
            AtEndOfLevel = true;
        }
    }
 
    public bool CheckIfPlayerIsMoving()
    {
        if ((CurrentVelocity.x >= -2 || CurrentVelocity.x >= 2) && (CurrentVelocity.y == 0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="enemy")
        {
            currentEnemy = collision.gameObject.GetComponent<Entity>();
            playerHealth.TakeDamage(currentEnemy.damage);
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + playerData.wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawCube(groundCheck.position, new Vector3(playerData.groundCheckX, playerData.groundCheckY + playerData.groundCastDistance*2, 0));
    }

    #endregion

    #region Other Functions

    public void AddForceX(float xForce)
    {
        RB.AddForce(new Vector2(xForce * FacingDirection,CurrentVelocity.y), ForceMode2D.Force);
        
    }
    private void AnimationTrigger()=> StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f,180.0f,0.0f);
    }
    #endregion
}
