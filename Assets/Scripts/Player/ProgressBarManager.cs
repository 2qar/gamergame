using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    // Get a reference to the game controller
    private GameController controller;

	// Use this for initialization
	void Start ()
    {
        // Get the GameController script
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    private void FixedUpdate()
    {
        // If the game isn't currently in a pause,
        if (!controller.waitBeforeWave)
        {
            transform.position += new Vector3(.01f, 0, 0);
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
    }
}
