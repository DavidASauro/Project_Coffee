using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 _enemy1;
    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy1) : base(entity, fsm, animBoolName, stateData)
    {
        _enemy1 = enemy1;
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

        if (!isPlayerInMaxAgroRange)
        {
            _enemy1.idleState.SetFlipAfterIdle(false);
            fsm.ChangeState(_enemy1.idleState);
        }else if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState(_enemy1.chargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
