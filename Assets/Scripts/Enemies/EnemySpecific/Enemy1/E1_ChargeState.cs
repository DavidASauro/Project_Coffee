using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 _enemy1;
    public E1_ChargeState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_ChargeState stateData, Enemy1 enemy1) : base(entity, fsm, animBoolName, stateData)
    {
        _enemy1 = enemy1;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if (hasReachedLastPlayerLocation)
        {
            if (isPlayerInMinAgroRange)
            {
                //Maybe add delay in future if needed
               
                fsm.ChangeState(_enemy1.playerDetectedState);
            }
            else
            {
                fsm.ChangeState(_enemy1.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

   
}
