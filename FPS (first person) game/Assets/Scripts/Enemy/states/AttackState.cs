using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter()
    {

    }

    public override void Perform()
    {
        // check if the player can be seen
        if (enemy.CanSeePlayer())
        {
            // lock the lose player timer & increment the move and shot timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            // if shot timer  then fireRate 
            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            // move the enemy to a random position after a random time
            if (moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;  
            }
            enemy.LastKnowPos = enemy.Player.transform.position;

        }
        // Lost sight of a player
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer >8)
            {
                // change to the search state
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        // 1. store references to the gun barrel
        Transform gunbarrel = enemy.grunbarrel;

        // 2. Instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);

        // 3. Calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;   

        // 4. add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f), Vector3.up) * shootDirection * 40;
        
        Debug.Log("Shoot");
        shotTimer = 0; // reset the timer
    }

    public override void Exit() 
    { 

    } 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
