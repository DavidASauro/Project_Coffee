using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFlyingEnemy : Entity {

    //IDOL,MOVE,SWOOP(CHARGE),DETECT PLAYER
    public FE1_ChargeState chargeState { get; private set; }
    public FE1_DetectPlayerState detectPlayerState { get; private set; }
    public FE1_FlyingState flyingState { get; private set; }
    public FE1_IdleState idleState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_FlyingStateData flyingStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;

    public override void Start()
    {
        //CHANGE ANIMATION NAMES
        base.Start();
        flyingState = new FE1_FlyingState(this, fsm, "move", flyingStateData, this);
        idleState = new FE1_IdleState(this, fsm, "idle", idleStateData, this);
        detectPlayerState = new FE1_DetectPlayerState(this, fsm, "detected", playerDetectedData, this);
        chargeState = new FE1_ChargeState(this, fsm, "charge", chargeStateData, this);
        fsm.Initialize(flyingState);

    }

}
