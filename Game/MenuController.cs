/* -----------------------------------------------------------------------------------
 * Class Name: MenuController
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
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public Canvas mainMenu;
    public Canvas controlMenu;
    public Canvas creditsMenu;
    public Canvas introMenu;
    public Canvas hud;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    GameController gameController;

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
        gameController = gameObject.GetComponent<GameController>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.enabled = true;
            controlMenu.enabled = false;
            creditsMenu.enabled = false;
        }

        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            introMenu.enabled = true;

            hud.enabled = false;
            mainMenu.enabled = false;
            controlMenu.enabled = false;
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
	
	public void Return_To_Main ()
    {
        mainMenu.enabled = true;
        controlMenu.enabled = false;

        if (hud != null)
            hud.enabled = false;

        if (creditsMenu != null)
            creditsMenu.enabled = false;
        
    }

    public void ControlMenu ()
    {
        controlMenu.enabled = true;
        mainMenu.enabled = false;

        if (hud != null)
            hud.enabled = false;

        if (creditsMenu != null)
            creditsMenu.enabled = false;
        
    }

    public void CreditsMenu ()
    {
        creditsMenu.enabled = true;
        mainMenu.enabled = false;
        controlMenu.enabled = false;

    }

    public void NewGame ()
    {
        SceneManager.LoadScene(1);

    }

    public void ReturnToMainMenu ()
    {
        SceneManager.LoadScene(0);

    }

    public void ReturnToGame()
    {
        mainMenu.enabled = false;
        controlMenu.enabled = false;

        if (hud != null)
            hud.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;

        gameController.SetPaused();

    }

    public void StartGame ()
    {
        introMenu.enabled = false;

        if (hud != null)
            hud.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;

        gameController.SetPaused();

    }

    public void ExitGame ()
    {
        Application.Quit();

    }

} // End MenuController