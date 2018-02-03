using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour 
{
	// Text references
	public Text gameOverText;
	public Text resetText;

	void Start()
	{
		// Get the text objects
		//gameOverText = GameObject.FindGameObjectWithTag ("GameOverText");
		//resetText = GameObject.FindGameObjectWithTag ("ResetText");
	}

	// Update is called once per frame
	void Update () 
	{
		updateTextSize ();
	}

	void updateTextSize()
	{
		// Set the font size for the text objects
		gameOverText.fontSize = textSizeCalc (10);
		resetText.fontSize = textSizeCalc (15);
	}

	int textSizeCalc(int divisor)
	{
		return Screen.height / divisor;
	}
}
