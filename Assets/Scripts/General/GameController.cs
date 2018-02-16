using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * IDEA FOR COMPLETE GAME REDESIGN: 
 * 
 * Instead of WAVE-BASED COMBAT, give the player a FUEL BAR
 * SHOOTING makes FUEL go DOWN
 * Killing ENEMIES makes them EXPLODE into FUEL, HEALTH, MONEY
 * 
 * The TWIST; the FUEL BAR is actually more like a SPEED BAR
 * 
 * MORE FUEL makes you GO FASTER
 * 
 * THE GAME CAN BE LEVEL BASED INSTEAD, WHERE THE PLAYER IS 
 * ENCOURAGED TO GET A TON OF FUEL AND SPEED TO THE END OF THE LEVEL
 * 
 * -POWER UP THAT MAKES YOU LOSE NO FUEL
 * -Enemy level goes up the further into the level you get
 **/

public class GameController : MonoBehaviour
{
    //UI Elements
    public Text[] UIElements = new Text[5];

    //Bunch of variables
    private int level = 1;
    public int enemyLevel = 2;
    // The player's score earned from destroying enemies and stuff
    public int score = 0;
    // The player's money earned from destroying enemies that they can spend
    public int money = 0;
	private int wave = 1;
	private int spawns = 5;
	public float spawnRate = 1.5f;
	public float waveRate = 7f;
	public int enemyWeapon;

    // Should the game wait before starting the wave???
    public bool waitBeforeWave = true;

    // Is it the start of a level?
    public bool levelStart = true;

    //Enemies to spawn
	public GameObject enemy;
	public GameObject mine;

	// Spawn point for enemies
	//private Vector2 enemySpawnPos;

    // Player Stuff
    private GameObject player;
    public PlayerManager playerMan;

    // The shop
    public GameObject shopEntrance;

    private void Start()
    {
		// Get the player object so the script can snatch some stuff off the player
		// IDK why this reference is here anymore, I think some other scripts
		// leech off of this one for player info
        player = GameObject.FindGameObjectWithTag("Player");
        playerMan = player.GetComponent<PlayerManager>();
        
        // Start spawning enemies
        StartCoroutine(spawnEnemies());
    }

    // Update is called once per frame
    void Update () 
	{
        // Update text
		updateText();
    }

    // Update UI text
    void updateText()
    {
        // Get the player's speed value, then "truncate" by taking substring
        string speedMsg = playerMan.Speed.ToString();
        int startIndex = speedMsg.IndexOf(".");
        UIElements[3].text = speedMsg.Substring(0, startIndex + 2);
    }

    public IEnumerator spawnEnemies()
    {
        Debug.Log("working");
        // If true,
        if (levelStart)
        {
            // Set up the level text element to show the right stuff
            UIElements[4].text = "Level " + level + " - " + (enemyLevel - 1);
            // Show the leveltext element
            UIElements[4].gameObject.SetActive(true);
            // Wait for a few seconds
            yield return new WaitForSeconds(3);
            // Hide the text
            UIElements[4].gameObject.SetActive(false);
        }
        // Make sure we don't wait again next time around
        //levelStart = false;

        // Disable the wait before wave cus yeah
        waitBeforeWave = false;

        // Spawn <spawns> enemies
        for(int enemies = 0; enemies <= spawns; enemies++)
        {
            // Generate a random spawn position for the enemy
            Vector2 spawnPos = new Vector2(9.5f, Random.Range(-4f, 4f));
            // Generate a random weapon for the enemy based on how high the current level is
            enemyWeapon = Random.Range(1, enemyLevel);
            // If the enemy's weapon is 1 or 2,
            if (enemyWeapon == 1 || enemyWeapon == 2)
                // Create a normal enemy object
                Instantiate(enemy, spawnPos, transform.rotation);
            // If the enemy's weapon is 3,
            else if (enemyWeapon == 3)
                // Create a mine
                Instantiate(mine, spawnPos, transform.rotation);
            // Wait to spawn another enemy
            yield return new WaitForSeconds(spawnRate);
        }

        // Set up next wave
        StartCoroutine(nextWaveSetup());
        yield break;
    }

    public IEnumerator nextWaveSetup()
    {
        //int shopSpawn = (int)Random.Range(1, 4);
        //if (shopSpawn == 3)
        // Spawn the shop entrance
        Instantiate(shopEntrance, new Vector3(12, 0, 0), transform.rotation);
        // Wait to start the next wave
        yield return new WaitForSeconds(waveRate);

        // Transition into the next wave
        // While the spawnRate is above a certain num,
        if (spawnRate >= .5f)
            // Decrease it
            spawnRate -= .1f;
        // Increase the range of enemies allowed
        enemyLevel++;
        // Double the spawns
        spawns *= 2;
        // Start a new sublevel
        levelStart = true;
        // Go back to spawning enemies
        StartCoroutine(spawnEnemies());
        yield break;
    }

    // Called by the shop entrance after stopping all other coroutines; 
    // makes the game wait to spawn enemies and stuff
    public IEnumerator shopWait()
    {
        waitBeforeWave = true;
        while (true)
            yield return new WaitForSeconds(1);
    }
}