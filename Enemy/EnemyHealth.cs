/* -----------------------------------------------------------------------------------
 * Class Name: EnemyHealth
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
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float health = 50f;
    public Image healthBar;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    Animator anim;
    HashIDs hash;
    NPCController enemyController;
    Pickups pickupController;
    bool enemyDeath;
    CapsuleCollider capsule;
    SphereCollider sightingCollider, pickupCollider;
    float startHealth;
	
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
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        enemyController = GetComponent<NPCController>();
        pickupController = GetComponent<Pickups>();
        capsule = GetComponent<CapsuleCollider>();
        SphereCollider[] tmp = GetComponents<SphereCollider>();
        startHealth = health;

        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i].radius == 10)
            {
                sightingCollider = tmp[i];
            }
            else
            {
                pickupCollider = tmp[i];
            }
        }

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
        UpdateHealthBar();

		if (health <= 0f)
        {
            if (!enemyDeath)
            {
                EnemyDying();
            }

            else
            {
                EnemyDeath();
            }
        }

	} //End Update

    void EnemyDying ()
    {
        enemyDeath = true;
        anim.SetBool(hash.deadBool, true);

    }

    void EnemyDeath ()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.dyingState)
        {
            anim.SetBool(hash.deadBool, false);
        }

        anim.SetFloat(hash.speedFloat, 0f);
        enemyController.enabled = false;
        pickupController.enabled = true;

        sightingCollider.enabled = false;
        capsule.enabled = false;
        pickupCollider.enabled = true;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemySight>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        
        
        gameObject.tag = Tags.clothes;
        
    }

    public void Damage (float amount)
    {
        health -= amount;

    }

    void UpdateHealthBar ()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x, health/startHealth);

    }

} // End EnemyHealth