using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager1 : MonoBehaviour {

	//public int moveSpeed = 5;
    public Vector3 rotation = new Vector3(0, 0, 3);

	// GameController reference
	GameObject gameController;
	GameController controller;

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
		if (other.gameObject.tag == "Player") 
		{
			Destroy (other.gameObject);
			if (gameObject.name == "Mine")
				Destroy (gameObject);
		}
		if (other.gameObject.tag == "PlayerBullet") 
		{
			// Set player weapon to mine when
			// mine is destroyed
			if (gameObject.name == "Mine")
				controller.weapon = 3;
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
    }
}