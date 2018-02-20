using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the behavior and health of the single fire and triple fire enemies.
/// </summary>
public class EnemyManager2 : MonoBehaviour 
{

    public float moveSpeed = 5;
    private int enemyWeapon;
    // Enemy's health value
	private int health = 1;
    // Health value to be accessed by everything else,
    // so that when other objects change the enemy's health, a hit effect will play
    public int Health
    {
        get { return health; }
        set
        {
            // Play the hit effect
            StartCoroutine(hitEffect(sr));
            // If the enemy has no health left,
            if (value <= 0)
            {
                // Destroy the enemy
                Destroy(gameObject);
                // Give the player the enemy's weapon
                controller.playerMan.Weapon = enemyWeapon;
                // Increase the player's score
                controller.score++;
                // Explode into a bunch of particles
                Instantiate(explosion, transform.position, transform.rotation);
            }
            // Update the enemy's health value
            health = value;
        }
    }
    // The explosion for the enemy to create on death
    public GameObject explosion;

    // Sound Effect objects
    public GameObject bulletSound;
    public GameObject hurtSound;

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

	SpriteRenderer sr;

	void Start()
    {
        // Get GameController script
        gameController = GameObject.FindGameObjectWithTag("GameController");
        controller = gameController.GetComponent<GameController>();
		// Get SpriteRenderer component so the sprite can be changed
        sr = GetComponent<SpriteRenderer>();
		// Get the BoxCollider so the size can be changed
		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
        // Set weapon and ship
		enemyWeapon = controller.enemyWeapon;
		// If the weapon is 2, triple blast ship
		if (enemyWeapon == 2) 
		{
			sr.sprite = altShip;
			health = 2;
            collider.size = new Vector2(.6f, .9f);
		}
		// Set nextFire on startup so that the enemy doesn't just
		// immediately shoot when they spawn
		nextFire = Time.time + Random.Range(0.0f, 1.0f);
    }

    private void Update()
    {
        // Change the enemy's move speed based on the player's speed
        moveSpeed = 3 + (controller.playerMan.Speed / 10);

        // Checks to fire weapon
        if (Time.time >= nextFire)
			fireWeapon (enemyWeapon);
    }
		
    void FixedUpdate()
    {
		//Move the dude
        transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Kill the player on collision
        if (other.gameObject.tag == "Player")
        {
            //Destroy(other.gameObject);
            // Update the health to 0 so no funky stuff happens
            controller.playerMan.Health = 0;
        }

        // Take enemy health when shot, destroy player bullet object
		// also flash white cus muh game feel
		if (other.gameObject.tag == "PlayerBullet") 
		{
			Destroy (other.gameObject);
			Health--;
		}
    }

    /// <summary>
    /// Fires a certain weapon based on the enemy's weapon value that's passed in.
    /// </summary>
    /// <param name="weapon">The enemy weapon.</param>
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

        // Make a lil noise
        GameObject noise = (GameObject)Instantiate(bulletSound, transform.position, transform.rotation);
        // Destroy the object that makes the noise a lil bit after so there isn't garbage everywhere
        Destroy(noise, .5f);
	}

	/// <summary>
    /// Make the enemy flash white when hit.
    /// </summary>
    /// <param name="sr">The enemy's spriterenderer where the flash is applied.</param>
	IEnumerator hitEffect(SpriteRenderer sr)
	{
		// Change the ship color to white
		sr.color = new Color (255, 255, 255, 255);
        // Play the hit sound
        GameObject hurt = (GameObject)Instantiate(hurtSound, transform.position, transform.rotation);
        // Destroy it automatically a lil later cus garbage collection yada yada
        Destroy(hurt, .2f);
		// Wait for like a a millisecond
		yield return new WaitForSeconds(0.05f);
		// Change the ship color back to red
		sr.color = new Color (255, 0, 0, 255);
		// Exit the method
		yield break;
	}

}