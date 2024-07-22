using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    
    public EnemyStates currentState {  get; private set; }

    public void Initialize(EnemyStates startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EnemyStates newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }



}
