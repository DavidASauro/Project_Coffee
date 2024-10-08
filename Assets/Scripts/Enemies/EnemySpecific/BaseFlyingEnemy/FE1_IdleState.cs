using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE1_IdleState : IdleState
{
    BaseFlyingEnemy BaseFlyingEnemy;
    public FE1_IdleState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_IdleState stateData, BaseFlyingEnemy baseFlyingEnemy) : base(entity, fsm, animBoolName, stateData)
    {
        BaseFlyingEnemy = baseFlyingEnemy;
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

        if (isIdleTimeOver)
        {
            fsm.ChangeState(BaseFlyingEnemy.flyingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
