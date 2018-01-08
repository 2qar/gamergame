using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables n shit
	public int moveSpeed = 50;

    //Components n shit
	Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
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
}
