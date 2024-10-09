using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedWeaponAttackState : PlayerAttackState
{
    public PlayerRangedWeaponAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.currentWeapon.shoot();
    }

}
