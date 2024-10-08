using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE1_FlyingState : FlyingState
{
    BaseFlyingEnemy _BaseFlyingEnemy;
    public FE1_FlyingState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_FlyingStateData stateData, BaseFlyingEnemy baseFlyingEnemy) : base(entity, fsm, animBoolName, stateData)
    {
        _BaseFlyingEnemy = baseFlyingEnemy;
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

        if (Mathf.Abs(entity.transform.localPosition.x - targetWaypoint.x) < stateData.tolerance)
        {
            _BaseFlyingEnemy.idleState.SetFlipAfterIdle(true);
            fsm.ChangeState(_BaseFlyingEnemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
