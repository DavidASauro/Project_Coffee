using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState {  get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;

    public override void Start()
    {
        base.Start();
        moveState = new E1_MoveState(this, fsm, "move", moveStateData, this);
        idleState = new E1_IdleState(this, fsm, "idle", idleStateData,this);
        playerDetectedState = new E1_PlayerDetectedState(this, fsm, "detected", playerDetectedData, this);
        chargeState = new E1_ChargeState(this, fsm,"charge", chargeStateData, this);
        fsm.Initialize(moveState);
    }

}
