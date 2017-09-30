/* -----------------------------------------------------------------------------------
 * Class Name: PlayerHealth
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float health = 100f;
    public Canvas deathCanvas;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    Animator anim;
    PlayerController playerController;
    HashIDs hash;
    LastPlayerSighted lastPlayerSighted;
    public bool playerDeath;
	
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
        playerController = GetComponent<PlayerController>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        lastPlayerSighted = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighted>();

        deathCanvas.enabled = false;

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
		if (health <= 0f)
        {
            if (!playerDeath)
            {
                PlayerDying();
            }
            else
            {
                PlayerDead();
            }
        }

	} //End Update


    void PlayerDying()
    {
        playerDeath = true;
        anim.SetBool(hash.deadBool, true);
        
    }

    void PlayerDead ()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.dyingState)
        {
            anim.SetBool(hash.deadBool, false);
        }

        anim.SetFloat(hash.vertFloat, 0f);
        playerController.enabled = false;

        lastPlayerSighted.position = lastPlayerSighted.resetPosition;

        deathCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void Damage (float amount)
    {
        health -= amount;
    }

    public void Heal (float amount)
    {
        health = Mathf.Max(health + amount, 100f);

    }

} // End PlayerHealth