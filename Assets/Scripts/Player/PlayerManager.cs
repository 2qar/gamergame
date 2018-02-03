using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Variable shit
    public int health = 3;
	private bool iFrames = false;

    // GameController reference shit
    private GameObject controller;
    private GameController gameController;

	// The player's particle booster
	ParticleSystem booster;

    // Sprite shit
    private SpriteRenderer sr;

    // Sprites n shit
    public Sprite ship1;
    public Sprite ship2;

	public GameObject resetUI;

	void Start ()
    {
        // Getting components n shit
        controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        sr = gameObject.GetComponent<SpriteRenderer>();
		booster = gameObject.GetComponent<ParticleSystem> ();
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
		// Kill the player on collision with an enemy
		/*if (collision.gameObject.tag == "Enemy") 
		{
			Destroy (collision.gameObject);
			Destroy(gameObject);
			showResetButton ();
		}*/
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
		//booster.main.startColor = new Color(255, 111, 0, 125);
		// Wait for 1 second
		yield return new WaitForSeconds (1f);
		// Change alpha back to full
		sr.color = new Color(255, 255, 255, 255);
		// Change alpha of booster back to full
		//booster.main.startColor = Color(255, 111, 0, 255);
		// Disable invincibility
		iFrames = false;
		// Exit
		//yield break;
	}

	void showResetButton()
	{
		// Create the gameover text and the reset text
		Instantiate (resetUI, new Vector3 (0, 0, 0), transform.rotation);
	}
}