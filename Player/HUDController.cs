/* -----------------------------------------------------------------------------------
 * Class Name: HUDController
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

public class HUDController : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public RectTransform missionNeedle;
    public Image playerHealth;
    public Text blendText;

    public Transform playerFamily;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    Vector3 northDirection;
    GameObject player;
    Quaternion missionDirection;

	
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
        player = GameObject.FindGameObjectWithTag(Tags.player);

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
        UpdateNorthDirection();
        UpdateMissionDirection();
        UpdateHealthBar();

	} //End Update

    void UpdateNorthDirection ()
    {
        northDirection.z = player.transform.eulerAngles.y;

    }

    void UpdateMissionDirection ()
    {
        Vector3 dir = player.transform.position - playerFamily.transform.position;

        missionDirection = Quaternion.LookRotation(dir);

        missionDirection.z = -missionDirection.y;
        missionDirection.x = 0;
        missionDirection.y = 0;

        missionNeedle.localRotation = missionDirection * Quaternion.Euler(northDirection);

    }

    void UpdateHealthBar ()
    {
        PlayerHealth curHealth = player.GetComponent<PlayerHealth>();

        playerHealth.rectTransform.sizeDelta = new Vector2(playerHealth.rectTransform.sizeDelta.x, curHealth.health);

    }

} // End HUDController