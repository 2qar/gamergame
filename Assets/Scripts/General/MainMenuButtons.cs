using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// TODO: Use Mathf.Smoothstep for a nice smooth title rotation
// TODO: Also use Mathf.Smoothstep for the selector cus it would probably look nice

/// <summary>
/// Handles all of the buttons on the main menu.
/// </summary>
public class MainMenuButtons : MonoBehaviour
{
    // The UI buttons on the main menu
    public Text[] buttons = new Text[3];
    // The UI element with options
    public Canvas optionsUI;
    // The little selector thingy
    public Image selector;
    RectTransform selectorPos;
    // The position of the selector in the button array,
    // default on play button
    private int index = 0;
    /// <summary>
    /// Moves the selector based on what value index currently is compared to
    /// the value being passed in.
    /// </summary>
    /// <value>The new index value being passed in.</value>
    int Index
    {
        get { return index; }
        set
        {
            // Space between one button
            int moveFact = 50;
            if (value > index)
                moveFact *= -1;
            // If the selector is at the top element and the value passed in is
            // the bottom element,
            if (index == 0 && value == 2)
                // Set the position the selector will move to at 2 buttons below
                moveFact = -100;
            // If the selector is at the bottom element and the value passed in is
            // the top element,
            if (index == 2 && value == 0)
                // Set the position the selector will move to at 2 buttons above
                moveFact = 100;
            // Get the current position of the selector
            Vector2 pos = selectorPos.anchoredPosition;
            // Get the new position for the selector to go to
            Vector2 newSelectorPos = new Vector2(pos.x, pos.y + moveFact);
            // Make it go there
            selectorPos.anchoredPosition = newSelectorPos;
            // Update the index
            index = value;
        }
    }

	// Use this for initialization
	void Start ()
    {
        // Get the selector position
        selectorPos = (RectTransform)selector.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Check for an up or down key press and change the UI element to fit it
        changeSelectedElement();
        // Do the appropriate thing when the player presses the currently selected button
        pressButton();
	}

    /// <summary>
    /// Get input from the player and update the index based on their input.
    /// </summary>
    void changeSelectedElement()
    {
        // If the player presses an up button,
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            // If they're at the top of the button list,
            if (index == 0)
                // Move to the bottom
                Index = 2;
            // If they're on any other element,
            else
                // Lower index, moving up
                Index--;
        // If the player presses a down button,
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            // If they're already at the bottom of the list,
            if (index == 2)
                // Go back to the top
                Index = 0;
            // If they're on any other element,
            else
                // Add to index, going down
                Index++;
    }

    /// <summary>
    /// Get input from the player and do stuff when they press buttons.
    /// </summary>
    void pressButton()
    {
        // If the player gives input to press the currently selected item,
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            // If they're on the play button,
            if (index == 0)
                // Load the game
                SceneManager.LoadScene(1);
            // If they're on the options button,
            else if (index == 1)
            {
                // Show the options menu
                optionsUI.gameObject.SetActive(true);
                // Hide the main menu
                gameObject.SetActive(false);
            }
            // If they're on the exit button, 
            else if (index == 2)
                // Quit the game somehow
                //Application.Quit;
                Debug.Log("Quit");
    }

}