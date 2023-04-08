using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public Animator enemyAnimator;
    public float waitTime;

    public void Initialise()
    {
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState != null
        if (activeState != null)
        {
            //run cleanup on activeState.
            activeState.Exit();
        }

        //change to a new state.
        activeState = newState;
        
        
        //fai;-safe null check to make sure new state wasn't null
        if (activeState != null)
        {
            //Setup new state
            
            activeState._stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
