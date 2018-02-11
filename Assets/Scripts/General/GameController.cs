using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //UI Elements
	public Text scoreText;
	public Text waveText;
    public Text healthText;
    public Text speedText;
	//public Text gameOverText;
	//public Text resetText;
	// num that screen height will be divided by
	// to determine the font size of the text
	int textSizeDivisor = 15;

    //Bunch of variables
    public int enemyLevel = 2;
    public int score = 0;
	//public int weapon = 1;
	private int wave = 1;
	private int spawns = 5;
	private int currentSpawns = 5;
	public float spawnRate = 1.5f;
	private float nextSpawn;
	public float waveRate = 7f;
	private float nextWave;
	public int enemyWeapon;

    //Enemies to spawn
	public GameObject enemy;
	public GameObject mine;

	//Spawn point for enemies
	private Vector2 enemySpawnPos;

    //Player Stuff
    private GameObject player;
    public PlayerManager playerMan;

    private void Start()
    {
		// Get the player object so the script can snatch some stuff off the player
		// IDK why this reference is here anymore, I think some other scripts
		// leech off of this one for player info
        player = GameObject.FindGameObjectWithTag("Player");
        playerMan = player.GetComponent<PlayerManager>();
        /*while(Screen.height % textSizeDivisor != 0)
        {
            textSizeDivisor++;
        }*/
    }

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

    // Update is called once per frame
    void Update () 
	{
        // Update text
		updateText();

        // Begin Waves
        // Spawn enemies n stuff
        if (Time.time >= nextSpawn && currentSpawns > 0) 
		{
			// Reset timer
			nextSpawn = Time.time + spawnRate;
			// Generate a random enemy spawn position
				// NOTE: Move the spawn position and the killbox further right
			enemySpawnPos = new Vector2 (9.5f, Random.Range (-4f, 4f));
			// Pick a random weapon up to the current cap
			enemyWeapon = Random.Range(1, enemyLevel);
			if (enemyWeapon == 1 || enemyWeapon == 2)
				Instantiate (enemy, enemySpawnPos, transform.rotation);
			else if (enemyWeapon == 3)
				Instantiate (mine, enemySpawnPos, transform.rotation);
			currentSpawns--;
		}
        //Break between waves
		if (currentSpawns == 0) 
		{
			nextWave = Time.time + waveRate;
			currentSpawns--;
		}
        //Transition to next wave, increase spawns
		if (Time.time >= nextWave && currentSpawns == -1) 
		{
            if (spawnRate >= .5f)
                spawnRate -= .1f;
            enemyLevel++;
			spawns *= 2;
			currentSpawns = spawns;
			wave++;
        }
    }

    // Update UI text
    void updateText()
    {
        // Get the player's speed value, then "truncate" by taking substring
        string speedMsg = "" + playerMan.Speed;
        int startIndex = speedMsg.IndexOf(".");
        speedText.text = speedMsg.Substring(0, startIndex + 2);
    }

}