using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractNextLevelState : PlayerGroundedState
{
    public PlayerInteractNextLevelState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
        player.isChangingLevel = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
