using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player Health
    private int health = 3;
    // Array that will hold the player's health cells shown on the UI
    GameObject[] healthCells = new GameObject[10];
    // Sorted version of the health cells
    GameObject[] sortedHealthCells = new GameObject[10];
    // The health cell object that will be created on the UI
    //public GameObject healthCell;
    // Does the player currently have invincibility?
	private bool iFrames = false;

    // Store the player's weapon,
    // play an animation and change ship when the value changes
    private int weapon = 1;
    public int Weapon
    {
        get { return weapon; }
        set
        {
            // If the value didn't change, just ignore
            if (weapon == value) return;
            // If the value did change,

            // Store the value
            weapon = value;
            // Change ship + animate
            changeShip();
        }
    }

	// The player's particle booster
	ParticleSystem booster;

    // Sprite shit
    private SpriteRenderer sr;

    // Sprites n shit
    public Sprite ship1;
    public Sprite ship2;

    // Stuff that's gonna get instantiated
	public GameObject resetUI;
    public GameObject poof;

	void Start ()
    {
        // Getting components n shit
        sr = gameObject.GetComponent<SpriteRenderer>();
		booster = gameObject.GetComponent<ParticleSystem> ();

        // Set up the UI health element
        healthCells = GameObject.FindGameObjectsWithTag("HealthCell");
        // Sort the GameObjects into a seperate array
        // Run through each element in the new array
        for(int pos = 0; pos < sortedHealthCells.Length; pos++)
            // Run through each element in the old array
            for(int reps = 0; reps < healthCells.Length; reps++)
            {
                // Store the name of the current object
                string objName = "" + healthCells[reps];
                // If the object has the correct number for the current sorted array position,
                if(objName.Contains("" + pos))
                    // Put the cola in the slot
                    sortedHealthCells[pos] = healthCells[reps];
            }
        updateHealthCells(sortedHealthCells);
    }

    // Health value that everything else will access
    public int Health
    {
        get { return health; }
        set
        {
            // Update health
            health = value;
            // Make sure the health cells on the UI reflect the player's health
            updateHealthCells(sortedHealthCells);
        }
    }
    
    void Update ()
    {
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

        // TODO: Maybe use this instead of the 2 seperate bits of code in both of the 
        // EnemyManager scripts???

		// Kill the player on collision with an enemy
		/*if (collision.gameObject.tag == "Enemy") 
		{
			Destroy (collision.gameObject);
			Destroy(gameObject);
			showResetButton ();
		}*/
    }

    // TODO: Fix the alpha change
    // Subtracts from the player's health, gives a small frame of invincibility and
    // does some fancy visual stuff
	IEnumerator subtractHealth(Collision2D other)
	{
		// Take 1 HP
		Health--;
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

    // Shows the game over text and reset button
	void showResetButton()
	{
		// Create the gameover text and the reset text
		Instantiate (resetUI, new Vector3 (0, 0, 0), transform.rotation);
	}

    // Changes the player's ship and makes a lil poof
    void changeShip()
    {
        // Give player right sprite for right weapon
        if (Weapon == 1)
            sr.sprite = ship1;
        if (Weapon == 2)
            sr.sprite = ship2;
        // Make a lil poof and store it
        GameObject poofRef = Instantiate(poof, transform.position, transform.rotation);
        // Destroy it after 1 second of existing, how sad :(
        Destroy(poofRef, 1);
    }

    // Updates the UI health element
    void updateHealthCells(GameObject[] cells)
    {
        // Run through all of the objects in the array
        for (int pos = 0; pos < cells.Length; pos++)
            // If the current cell <= the player's health,
            if (pos < health)
                // Show it on the UI
                cells[pos].SetActive(true);
            // If the current cell is at a position greater than the player's health,
            else
                // Hide it on the UI
                cells[pos].SetActive(false);
    }
}