using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : State
{

    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool coyoteTime;
    private bool isJumping;
    private bool JumpInputStop;
    private bool isThouchingWall;
    private bool dashInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isThouchingWall = player.CheckIfTouchingWall();

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

        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;
        JumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }else if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
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
            
        }else
        {
           Debug.Log("Here");
           player.CheckIfShouldFlip(xInput);
           player.SetVelocityX(playerData.movementSpeed * xInput);
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

    public void StartCoyoteTime()
    {
        coyoteTime = true; 
    }

    public void SetIsJumping()
    {
        isJumping = true;
    }
}
