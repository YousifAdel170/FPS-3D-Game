using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    // propery for the patrol state
    // public PatrolState patrolState;
    public void Initialise()
    {
        // setup default state
        //  patrolState = new  PatrolState();
        // ChangeState(patrolState);
        ChangeState(new PatrolState());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        // check activeState not equal NULL
        if(activeState != null)
        {
            // run cleanup on activeState
            activeState.Exit();
        }
        // change to new state
        activeState = newState;

        // fail-safe null check to make sure new state wasn't NULL
        if(activeState != null)
        {
            // setup new state
            activeState.stateMachine = this;
            // assign state enemy class
            activeState.enemy = GetComponent<Enemy>();  
            activeState.Enter();
        }
    }
}