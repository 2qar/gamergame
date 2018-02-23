using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles behavior of the mine enemy and the meteor thingy
/// </summary>
public class EnemyManager1 : MonoBehaviour 
{
	//public int moveSpeed = 5;
    public Vector3 rotation = new Vector3(0, 0, 3);

	// GameController reference
	GameObject gameController;
	GameController controller;

    // Bullets to shoot, as you do with bullets
    public GameObject enemyBullet;

    // Noise for the enemy mine to make when it explodes
    public GameObject explosionSound;

    // The enemy's health
    public int Health
    {
        get { return 1; }
        set
        {
            if(gameObject.name.Contains("Mine"))
            {
                controller.playerMan.Weapon = 3;
                // Shoot out a bunch of bullets in a cirlce
                for (int pos = 0; pos < 360; pos += 45)
                    Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, pos)));

                // Shake the screen
                controller.shaker.ShakeCamera(.5f);
                // Play the explosion sound
                GameObject sound = Instantiate(explosionSound, transform.position, transform.rotation);
                // Destroy the sound object after it has finished playing
                Destroy(sound, .6f);
            }
        }
    }

	void Start()
	{
        // Get the gamecontroller
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		controller = gameController.GetComponent<GameController> ();
	}

	void FixedUpdate ()
	{
        // Rotate the object
        transform.Rotate(rotation);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		// Kill the player on collision
		if (other.gameObject.tag == "Player") 
		{
			controller.playerMan.Health = 0;
			if (gameObject.name == "Mine")
				Destroy (gameObject);
		}
		if (other.gameObject.tag == "PlayerBullet") 
		{
            Health--;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}