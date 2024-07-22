using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private int amountOfDashes;

    public float lastDashTime { get; private set; }
    public bool CanDash { get; private set; }

    public bool isDashing;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
        amountOfDashes = playerData.amountOfDashes;
    }

    public override void Enter()
    {
        base.Enter();
        isDashing = true;
        CanDash = false;
        player.InputHandler.UseDash();
        player.AddForceX(playerData.dashSpeed);
        isAbilityDone = true;
        lastDashTime = Time.time;
        amountOfDashes--;
        
        
    }
    public bool CheckIfCanDash()
    {
        if (CanDash && amountOfDashes > 0 && Time.time >= lastDashTime + playerData.dashCoolDown)
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
        CanDash = true;
    }
    public override void Exit()
    {
        base.Exit();
        
    }
}
