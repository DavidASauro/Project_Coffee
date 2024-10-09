using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : EnemyStates
{
    protected D_FlyingStateData stateData;
    protected bool flyingEnemyWallCheck;
    protected bool playerDectected;
    protected Vector3 targetWaypoint;
    protected bool firstEnter = true;

    private int currentWaypointIndex = 0;

    public FlyingState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_FlyingStateData stateData) : base(entity, fsm, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        flyingEnemyWallCheck = entity.CheckWall();
        playerDectected = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        if (firstEnter)
        {
            targetWaypoint = stateData.pointB;
            firstEnter = false;
        }
        else
        {
            
            targetWaypoint = (targetWaypoint == stateData.pointA) ? stateData.pointB : stateData.pointA;
        }

       

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.SetVelocity(stateData.flyingSpeed);
        //entity.transform.position = Vector3.MoveTowards(entity.transform.position, targetWaypoint, stateData.flyingSpeed * Time.deltaTime);

    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
