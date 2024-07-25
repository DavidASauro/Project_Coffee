using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyStates
{
    protected FiniteStateMachine fsm;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName;

    public EnemyStates(Entity entity, FiniteStateMachine fsm, string animBoolName)
    {
        this.entity = entity;
        this.fsm = fsm;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        //entity.animator.SetBool(animBoolName, true);
        DoChecks();
    }
    
    public virtual void Exit()
    {
        //entity.animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
