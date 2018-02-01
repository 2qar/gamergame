using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Variable shit
    public int health = 3;
	bool iFrames = false;

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
		{
			Destroy (gameObject);
			showResetButton ();
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Take health off when the player gets hit
        if(collision.transform.tag == "EnemyBullet" && !iFrames)
        {
			StartCoroutine (subtractHealth(collision));
        }
    }

	IEnumerator subtractHealth(Collision2D other)
	{
		// Take 1 HP
		health--;
		// Destroy the enemy's bullet
		Destroy (other.gameObject);
		// Maybe subtract fuel???
		// Enable iFrames so the player can't get clipped again
		iFrames = true;
		// Change alpha to half
		sr.color = new Color(255, 255, 255, 125);
		// Change alpha of booster to half

		// Wait for 1 second
		yield return new WaitForSeconds (1f);
		// Change alpha back to full
		sr.color = new Color(255, 255, 255, 255);
		// Change alpha of booster back to full

		// Disable invincibility
		iFrames = false;
		// Exit
		//yield break;
	}

	void showResetButton()
	{
		// Show the game over text and prompt to restart
		gameController.gameOverText.enabled = true;
		gameController.resetText.enabled = true;
	}
}
