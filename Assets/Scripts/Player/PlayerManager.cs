using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Variable shit
    public int health = 3;

    // GameController reference shit
    private GameObject controller;
    private GameController gameController;

    // Sprite shit
    private SpriteRenderer sr;

    // Sprites n shit
    public Sprite ship1;
    public Sprite ship2;

	void Start ()
    {
        // Getting components n shit
        controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        sr = gameObject.GetComponent<SpriteRenderer>();

	}
	
	void Update ()
    {
        // Give player right sprite for right weapon
        if (gameController.weapon == 1)
            sr.sprite = ship1;
        if (gameController.weapon == 2)
            sr.sprite = ship2;
		
        // Kill the player when health is less than 0
        if (health <= 0)
            Destroy(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Take health off when the player gets hit
        if(collision.transform.tag == "EnemyBullet")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }

	public IEnumerator subtractHealth()
	{
		while (true) 
		{
			health--;
			//Change alpha to half
			//Disable collider
			yield return new WaitForSeconds (1);
			//Change alpha to full
		}
	}
}
