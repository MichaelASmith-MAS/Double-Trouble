/* -----------------------------------------------------------------------------------
 * Class Name: CameraController
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

public class CameraController : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public GameObject target, lookTarget;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] float verticalOffset = 3f, horizontalOffset = 1f, distance = 5f, focusSmoothing = 5f;
    [SerializeField] float mouseSmoothing = 2f, mouseSensitivity = 2f;

    Vector2 mouseLook, smoothV;

    bool aiming = false;
    Vector3 targetPosition;

	// ------------------------------------------------------------------------------
    // GETTERS/SETTERS
    // ------------------------------------------------------------------------------

    public bool Aiming
    {
        get { return aiming; }
        set { aiming = value; }
    }

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

    void Update()
    {

    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: N/A
    // Argument types: N/A
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Runs each frame. Used to perform frame based checks and actions.
    // ------------------------------------------------------------------------------

    void LateUpdate () 
	{
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(mouseSensitivity * mouseSmoothing, mouseSensitivity * mouseSmoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / mouseSmoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / mouseSmoothing);
        mouseLook += smoothV;
        
        target.transform.rotation = Quaternion.AngleAxis(mouseLook.x, target.transform.up);

        targetPosition = target.transform.position + target.transform.up * verticalOffset + target.transform.right * horizontalOffset - target.transform.forward * distance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * focusSmoothing);

        transform.LookAt(lookTarget.transform);
        
    } //End LateUpdate
} // End CameraController