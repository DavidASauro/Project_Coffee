using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity enemy, FiniteStateMachine fsm, string animBoolName, D_MoveState stateData, Enemy1 enemy1) : base(enemy, fsm, animBoolName, stateData)
    {
        this.enemy = enemy1;
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


        if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState(enemy.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            fsm.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
