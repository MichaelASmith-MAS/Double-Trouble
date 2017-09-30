/* -----------------------------------------------------------------------------------
 * Class Name: EnemySight
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
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;
    public float fireRate = .5f;
    public float fireForce = 100f;
    public GameObject gunBarrel;
    public Rigidbody bullet;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    NavMeshAgent nav;
    SphereCollider col;
    Animator anim;
    LastPlayerSighted lastPlayerSighted;
    GameObject player;
    Animator playerAnim;
    PlayerHealth playerHealth;
    HashIDs hash;
    Vector3 previousSighting;
    float fireTimer;
	
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
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lastPlayerSighted = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighted>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        SphereCollider[] tmp = GetComponents<SphereCollider>();

        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i].radius == 10)
            {
                col = tmp[i];
            }
        }

        personalLastSighting = lastPlayerSighted.resetPosition;
        previousSighting = lastPlayerSighted.resetPosition;
        ResetTimer();

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
		if (lastPlayerSighted.position != previousSighting && CalculatePathLength(lastPlayerSighted.position) <= (col.radius * 3))
        {
            personalLastSighting = lastPlayerSighted.position;
        }

        previousSighting = lastPlayerSighted.position;

        if (playerHealth.health > 0f)
        {
            anim.SetBool(hash.playerInSightBool, playerInSight);
        }
        else
        {
            anim.SetBool(hash.playerInSightBool, false);
        }

        fireTimer += Time.deltaTime;

	} //End Update

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Tags.enemy)
        {
            if (other.GetComponent<NPCController>().team != GetComponent<NPCController>().team)
            {
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < fieldOfViewAngle * .5f)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                    {
                        if (hit.collider.tag == Tags.enemy)
                        {
                            personalLastSighting = other.transform.position;

                            if (fireRate <= fireTimer)
                            {
                                FireAtEnemy();
                            }
                        }

                    }
                }

            }
        }

        else if (other.gameObject == player)
        {
            if (player.GetComponent<PlayerController>().team != GetComponent<NPCController>().team)
            {
                playerInSight = false;

                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < fieldOfViewAngle * .5f)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                    {
                        if (hit.collider.gameObject == player)
                        {
                            playerInSight = true;
                            lastPlayerSighted.position = player.transform.position;

                            if (fireRate <= fireTimer)
                            {
                                FireAtEnemy();
                            }

                        }

                    }
                }

                int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
                int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

                if (playerLayerZeroStateHash == hash.walkingState || playerLayerZeroStateHash == hash.runningState)
                {
                    if (CalculatePathLength(player.transform.position) <= col.radius)
                    {
                        personalLastSighting = player.transform.position;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }

    float CalculatePathLength (Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;

    }

    public void FireAtEnemy ()
    {
        Rigidbody bulletInstance = Instantiate<Rigidbody>(bullet, gunBarrel.transform.position, bullet.transform.rotation);

        bulletInstance.velocity = gunBarrel.transform.forward * fireForce;

        gunBarrel.GetComponent<AudioSource>().Play();

        ResetTimer();
    }

    void ResetTimer ()
    {
        fireTimer = 0f;
    }

} // End EnemySight