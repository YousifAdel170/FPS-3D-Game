using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnowPos;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    
    
    public Path path;
    public GameObject debugsphere;

    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform grunbarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    // Just for Debugging
    [SerializeField]
    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        debugsphere.transform.position = lastKnowPos;
    }
    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // Is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                // range -85 degree & 85 degree
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    Debug.DrawLine(ray.origin, player.transform.position, Color.red); // Draw a line from the enemy to the player
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }


}
