using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all things related to the player, like:
/// - Health UI component
/// - Health
/// - Speed
/// - Weapon and Ship
/// and probably a few other things that I'm forgetting about right now.
/// </summary>

// FIXME: Don't subtract from player speed when firing in powered up mode
// TODO: Make the player's booster turn blue and maybe change the color of their bullets to blue w/ blue trails
    // Maybe also increase the size of their bullets when they're in powered up mode?
    // Maybe bullets could have an explosion radius?

public class PlayerManager : MonoBehaviour
{
    // Player Health
    private int health = 3;
    private int maxHealth = 3;
    // Array that will hold the player's health cells shown on the UI
    GameObject[] healthCells = new GameObject[10];
    // Sorted version of the health cells
    GameObject[] sortedHealthCells = new GameObject[10];
    // The background for the health cells
    private GameObject healthCellBG;
    // Transform for adjusting the size of the bg later
    private RectTransform healthCellRect;
    // Does the player currently have invincibility?
	private bool iFrames = false;

    // SPEEEEEEED BOOST
    private float speed = 1.5f;
    // If you couldn't tell I just learned about properties and I love them :^)
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            if (speed <= 0.1)
                Health = 0;
            // TODO: Make the text blink blue or something every time the speed increases
        }
    }
    private float maxSpeed = 25.01f;
    // The player's max speed
    public float MaxSpeed
    {
        get { return maxSpeed; }
        set
        {
            // Increase the player's max speed
            maxSpeed = value;
            // Set the player's current speed to the max speed
            Speed = maxSpeed;
        }
    }
    // Factor to be taken off of player's speed; gets set later in a method
    float speedSub = 0;
    // Powered-Up mode that player enters when they hits max speed
    bool poweredUp = false;
    // Two variables to hold the initial values that they were so they can be set back
    float fireRateBackup;
    int moveSpeedBackup;
    /// <summary>
    /// Changes the player's speed and firing rate or not based on <see cref="T:PlayerManager"/> powered up.
    /// </summary>
    /// <value><c>true</c> if powered up; otherwise, <c>false</c>.</value>
    bool PoweredUp
    {
        get { return poweredUp; }
        set
        {
            // If the powered up mode was just enabled,
            if(value)
            {
                // Remove the fire rate limit on the player's weapon
                fireScript.fireRate = 0f;
                // Make the player move a little faster
                movement.moveSpeed = 2 + moveSpeedBackup;
                // Make their trail blue maybe???
                // Make bullets + bullet trail blue maybe???
            }
            // If the player isn't powered up,
            else
            {
                // Keep the fire rate and movement speed at their normal values
                fireScript.fireRate = fireRateBackup;
                movement.moveSpeed = moveSpeedBackup;
            }
        }
    }
    private int weapon = 1;

    /// <summary>
    /// Store the player's weapon, play an animation and change ship when the value changes
    /// </summary>
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

    // The player's firing script
    PlayerFire fireScript;

    // Player movement script
    PlayerMovement movement;

    // Sprite shit
    private SpriteRenderer sr;

    // GameController
    GameObject gameController;
    GameController controller;

    // Sprites n shit
    public Sprite ship1;
    public Sprite ship2;

    // Stuff that's gonna get instantiated
	public GameObject resetUI;
    public GameObject poof;
    public GameObject deathExplosion;
    public GameObject hurtSound;

	void Start ()
    {
        // Getting components n shit
        sr = gameObject.GetComponent<SpriteRenderer>();
		booster = gameObject.GetComponent<ParticleSystem> ();
        healthCellBG = GameObject.FindGameObjectWithTag("HealthCellBG");
        fireScript = gameObject.GetComponent<PlayerFire>();
        movement = gameObject.GetComponent<PlayerMovement>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        controller = gameController.GetComponent<GameController>();

        // Set up backup variables for powerup stuff
        fireRateBackup = fireScript.fireRate;
        moveSpeedBackup = movement.moveSpeed;

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
        // Get the rect transform of the healthcellbg
        healthCellRect = (RectTransform)healthCellBG.transform;
        // Adjust the size to fit the 3 health cells
        healthCellRect.sizeDelta = new Vector2(16.3f * health + 5f, healthCellRect.sizeDelta.y);
    }

    /// <summary>
    /// Health value that other objects, like enemies and the shop, will access.
    /// When the value is changed, it will update the amount of health cells
    /// shown in the UI component to represent the player's health.
    /// </summary>
    /// <value>The new value being passed in.</value>
    public int Health
    {
        get { return health; }
        set
        {
            // If the player's health decreased,
            if(value < health)
            {
                // Play a hurt sound
                GameObject hurt = (GameObject)Instantiate(hurtSound, transform.position, transform.rotation);
                // Get rid of the obj a lil bit later cus garbage collection
                Destroy(hurt, .2f);
            }
            // Update health
            health = value;
            // Make sure the health cells on the UI reflect the player's health
            updateHealthCells(sortedHealthCells);
            // If the player's health reaches zero or somehow below that,
            if (health <= 0)
            {
                // Explode into a bunch of bits
                Instantiate(deathExplosion, transform.position, transform.rotation);
                // Kill the player
                Destroy(gameObject);
                // Show the reset button
                showResetButton();
                // Stop the wave sequences n stuff
                controller.StopAllCoroutines();
            }
        }
    }

    /// <summary>
    /// Sets the player's max health and updates the UI health component's size
    /// based on the player's current max health.
    /// </summary>
    /// <value>The max health.</value>
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            if (value <= 10)
            {
                maxHealth = value;
                // Be nice and give the player max health when their maxhealth increases
                Health = maxHealth;
                // Adjust the size of the bg to compensate for an extra health cell
                healthCellRect.sizeDelta += new Vector2(16.3f, 0f);
            }
        }
    }
    
    void Update ()
    {
        //Debug.Log(Speed);
        // Kill the player when health is less than 0
    }

    private void FixedUpdate()
    {
        subtractSpeed();
    }

    /// <summary>
    /// Subtracts speed from the player based on whether they're powered up or not.
    /// Doesn't subtract speed from the player if the game is in a waiting phase,
    /// like in a shop or in between waves.
    /// </summary>
    void subtractSpeed()
    {
        // If the game isn't in a break period right now, 
        if(!controller.waitBeforeWave)
        {
            // Constantly lower the player's speed, encourage v i o l e n c e
            if (speed > 0f && speed < 20f)
            {
                PoweredUp = false;
                speedSub = .0025f;
            }
            // If the player's speed is at the cap or somehow above,
            if (speed >= 25f)
            {
                // Make the player powered up
                PoweredUp = true;
                // Set the amount removed from their speed each frame higher
                speedSub = .025f;
            }
            // Take the subrate off of the player's speed
            Speed -= speedSub;
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

    // FIXME: Fix the alpha change
    /// <summary>
    /// Takes 1 HP from the player and plays some fancy effects to let the player
    /// know that they just got hit.
    /// </summary>
    /// <param name="enemyBullet">The enemy's bullet that will get destroyed.</param>
	IEnumerator subtractHealth(Collision2D enemyBullet)
	{
		// Take 1 HP
		Health--;
		// Destroy the enemy's bullet
		Destroy (enemyBullet.gameObject);
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

    /// <summary>
    /// Shows the reset and main menu buttons.
    /// </summary>
	void showResetButton()
	{
		// Create the gameover text and the reset text
		Instantiate (resetUI, new Vector3 (0, 0, 0), transform.rotation);
	}

    /// <summary>
    /// Changes the player's ship and makes a lil poof.
    /// </summary>
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

    /// <summary>
    /// Updates the UI health element.
    /// </summary>
    /// <param name="cells">The health cells shown on the UI.</param>
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