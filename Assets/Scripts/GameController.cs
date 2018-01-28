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

    //Bunch of variables
    public int enemyLevel = 2;
    public int score = 0;
	public int weapon = 1;
	private int wave = 1;
	private int spawns = 5;
	private int currentSpawns = 5;
	public float spawnRate = 1.5f;
	private float nextSpawn;
	public float waveRate = 100000f;
	private float nextWave;
	public int enemyWeapon;

    //Enemies to spawn
	public GameObject enemy;
	public GameObject mine;

	//Spawn point for enemies
	private Vector2 enemySpawnPos;

    //Player Stuff
    private GameObject player;
    private PlayerManager playerMan;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMan = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update () 
	{
        // Update text
		UpdateText();

		// Begin Waves
        // Spawn enemies n stuff
		if (Time.time >= nextSpawn && currentSpawns > 0) 
		{
			nextSpawn = Time.time + spawnRate;
			enemySpawnPos = new Vector2 (9.5f, Random.Range (-4f, 4f));
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

	// NOTE:
	// IF IT AIN'T BROKE, DON'T FIX IT
	// HINT: THAT SHIT BELOW IS A BROKE BOYE

	/*IEnumerator WaveManager()
	{
		while (currentSpawns > 0) 
		{
			Vector2 enemySpawn = new Vector2 (9.5f, Random.Range (-4f, 4f));
			Instantiate (enemy, enemySpawnPos, transform.rotation);
			currentSpawns--;
			yield return new WaitForSeconds(spawnRate);
		}
		while (currentSpawns == 0) 
		{
			yield return new WaitForSeconds (waveRate);
			currentSpawns--;
		}
		while(currentSpawns == -1)
		{
			if (spawnRate >= .5f)
				spawnRate -= .1f;
			enemyLevel++;
			spawns *= 2;
			currentSpawns = spawns;
		}
	}*/

	// Update the text when called
	void UpdateText()
	{
		healthText.text = "Health: " + playerMan.health;
		scoreText.text = "Score: " + score;
		waveText.text = "Wave " + wave;
	}
}
