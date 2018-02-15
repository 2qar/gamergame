using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make mines explode into a bunch of bullets going in every direction

public class EnemyManager1 : MonoBehaviour {

	//public int moveSpeed = 5;
    public Vector3 rotation = new Vector3(0, 0, 3);

	// GameController reference
	GameObject gameController;
	GameController controller;

    // Bullets to shoot, as you do with bullets
    public GameObject enemyBullet;

	void Start()
	{
		//Debug.Log (enemyWeapon); //Check weapon
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		controller = gameController.GetComponent<GameController> ();
	}

	void FixedUpdate ()
	{
		//transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        transform.Rotate(rotation);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		// Kill the player on collision
		if (other.gameObject.tag == "Player") 
		{
			//Destroy (other.gameObject);
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
                {
                    Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, pos)));
                    Debug.Log("Bullet #" + (pos + 1));
                }
				//Debug.Log ("Weapon Changed");
			}
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
    }
}