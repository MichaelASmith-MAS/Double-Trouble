/* -----------------------------------------------------------------------------------
 * Class Name: AnimatorSetup
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;

public class AnimatorSetup
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float speedDampTime = 0.1f, angularSpeedDampTime = 0.7f, angleResponseTime = 0.6f;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    Animator anim;
    HashIDs hash;
	
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

    public AnimatorSetup (Animator animator, HashIDs hashIDs)
    {
        anim = animator;
        hash = hashIDs;
    }

    public void Setup (float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;

        anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }

} // End AnimatorSetup