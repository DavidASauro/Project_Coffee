using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(input.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);

        }else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

 
}
