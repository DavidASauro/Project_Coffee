using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : State
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool jumpInput;
    protected int xInput;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        

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
        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;


        if (jumpInput)
        {
            
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);

        }
        else if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }else if (!isTouchingWall || xInput != player.FacingDirection)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
