/* -----------------------------------------------------------------------------------
 * Class Name: GameController
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

public class GameController : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public GameObject menuCanvas;
    public GameObject introCanvas;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    bool isPaused;
	
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
        isPaused = true;

        introCanvas.SetActive(true);

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
		if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            SetPaused();
            GetComponent<MenuController>().mainMenu.enabled = !GetComponent<MenuController>().mainMenu.enabled;
            GetComponent<MenuController>().hud.enabled = !GetComponent<MenuController>().hud.enabled;
        }

    } //End Update

    public void SetPaused ()
    {
        isPaused = !isPaused;

    }

} // End GameController