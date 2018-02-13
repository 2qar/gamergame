using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
        // If the player presses the R key, restart the game
		if (Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton7)) 
			Application.LoadLevel (Application.loadedLevel);
        // If the player presses the E key,
        if (Input.GetKeyDown(KeyCode.E))
            // Load the main menu scene
            SceneManager.LoadScene(0);
	}
}
