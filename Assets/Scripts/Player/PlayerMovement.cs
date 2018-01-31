using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables n shit
	public int moveSpeed = 50;

    //Components n shit
	Rigidbody2D rb;
    ParticleSystem booster;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
        booster = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
        //Get input axis
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

        //Apply movement
		Vector2 movement = new Vector2 (x, y) * moveSpeed;
		rb.velocity = movement;
	}

    private void Update()
    {
        setBoosterSway();
    }

    // Makes the player's booster react to their movement
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
}
