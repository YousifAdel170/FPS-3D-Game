using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Perform()
    {

        /*
            - make the enemy search around the area the player was last spotted
            - Use the code from Attack state
            - check to see if time.delta > moveTimer
            - set the destination of NavMeshAgent using Random.insideUnitSphere()
         
         */



        // check if see the player again & if yes attack
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
        // if lost the position search
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            // move the enemy to a random position after a random time
            if (moveTimer > Random.Range(3, 5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
            // if time> 10 go to patrol state 
            if (searchTimer > 10)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit() 
    {
    
    } 
}
