using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private int amountOfDashes;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
        amountOfDashes = playerData.amountOfDashes;
    }

    public override void Enter()
    {
        base.Enter();
        //player.SetVelocityX(playerData.dashSpeed);
        player.AddForceX(playerData.dashSpeed);
        isAbilityDone = true;
        amountOfDashes--;
        player.InputHandler.UseDash();
    }
    public bool canDash()
    {
        if (amountOfDashes > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetDash()
    {
        amountOfDashes = playerData.amountOfDashes;
    }
}
