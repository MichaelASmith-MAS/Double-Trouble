/* -----------------------------------------------------------------------------------
 * Class Name: HashIDs
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharacterTeam { Base, Soldier, Guard }

public class HashIDs : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public int dyingState;
    public int sneakingState;
    public int walkingState;
    public int runningState;
    public int idleState;
    public int aimingState;
    public int locomotionState;

    public int deadBool;
    public int vertFloat;
    public int horiFloat;
    public int runningBool;
    public int sneakingBool;
    public int aimingBool;
    public int speedFloat;
    public int angularSpeedFloat;
    public int shotFloat;
    public int aimWeightFloat;
    public int playerInSightBool;
	
	// ------------------------------------------------------------------------------
	// Protected Variables
	// ------------------------------------------------------------------------------



	// ------------------------------------------------------------------------------
	// Private Variables
	// ------------------------------------------------------------------------------


	
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
	void Awake () 
	{
        dyingState = Animator.StringToHash("Base Layer.Dying");
        sneakingState = Animator.StringToHash("Base Layer.Sneaking");
        walkingState = Animator.StringToHash("Base Layer.Walking");
        runningState = Animator.StringToHash("Base Layer.Running");
        idleState = Animator.StringToHash("UpperBody.Idling");
        aimingState = Animator.StringToHash("UpperBody.PlayerAiming");
        locomotionState = Animator.StringToHash("Base Layer.Locomotion");

        deadBool = Animator.StringToHash("Dead");
        aimingBool = Animator.StringToHash("Aiming");
        runningBool = Animator.StringToHash("Running");
        sneakingBool = Animator.StringToHash("Sneaking");
        vertFloat = Animator.StringToHash("VerticalSpeed");
        horiFloat = Animator.StringToHash("HorizontalSpeed");
        speedFloat = Animator.StringToHash("Speed");
        angularSpeedFloat = Animator.StringToHash("AngularSpeed");
        shotFloat = Animator.StringToHash("Shot");
        aimWeightFloat = Animator.StringToHash("AimWeight");
        playerInSightBool = Animator.StringToHash("PlayerInSight");

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
		
	} //End Update
} // End HashIDs