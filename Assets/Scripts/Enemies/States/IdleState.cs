using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyStates
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPLayerInMinAgroRange;

    protected float idleTime;

    public IdleState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_IdleState stateData) : base(entity, fsm, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPLayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0);
        isIdleTimeOver = false;
        
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }

    public void SetFlipAfterIdle(bool shouldFlip)
    {
        flipAfterIdle = shouldFlip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
