using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all of the buttons on the main menu.
/// </summary>
public class MainMenuButtons : MonoBehaviour
{
    // The UI buttons on the main menu
    public Text[] buttons = new Text[3];
    // The UI element with options
    public Canvas optionsUI;
    // The current canvas
    private Canvas mainCanvas;
    // Stores whether the menu is showing or not
    private bool isShowing = true;
    public bool IsShowing
    {
        get { return isShowing; }
        set
        {
            if(isShowing != value && value == true)
                Index = 1;
            isShowing = value;
        }
    }

    // The little selector thingy
    public Image selector;
    RectTransform selectorPos;
    // The position of the selector in the button array,
    // default on play button
    public int index = 0;

    // Stores the current position of the selector
    private Vector2 pos;
    // Stores the position that the selector needs to go to
    private Vector2 newPos;

    // Positions of each of the UI buttons that the selector will go to
    private float[] selectorPositions = { 0, 50, 100 };

    // Stores the current time when the player presses a button to move the selector
    private float startTime;
    public float duration = .2f;

    private JoystickMenuInput joystick;

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
            // Get the current time
            startTime = Time.time;

            // Get the current position of the selector
            pos = selectorPos.anchoredPosition;
            // Get the new position for the selector to go to
            newPos = new Vector2(pos.x, selectorPositions[value] * -1);

            // Update the index
            index = value;

            joystick.ResetInputs();
        }
    }

	// Use this for initialization
	void Start ()
    {
        // Get this gameobject's canvas
        mainCanvas = gameObject.GetComponent<Canvas>();

        // Get the joystick menu input thingy
        joystick = gameObject.GetComponent<JoystickMenuInput>();

        // Get the selector position
        selectorPos = (RectTransform)selector.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Update whether the canvas is showing or not
        mainCanvas.enabled = isShowing;

        float t = (Time.time - startTime) / duration;
        // The position of the selector, but smoooooth
        float smoothStuff = Mathf.SmoothStep(pos.y, newPos.y, t);
        // Update the position of the selector
        selectorPos.anchoredPosition = new Vector2(selectorPos.anchoredPosition.x, smoothStuff);

        // If the player is actually on the main menu,
        if(isShowing)
        {
            // Check for an up or down key press and change the UI element to fit it
            changeSelectedElement();
            // Do the appropriate thing when the player presses the currently selected button
            pressButton();
        }
	}

    /// <summary>
    /// Get input from the player and update the index based on their input.
    /// </summary>
    void changeSelectedElement()
    {
        // If the player presses an up button,
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Up))
            // If they're at the top of the button list,
            if (index == 0)
                // Move to the bottom
                Index = 2;
            // If they're on any other element,
            else
                // Lower index, moving up
                Index--;
        // If the player presses a down button,
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Down))
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0))
            // If they're on the play button,
            if (index == 0)
                // Load the game
                SceneManager.LoadScene(1);
            // If they're on the options button,
            else if (index == 1)
            {
                isShowing = false;
            }
            // If they're on the exit button, 
            else if (index == 2)
                // Quit the game 
                Application.Quit();
    }

}