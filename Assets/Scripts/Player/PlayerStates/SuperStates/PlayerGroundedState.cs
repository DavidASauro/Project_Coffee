using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : State
{
    protected Vector2 input;

    private bool JumpInput;
    private bool isGrounded;
    private bool DashInput;
    private bool InteractInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetDash();
        player.InputHandler.UseInteractInput();

    }

    public override void Exit()
    {
        
        base.Exit();
        

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandler.RawMovementInput;
        JumpInput = player.InputHandler.JumpInput;
        DashInput = player.InputHandler.DashInput;
        InteractInput = player.InputHandler.InteractInput;
        

        if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }else if(!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);

        }else if (DashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
            
        }else if (!player.CheckIfPlayerIsMoving() && player.InputHandler.InteractInput && player.AtEndOfLevel)
        {
            
            player.InputHandler.UseInteractInput();
            stateMachine.ChangeState(player.InteractNextLevelState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
