/* -----------------------------------------------------------------------------------
 * Class Name: NPCController
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

public class NPCController : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float deadZone = 10f;
    public CharacterTeam team;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] GameObject clothes;
    NavMeshAgent nav;
    Animator anim;
    HashIDs hash;
    Transform player;
    EnemySight enemySight;
    AnimatorSetup animSetup;
	
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
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        nav.updateRotation = false;
        animSetup = new AnimatorSetup(anim, hash);

        anim.SetLayerWeight(1, 1f);

        deadZone *= Mathf.Deg2Rad;

        SetClothingColor();

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
        NavAnimSetup();

	} //End Update

    void OnAnimatorMove()
    {
        if (Time.timeScale != 0 && Time.deltaTime > 0)
            nav.velocity = anim.deltaPosition / Time.deltaTime;

        transform.rotation = anim.rootRotation;
    }

    void NavAnimSetup ()
    {
        float speed;
        float angle;

        if (enemySight.playerInSight)
        {
            speed = 0f;

            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);

        }

        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }

        animSetup.Setup(speed, angle);
    }

    float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
        {
            return 0f;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);

        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;

        return angle;
    }

    void SetClothingColor ()
    {
        switch (team)
        {
            case CharacterTeam.Soldier:
                clothes.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
                break;
            case CharacterTeam.Guard:
                clothes.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
                break;
        }
    }

} // End NPCController