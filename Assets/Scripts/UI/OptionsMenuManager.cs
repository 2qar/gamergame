using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles all of the buttons in the options submenu on the main menu.
/// </summary>
public class OptionsMenuManager : MonoBehaviour
{
    // The other UI stuff
    public Canvas mainUI;

    // List of all of the buttons
    public Text[] UIElements = new Text[3];
    // Button selector
    public Image selector;
    // Position in list that the selector is at
    int index = 0;

    // Used to store position of the selector and stuff
    RectTransform selectorPos;
    // Used to store the position of the button and stuff???
    RectTransform buttonPos;

    // The time when the movement begins
    private float startTime;
    // How long the movement should last
    public float duration = .3f;

    // Manages making each dpad button press once
    private JoystickMenuInput joystick;

    /// <summary>
    /// Updates the selector's position based on what the index currently is 
    /// compared to the new index value that's being passed in.
    /// </summary>
    /// <value>The index.</value>
    public int Index
    {
        get { return index; }
        set
        {
            // Update the start time
            startTime = Time.time;

            // Update the index
            index = value;
            // Get the selector's recttransform to set its position
            selectorPos = (RectTransform)selector.transform;
            // Get the new button's recttransform to get the position that the selector needs to move to
            buttonPos = (RectTransform)UIElements[index].transform;

            // Reset each of the buttons back to false
            joystick.ResetInputs();
        }
    }

    /// <summary>
    /// List of supported resolutions in the game.
    /// </summary>
    private int[][] resolutions =
    {
        new int[] {640, 360},
        new int[] {1024, 576},
        new int[] {1280, 720},
        new int[] {1920, 1080}
    };
    // Index for the resolution list
    int resIndex = 0;
    /// <summary>
    /// Updates the resolution being shown based on the current index,
    /// and moves to the correct index based on the current value versus the new one.
    /// </summary>
    /// <value>The index in the resolution array.</value>
    int ResIndex
    {
        get { return resIndex; }
        set
        {
            resIndex = value;
            // Update the text element
            UIElements[0].text = "< " + resolutions[resIndex][0] + "x" + resolutions[resIndex][1] + " >";

            // Reset dpad inputs to false
            joystick.ResetInputs();
        }
    }
    // False by default
    bool fullscreen = false;
    // Keep track of what value the player wants it at
    int fullscreenIndex = 0;

	// Use this for initialization
	void Start ()
    {
        // Get the joystick input manager
        joystick = gameObject.GetComponent<JoystickMenuInput>();

        selectorPos = (RectTransform)selector.transform;
        buttonPos = (RectTransform)UIElements[0].transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        selectorPositionUpdate();
        selectorMovement();
        resolutionSelector();
        getButtonInput();
        fullscreenSelector();
	}

    /// <summary>
    /// Updates the position of the selector based on the user's input.
    /// </summary>
    void selectorPositionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Up))
            if (index == 0)
                Index = 2;
            else
                Index--;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Down))
            if (index == 2)
                Index = 0;
            else
                Index++;
    }

    /// <summary>
    /// Moves the selector.
    /// </summary>
    void selectorMovement()
    {
        float t = (Time.time - startTime) / duration;
        float smoothStuff = Mathf.SmoothStep(selectorPos.anchoredPosition.y, buttonPos.anchoredPosition.y - 50, t);
        selectorPos.anchoredPosition = new Vector2(0, smoothStuff);
    }

    /// <summary>
    /// Gets button input from the player and does stuff based on
    /// what button they've pressed.
    /// </summary>
    void getButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0))
            // If the player hit enter on a button that isn't the back button,
            if (index != 2)
            {
                // Set the resolution AND apply fullscreen if the user set it to true
                Debug.Log("res set to: " + resolutions[resIndex][0] + "x" + resolutions[resIndex][1] + " | fullscreen = " + fullscreen);
                Screen.SetResolution(resolutions[resIndex][0], resolutions[resIndex][1], fullscreen);
            }
            else
            {
                // Show the main menu UI
                mainUI.gameObject.SetActive(true);
                // Hide this menu
                gameObject.SetActive(false);
            }
        else if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            // Show the main menu UI
            mainUI.gameObject.SetActive(true);
            // Hide this menu
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the currently selected resolution based on user input.
    /// </summary>
    void resolutionSelector()
    {
        if (index == 0)
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Left))
                if (resIndex == 0)
                    ResIndex = 3;
                else
                    ResIndex--;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Right))
                if (resIndex == 3)
                    ResIndex = 0;
                else
                    ResIndex++;
    }

    /// <summary>
    /// Updates the fullscreen state and its text based on the user's input.
    /// </summary>
    void fullscreenSelector()
    {
        if (index == 1)
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Left))
                if (fullscreenIndex == 0)
                    fullscreenIndex = 1;
                else
                    fullscreenIndex--;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Right))
                if (fullscreenIndex == 1)
                    fullscreenIndex = 0;
                else
                    fullscreenIndex++;

        if(fullscreenIndex == 0)
        {
            UIElements[1].text = "< off >";
            fullscreen = false;
        } 
        else
        {
            UIElements[1].text = "< on >";
            fullscreen = true;
        }
    }

}
