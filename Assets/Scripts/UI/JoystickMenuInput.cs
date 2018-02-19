using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMenuInput : MonoBehaviour
{
    private int dpadButtonPressed;
    private int DpadButtonPressed
    {
        get { return dpadButtonPressed; }
        set
        {
            int pressCount = 0;
            if(value != -1)
            {
                // If the last button pressed is different from the current one,
                if (dpadButtonPressed != value)
                {
                    // Set the current one being pressed to true
                    IsDpadButtonPressed[value] = true;
                    pressCount++;
                }
                //if(IsDpadButtonPressed[value]) 
                    // Set it back to false to prevent the whole being held thing
                    //IsDpadButtonPressed[value] = false;
            }
            dpadButtonPressed = value;
        }
    }

    // Holds the pressed state of each dpad button.
    // Button for each index: 
    // 0 = up
    // 1 = down
    // 2 = right
    // 3 = left
    public bool[] IsDpadButtonPressed = new bool[4];
	
	// Update is called once per frame
	void Update ()
    {
        // Get the button being pressed constantly
        DpadButtonPressed = DpadInputConverter();
	}

    /// <summary>
    /// Set the state of each button back to false.
    /// Use in a set statement for a UI button index to prevent the player from
    /// holding the dpad button and spamming through each button a million times.
    /// If this is put in a set statement for a UI index, each time the player presses
    /// a dpad button it will move button at a time instead of a million or smth
    /// </summary>
    public void ResetInputs()
    {
        // Make all of the inputs false again
        for (int pos = 0; pos < IsDpadButtonPressed.Length; pos++)
            IsDpadButtonPressed[pos] = false;
    }

    /// <summary>
    /// Checks if a dpad button is pressed.
    /// </summary>
    /// <param name="button">
    /// Button keycode passed in by user.
    /// See DpadButtons enum for a list of inputs.
    /// </param>
    /// <returns>
    /// True if the specified button is pressed,
    /// false if not.
    /// </returns>
    public bool IsButtonPressed(DpadButtons button)
    {
        return IsDpadButtonPressed[(int)button];
    }
    
    /// <summary>
    /// Take input from the dpad and convert it to a number.
    /// </summary>
    /// <returns>
    /// 0 = up on the dpad
    /// 1 = down on the dpad
    /// 2 = right on the dpad
    /// 3 = left on the dpad
    /// -1 = no input detected
    /// </returns>
    private int DpadInputConverter()
    {
        if (Input.GetAxis("dpad_updown") == 1)
            return 0;
        else if (Input.GetAxis("dpad_updown") == -1)
            return 1;
        else if (Input.GetAxis("dpad_leftright") == 1)
            return 2;
        else if (Input.GetAxis("dpad_leftright") == -1)
            return 3;
        else
            return -1;
    }

    /// <summary>
    /// Dpad input keycodes.
    /// </summary>
    public enum DpadButtons
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3
    }

    /// <summary>
    /// Writes the name of any button being pressed on a controller in the log.
    /// </summary>
    public static void ButtonPressedReporter()
    {
        // All of the buttons on a controller
        KeyCode[] joystickButtons =
        {
            KeyCode.JoystickButton0,
            KeyCode.JoystickButton1,
            KeyCode.JoystickButton2,
            KeyCode.JoystickButton3,
            KeyCode.JoystickButton4,
            KeyCode.JoystickButton5,
            KeyCode.JoystickButton6,
            KeyCode.JoystickButton7,
            KeyCode.JoystickButton8,
            KeyCode.JoystickButton9,
            KeyCode.JoystickButton10,
            KeyCode.JoystickButton11,
            KeyCode.JoystickButton12,
            KeyCode.JoystickButton13,
            KeyCode.JoystickButton14,
            KeyCode.JoystickButton15,
            KeyCode.JoystickButton16,
            KeyCode.JoystickButton17,
            KeyCode.JoystickButton18,
            KeyCode.JoystickButton19
        };
        // If any of the buttons on that list are pressed, write it's name in the log
        foreach (KeyCode button in joystickButtons)
            if (Input.GetKey(button))
                Debug.Log(button);
    }

}
