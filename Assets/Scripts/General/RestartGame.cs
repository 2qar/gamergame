using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
        // If the player presses the R key, restart the game
		if (Input.GetKeyDown (KeyCode.R)) 
			Application.LoadLevel (Application.loadedLevel);
	}
}
