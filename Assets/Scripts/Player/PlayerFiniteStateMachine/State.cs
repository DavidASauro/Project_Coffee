using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string animationBoolName;

    public State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log(animationBoolName);
        DoChecks();
        player.Anim.SetBool(animationBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
       
    }

    public virtual void Exit() 
    {
        player.Anim.SetBool(animationBoolName, false);
        isExitingState = true;
    }

    //Called every frame
    public virtual void LogicUpdate()
    {

    }

    //called every fixed update
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    //checks for ground, wall.. etc
    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;

}
