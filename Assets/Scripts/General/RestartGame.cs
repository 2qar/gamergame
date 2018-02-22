using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Restarts the game or goes to the main menu based on the user's input.
/// </summary>
public class RestartGame : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
        // If the player presses the R key, restart the game
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton7))
            SceneManager.LoadScene(1);
        // If the player presses the E key,
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton6))
            // Load the main menu scene
            SceneManager.LoadScene(0);
	}
}
