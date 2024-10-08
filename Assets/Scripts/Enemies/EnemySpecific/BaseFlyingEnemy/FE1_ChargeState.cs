using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE1_ChargeState : ChargeState
{
    BaseFlyingEnemy BaseFlyingEnemy;
    public FE1_ChargeState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_ChargeState stateData, BaseFlyingEnemy baseFlyingEnemy) : base(entity, fsm, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
