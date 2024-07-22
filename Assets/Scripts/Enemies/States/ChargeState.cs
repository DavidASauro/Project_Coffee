using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : EnemyStates
{

    protected D_ChargeState stateData;
    
    protected Vector3 playerChargeLocation;

    protected GameObject player;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool hasReachedLastPlayerLocation;

    float distanceToPlayer;
    public ChargeState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_ChargeState stateData) : base(entity, fsm, animBoolName)
    {
        this.stateData = stateData;     
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player");
        hasReachedLastPlayerLocation = false;
        playerChargeLocation = player.transform.position;
        

        entity.SetVelocity(stateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        distanceToPlayer = Vector3.Distance(entity.transform.position, playerChargeLocation);
        if (distanceToPlayer <= stateData.chargeToPlayerLocationBuffer)
        {
            
            hasReachedLastPlayerLocation = true;
        }
        else
        {
            
            hasReachedLastPlayerLocation = false;
        }
        
        /**
        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
        **/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
