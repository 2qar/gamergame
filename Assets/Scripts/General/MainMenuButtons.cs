using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    int Index
    {
        get { return index; }
        set
        {
            int moveFact = 50;
            if (value > index)
                moveFact *= -1;
            if (index == 0 && value == 2)
                moveFact = -100;
            if (index == 2 && value == 0)
                moveFact = 100;
            Vector2 pos = selectorPos.anchoredPosition;
            Vector3 newSelectorPos = new Vector3(pos.x, pos.y + moveFact, 0);
            selectorPos.anchoredPosition = newSelectorPos;
            index = value;
        }
    }

	// Use this for initialization
	void Start ()
    {
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