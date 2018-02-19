using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player movement by getting user input and adjusting the player's booster
/// based on the user's input.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    //Variables n shit
	public int moveSpeed = 50;

    //Components n shit
	Rigidbody2D rb;
    ParticleSystem booster;

    private bool stickMovement = false;
    private bool dpadMovement = false;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
        booster = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
        //Apply movement to the player's rigidbody
		Vector2 movement = GetMovementInput() * moveSpeed;
		rb.velocity = movement;
	}

    private void Update()
    {
        setBoosterSway();
    }

    /// <summary>
    /// Makes the player's booster react to their movement.
    /// </summary>
    void setBoosterSway()
    {
        // Get the velocity module from the booster particle system
        var forceOverLifetime = booster.velocityOverLifetime;
        // Enable the module for code
        forceOverLifetime.enabled = true;
        // Set the velocity on both the x and y relative to how the player is moving
        forceOverLifetime.x = -rb.velocity.x / 3;
        forceOverLifetime.y = -rb.velocity.y / 3;
    }

    /// <summary>
    /// Gets input from a keyboard or
    /// XBOX 360 controller (left stick + dpad)
    /// and prevents the multiple inputs from stacking.
    /// </summary>
    /// <returns>
    /// A Vector2 with movement based on the player's preferred input.
    /// </returns>
    Vector2 GetMovementInput()
    {
        // Variables that will apply movement
        float x = 0f;
        float y = 0f;

        // Store stick movement
        float stickLeftRight = Input.GetAxis("Horizontal");
        float stickUpDown = Input.GetAxis("Vertical");

        // Store dpad movement
        float dpadLeftRight = Input.GetAxis("dpad_leftright");
        float dpadUpDown = Input.GetAxis("dpad_updown");

        // If the stick is being used for movement and the dpad isn't being used,
        if ((stickLeftRight != 0 || stickUpDown != 0) && !dpadMovement)
        {
            // Apply movement based on the stick movement
            x = stickLeftRight;
            y = stickUpDown;
            // Update stickMovement to be true
            stickMovement = true;
        }
        // If the stick isn't being used or the dpad is being used,
        else
            // Update stickMovement to false
            stickMovement = false;

        // If the player is using the dpad for movement and the stick isn't also being used,
        if ((dpadLeftRight != 0 || dpadUpDown != 0) && !stickMovement)
        {
            // Apply movement based on dpad movement
            x = dpadLeftRight;
            y = dpadUpDown;
            // Update dpadMovement to be true
            dpadMovement = true;
        }
        // if the player isn't using the dpad for movement or the stick is being used,
        else
            // Update dpadMovement to false
            dpadMovement = false;

        return new Vector2(x, y);
    }

}
