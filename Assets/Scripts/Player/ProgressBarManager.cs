using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressBarManager : MonoBehaviour
{
    // Get a reference to the game controller
    private GameController controller;

    // Store whether the player's line object has reached the boss trigger at the end of the level or not
    private bool BossTriggerCollision = false;

    // UI showing some thanks text for the player
    public GameObject thanksUI;

	// Use this for initialization
	void Start ()
    {
        // Get the GameController script
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    private void FixedUpdate()
    {
        // If the game isn't currently in a pause,
        if (!controller.waitBeforeWave && !BossTriggerCollision)
        {
            transform.position += new Vector3(.005f, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player's object on the line collides with an enemy spawn trigger,
        if (collision.gameObject.name.Contains("EnemySpawnTrigger"))
        {
            // Start a new wave
            controller.StartCoroutine(controller.StartWave());
            // Disable the trigger's collider so it doesn't trigger another wave
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        // If the player's line object collides with an enemy level trigger,
        if(collision.gameObject.name.Contains("EnemyLevelTrigger"))
        {
            // Increase the enemy's level
            controller.enemyLevel++;
            // Disable the trigger's collider to disable extra collisions
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
            
        /*
        // If colliding with the boss trigger,
        if(collision.gameObject.name.Contains("BossTrigger"))
        {
            // Update the state of the collision between the player and the boss trigger
            BossTriggerCollision = true;
            // Disable the trigger's collider to disable extra collisions
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // Start the boss sequence
            controller.StartCoroutine(controller.level1BossBattle());
        }
        */

        // If the player's gameobject slider thingy collides with the end level trigger,
        if(collision.gameObject.name.Contains("EndLevel"))
        {
            // Disable the trigger's collider to disable extra collisions
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // If the player is on the first level, move to the second level
            if (controller.level == 1)
                SceneManager.LoadScene(2);

            // If the player is on the second level, just show the thanks ui thingy
            if (controller.level == 2)
                Instantiate(thanksUI, new Vector3(0, 0, 0), transform.rotation);
        }

    }
}
