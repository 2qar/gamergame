﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager2 : MonoBehaviour {

    public int moveSpeed = 5;
    private int enemyWeapon;
	private int health = 1;

    // Weapon Stuff
    float nextFire;
    public float fireRate = 2;
    public Vector3 bulletOffset = new Vector3(.2f, 0, 0);

    // Bullet and Ship objects
    public GameObject bullet;
    public Sprite altShip;

	// GameController object
    private GameObject gameController;
    private GameController controller;

	void Start()
    {
        // Get GameController script
        gameController = GameObject.FindGameObjectWithTag("GameController");
        controller = gameController.GetComponent<GameController>();
		// Get SpriteRenderer component so the sprite can be changed
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
		// Get the BoxCollider so the size can be changed
		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
        // Set weapon and ship
		enemyWeapon = controller.enemyWeapon;
		// If the weapon is 2, triple blast ship
		if (enemyWeapon == 2) 
		{
			sr.sprite = altShip;
			health = 2;
            collider.size = new Vector2(.6f, .7f);
		}
        //Debug.Log ("Enemy Weapon: " + enemyWeapon); //Check weapon
    }

    private void Update()
    {
        // Checks to fire weapon
        if (Time.time >= nextFire)
			fireWeapon (enemyWeapon);
		
		// Check enemy health & destroy enemy if health is 0
		if (health == 0) 
		{
			Destroy (gameObject);
			controller.weapon = enemyWeapon;
			controller.score++;
		}
    }
		
    void FixedUpdate()
    {
		//Move the dude
        transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Kill the player on collision
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            // Update the health to 0 so no funky stuff happens
            controller.playerMan.health = 0;
        }

        //Take enemy health when shot, destroy player bullet object
		if (other.gameObject.tag == "PlayerBullet") 
		{
			Destroy (other.gameObject);
			health--;
		}
    }

	void fireWeapon(int weapon)
	{
		// * * * * * * * * *
		// * Enemy Weapons *
		// * * * * * * * * *

		// Enemy Weapon #1: Single Fire
		// Simple single shot weapon with
		// normal cooldown
		if(enemyWeapon == 1)
			Instantiate(bullet, transform.position + bulletOffset, transform.rotation);

		// Enemy Weapon #2: Triple Shot
		// Same as the single shot with additional bullets
		// and normal cooldown
		if(enemyWeapon == 2)
			for (int reps = 15; reps >= -15; reps -= 15)
				Instantiate(bullet, transform.position + bulletOffset, Quaternion.Euler(new Vector3(0, 0, reps)));

		// Start cooldown
		nextFire = Time.time + fireRate;
	}

	// IDEA: When enemies die, the player gets a set amount of money
	// BUT
	// The enemy ship explodes into little bits, and the player can
	// collect these bits for extra cash at the risk of losing health
	// cus they have to get the bits and dodge enemy fire at the same
	// time

    // Add a little gravity field around the player that sucks up ship bits
}