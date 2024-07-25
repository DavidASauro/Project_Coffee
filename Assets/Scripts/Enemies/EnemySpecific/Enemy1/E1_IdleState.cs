using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{

    private Enemy1 _enemy1;
    public E1_IdleState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_IdleState stateData, Enemy1 enemy1) : base(entity, fsm, animBoolName, stateData)
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

        if (isPLayerInMinAgroRange)
        {
            fsm.ChangeState(_enemy1.playerDetectedState);
        }

        if (isIdleTimeOver)
        {
            fsm.ChangeState(_enemy1.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
