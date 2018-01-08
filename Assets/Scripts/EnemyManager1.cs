using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager1 : MonoBehaviour {

	//public int moveSpeed = 5;
    public Vector3 rotation = new Vector3(0, 0, 3);
	private int enemyWeapon;

	//private GameObject gameController;
	//private GameController controller;

	void Start()
	{
		//enemyWeapon = Random.Range (1, 3);
		//Debug.Log (enemyWeapon); //Check weapon
		//gameController = GameObject.FindGameObjectWithTag ("GameController");
		//controller = gameController.GetComponent<GameController> ();
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
		}
		if (other.gameObject.tag == "PlayerBullet") 
		{
			Destroy (other.gameObject);
			/*if (gameObject.name == "Mine") 
			{
				controller.score++;
				controller.weapon = enemyWeapon;
			}*/
            Destroy(transform.parent.gameObject);
			Destroy (gameObject);
		}
        if (other.gameObject.name == "EnemyDeleter")
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
