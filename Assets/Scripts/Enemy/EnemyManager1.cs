using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Play a sound on death and maybe explode into some particles

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
			// Set player weapon to mine when
			// mine is destroyed
			if (gameObject.name.Contains("Mine")) 
			{
				controller.playerMan.Weapon = 3;
                // Shoot out a bunch of bullets in a cirlce
                for (int pos = 0; pos < 360; pos += 45)
                    Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, pos)));
			}
			Destroy (other.gameObject);
            Destroy(gameObject);
		}
    }
}