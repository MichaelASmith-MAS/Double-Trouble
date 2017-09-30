/* -----------------------------------------------------------------------------------
 * Class Name: PlayerController
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

public class PlayerController : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public Camera chaseCam, aimCam;
    public GameObject gunBarrel;
    public Rigidbody bullet;
    public float fireForce = 100f;
    public float fireRate = 2f;
    public HUDController hud;
    public CharacterTeam team = CharacterTeam.Base;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] float damping = .1f;
    [SerializeField] GameObject clothes;

    bool sneak;
    Animator anim;
    HashIDs hash;
    float timer;
	
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
        sneak = false;
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

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
        Move();
        ShootingTimer();

        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetBool(hash.aimingBool) && timer >= fireRate)
            {
                Fire();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            sneak = !sneak;
            anim.SetBool(hash.sneakingBool, sneak);
        }

        if (sneak)
        {
            Sneak();
        }

        if (Input.GetMouseButton(1))
        {
            anim.SetBool(hash.aimingBool, true);
            aimCam.enabled = true;
            chaseCam.enabled = false;
            
        }
        else
        {
            anim.SetBool(hash.aimingBool, false);
            chaseCam.enabled = true;
            aimCam.enabled = false;
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            team = CharacterTeam.Base;
            ChangeTeam();
        }

	} //End Update

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: N/A
    // Argument types: N/A
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Runs each frame. Used to perform frame based checks and actions.
    // ------------------------------------------------------------------------------

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.clothes)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                team = other.GetComponent<Pickups>().clothes;
                ChangeTeam();
            }
        }

        else if (other.tag == Tags.ammo)
        {

        }

        else if (other.tag == Tags.health)
        {

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.clothes)
        {
            hud.blendText.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.clothes)
        {
            hud.blendText.enabled = false;
        }
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

    void Move ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        
        anim.SetBool(hash.runningBool, sprint);
        anim.SetFloat("VerticalSpeed", v, damping, Time.deltaTime);
        anim.SetFloat("HorizontalSpeed", h, damping, Time.deltaTime);
        
        if (sprint)
        {
            Sprint();
        }

        anim.speed = 1.5f;

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

    void Sneak ()
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

    void Fire ()
    {
        Rigidbody bulletInstance = Instantiate<Rigidbody>(bullet, gunBarrel.transform.position, gunBarrel.transform.rotation);

        bulletInstance.velocity = gunBarrel.transform.forward * fireForce;

        gunBarrel.GetComponent<AudioSource>().Play();
        gunBarrel.GetComponent<ParticleSystem>().Play();

        ResetTimer();
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

    void Sprint ()
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

    void ChangeTeam ()
    {
        switch (team)
        {
            case CharacterTeam.Base:
                clothes.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
                break;
            case CharacterTeam.Soldier:
                clothes.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
                break;
            case CharacterTeam.Guard:
                clothes.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
                break;
        }
    }

    void ShootingTimer ()
    {
        timer += Time.deltaTime;

    }

    void ResetTimer ()
    {
        timer = 0f;
    }

} // End PlayerController