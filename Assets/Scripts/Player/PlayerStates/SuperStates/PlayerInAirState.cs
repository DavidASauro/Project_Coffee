using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : State
{

    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isJumping;
    private bool JumpInputStop;
    private bool isThouchingWall;
    private bool dashInput;
    private bool isTouchignWallBack;
    private bool attackInput;

    private float startWallJumpCoyoteTime;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isThouchingWall;
        oldIsTouchingWallBack = isTouchignWallBack;

        isGrounded = player.CheckIfGrounded();
        isThouchingWall = player.CheckIfTouchingWall();
        isTouchignWallBack = player.CheckIfTouchingWallBehind();

        if (!wallJumpCoyoteTime && !isThouchingWall && !isTouchignWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        checkCoyoteTime();
        checkWallJumpCoyoteTime();

        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;
        JumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }else if (jumpInput && (isThouchingWall || isTouchignWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isThouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isThouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            
            stateMachine.ChangeState(player.JumpState);

        }else if (isThouchingWall && xInput == player.FacingDirection)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }else if (dashInput && player.DashState.CheckIfCanDash())
        {
            
            stateMachine.ChangeState(player.DashState);
        }
        else if (player.DashState.isDashing)
        {
            if (isGrounded)
            {
                player.DashState.isDashing = false;
            }
            
        }else if (attackInput)
        {
            if(player.currentWeapon is RangedWeapon)
            {
                stateMachine.ChangeState(player.RangedWeaponAttackState);
            }else
            {
                stateMachine.ChangeState(player.MeleeWeaponAttackState);
            }
        }
        else
        {
           
           player.CheckIfShouldFlip(xInput);
           player.SetVelocityX(playerData.movementSpeed * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (JumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void checkCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void checkWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true; 
    }
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StopWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = false;
    }
    public void SetIsJumping()
    {
        isJumping = true;
    }
}
