/* -----------------------------------------------------------------------------------
 * Class Name: EnemyAI
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    EnemySight enemySight;
    NavMeshAgent nav;
    Transform player;
    PlayerHealth playerHealth;
    LastPlayerSighted lastPlayerSighted;
    float chaseTimer;
    float patrolTimer;
    int wayPointIndex;
	
	// ------------------------------------------------------------------------------
    // GETTERS/SETTERS
    // ------------------------------------------------------------------------------



	// ------------------------------------------------------------------------------
	// FUNCTIONS
	// ------------------------------------------------------------------------------

	// ------------------------------------------------------------------------------
	// Function Name: Start
	// Return types: N/A
	// Argument types: N/A
	// Author: 
	// Date: 
	// ------------------------------------------------------------------------------
	// Purpose: Used to initialize variables or perform startup processes
	// ------------------------------------------------------------------------------
	void Start () 
	{
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighted = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighted>();

	} //End Start
	
	// ------------------------------------------------------------------------------
	// Function Name: Update
	// Return types: N/A
	// Argument types: N/A
	// Author: 
	// Date: 
	// ------------------------------------------------------------------------------
	// Purpose: Runs each frame. Used to perform frame based checks and actions.
	// ------------------------------------------------------------------------------
	
	void Update () 
	{
        if (enemySight.personalLastSighting != lastPlayerSighted.resetPosition && playerHealth.health > 0f)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }

	} //End Update

    void Chasing ()
    {
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

        if (sightingDeltaPos.sqrMagnitude > 4f)
        {
            nav.destination = enemySight.personalLastSighting;
        }

        nav.speed = chaseSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;

            if (chaseTimer > chaseWaitTime)
            {
                lastPlayerSighted.position = lastPlayerSighted.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighted.resetPosition;
                chaseTimer = 0f;
            }
        }

        else
        {
            chaseTimer = 0f;
        }

    }

    void Patrolling ()
    {
        nav.speed = patrolSpeed;

        if (nav.destination == lastPlayerSighted.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length-1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0f;
            }
        }
        else
        {
            patrolTimer = 0f;
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;

    }

} // End EnemyAI