using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteManager : MonoBehaviour
{
    // Stores whether there is a controller connected or not
    private bool xboxControllerIsConnected = false;
    private bool XboxControllerIsConnected
    {
        get { return xboxControllerIsConnected; }
        set
        {
            // if a controller was connected, assign the object a number for the right button prompt
            if (value == true)
                ButtonNumber = AssignControllerButtonNumber();
            xboxControllerIsConnected = value;
        }
    }

    // Store the object's image 
    private Image image;

    // Button prompts for a keyboard
    public Sprite[] KeyboardPrompts = new Sprite[15];
    // Assigned number based on the object's name
    private int KeyNumber;
    // Stores whether the arrow keys were the last movement keys pressed
    private bool ArrowsLastPressed = true;

    // All of the button prompts for a 360 controller
    public Sprite[] ControllerButtonPrompts = new Sprite[15];
    // Assigned number based on the object's name
    private int ButtonNumber;

    // Get the joystick menu stuffs
    private JoystickMenuInput joystick;

	// Use this for initialization
	void Start ()
    {
        // Get the image component
        image = gameObject.GetComponent<Image>();

        // Get the joystick input converter thingy
        joystick = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<JoystickMenuInput>();

        // Assign the keynumber, assuming the player will be using a keyboard by default
        KeyNumber = AssignKeyNumber();
        image.sprite = KeyboardPrompts[ArrowKeyNumAssigner()];
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get the names of the connected joysticks
        string[] joystickNames = Input.GetJoystickNames();
        // If there is a controller connected,
        if (joystickNames.Length != 0)
            // If that controller is a 360 controller,
            if (joystickNames[0] == "Controller (XBOX 360 For Windows)")
                XboxControllerIsConnected = true;
            else
                XboxControllerIsConnected = false;

        // If a 360 controller is connected, update the sprites using controller button prompts
        if (XboxControllerIsConnected)
            ControllerButtonSetup(ButtonNumber);

        // If the player isn't using a 360 controller, update the button prompts with keyboard sprites
        else
            KeyboardButtonSetup(KeyNumber);
	}

    /// <summary>
    /// Assigns a key number based on the object's name, kinda relies on the names being named after controller buttons.
    /// </summary>
    /// <returns>
    /// A key number corresponding to an index in KeyboardPrompts.
    /// 0 = escape key
    /// 1 = space bar
    /// 2 = arrow keys / wasd
    /// -1 = no sprite / name not recognized
    /// </returns>
    int AssignKeyNumber()
    {
        switch (gameObject.name)
        {
            case "B_button":
                return 0;
            case "A_button":
                return 1;
            case "X_button":
                return -1;
            case "Y_button":
                return -1;
            case "dpad":
                return 2;
            case "thumbstick":
                return 2;
        }
        return -1;
    }

    /// <summary>
    /// Animates the keyboard sprite if the sprite is taking arrow key or wasd key input.
    /// </summary>
    /// <param name="keyNum">
    /// The assigned key num of the object.
    /// </param>
    void KeyboardButtonSetup(int keyNum)
    {
        if (keyNum == 2)
        {
            ArrowKeyAnimator();
            MovementKeyChecker();
        }
        else
            image.sprite = KeyboardPrompts[keyNum];
    }

    /// <summary>
    /// Actually animates the key by creating a copy and destroying it after it's been showing long enough.
    /// </summary>
    void ArrowKeyAnimator()
    {
        bool[] WasdPressed = { Input.GetKeyDown(KeyCode.W), Input.GetKeyDown(KeyCode.A), Input.GetKeyDown(KeyCode.S), Input.GetKeyDown(KeyCode.D) };
        bool[] ArrowsPressed = { Input.GetKeyDown(KeyCode.UpArrow), Input.GetKeyDown(KeyCode.LeftArrow), Input.GetKeyDown(KeyCode.DownArrow), Input.GetKeyDown(KeyCode.RightArrow) };
        bool KeyPressed = WasdPressed[0] || WasdPressed[1] || WasdPressed[2] || WasdPressed[3] ||
                          ArrowsPressed[0] || ArrowsPressed[1] || ArrowsPressed[2] || ArrowsPressed[3];
        if(KeyPressed)
        {
            StopAllCoroutines();
            StartCoroutine(ArrowKeySpriteSetter(ArrowKeyNumAssigner()));
        }
    }

    IEnumerator ArrowKeySpriteSetter(int key)
    {
        image.sprite = KeyboardPrompts[key];
        yield return new WaitForSeconds(.3f);
        image.sprite = KeyboardPrompts[ArrowKeyNumAssigner()];
    }

    /// <summary>
    /// Animates the sprite using the arrow key sprites and the wasd key sprites.
    /// </summary>
    /// <returns>
    /// A number index in KeyboardPrompts giving the proper sprite.
    /// </returns>
    int ArrowKeyNumAssigner()
    {
        if (Input.GetKeyDown(KeyCode.W))
            return 8;
        else if (Input.GetKeyDown(KeyCode.A))
            return 9;
        else if (Input.GetKeyDown(KeyCode.S))
            return 10;
        else if (Input.GetKeyDown(KeyCode.D))
            return 11;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            return 3;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            return 4;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            return 5;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            return 6;
        else
        {
            if (ArrowsLastPressed)
                return 2;
            else
                return 7;
        }
    }

    /// <summary>
    /// Checks which set of movement keys the player last used; the arrow keys or the wasd keys.
    /// </summary>
    void MovementKeyChecker()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            ArrowsLastPressed = false;
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            ArrowsLastPressed = true;
    }

    /// <summary>
    /// Assigns a button number based on the object's name.
    /// </summary>
    /// <returns>
    /// A button number corresponding to an index in ControllerButtonPrompts.
    /// 0 = B_button
    /// 1 = A_button
    /// 2 = X_button
    /// 3 = Y_button
    /// 4 = dpad
    /// 9 = thumbstick
    /// -1 = name not recognized
    /// </returns>
    int AssignControllerButtonNumber()
    {
        switch(gameObject.name)
        {
            case "B_button":
                return 0;
            case "A_button":
                return 1;
            case "X_button":
                return 2;
            case "Y_button":
                return 3;
            case "dpad":
                return 4;
            case "thumbstick":
                return 9;
        }
        return -1;
    }

    /// <summary>
    /// Determines whether to use dpad sprites or thumbstick sprites based on the assigned number, buttonNum.
    /// </summary>
    /// <param name="buttonNum">
    /// The number that the object was assigned based on the name.
    /// </param>
    void ControllerButtonSetup(int buttonNum)
    {
        if (buttonNum == 4)
            DpadSpriteAnimator();
        else if (buttonNum == 9)
            ThumbstickSpriteAnimator();
        else
            image.sprite = ControllerButtonPrompts[buttonNum];
    }

    /// <summary>
    /// Animates the dpad sprite based on dpad input.
    /// </summary>
    void DpadSpriteAnimator()
    {
        // If a button is pressed,
        if(DpadButtonPressed())
        {
            // Create a new dpad
            GameObject dpad = new GameObject("dpad");
            // Add a recttransform
            RectTransform rect = dpad.AddComponent<RectTransform>();

            // Set the current dpad as the parent
            dpad.transform.SetParent(gameObject.transform);
            // Go to the position of the parent
            rect.anchoredPosition = new Vector3(0, 0, 0);
            // Set the scale to be the same as the parent
            rect.localScale = new Vector3(1, 1, 1);

            // Add an image to the dpad
            Image dpadImage = dpad.AddComponent<Image>();
            // Get the correct sprite based on the user's input
            dpadImage.sprite = ControllerButtonPrompts[DpadInputConverter()];

            // Show the input for .3 seconds
            Destroy(dpad, .3f);
            // Reset the inputs
            joystick.ResetInputs();
        }
    }

    /// <summary>
    /// Convert input from the dpad into an index in ControllerButtonPrompts.
    /// </summary>
    /// <returns>
    /// An index in ControllerButtonPrompts.
    /// 4 = default
    /// 5 = left
    /// 6 = up
    /// 7 = right
    /// 8 = down
    /// </returns>
    int DpadInputConverter()
    {
        if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Left))
            return 5;
        else if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Up))
            return 6;
        else if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Right))
            return 7;
        else if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Down))
            return 8;
        else
            return 4;
    }

    /// <summary>
    /// Checks to see if any of the dpad buttons are being pressed.
    /// </summary>
    /// <returns>
    /// True if a dpad button is being pressed,
    /// false if not.
    /// </returns>
    bool DpadButtonPressed()
    {
        if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Left))
            return true;
        else if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Up))
            return true;
        else if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Right))
            return true;
        else if (joystick.IsButtonPressed(JoystickMenuInput.DpadButtons.Down))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Animates the thumbstick sprite based on thumbstick input.
    /// </summary>
    void ThumbstickSpriteAnimator()
    {
        image.sprite = ControllerButtonPrompts[ThumbstickInputConverter()];
    }

    /// <summary>
    /// Convert input from the thumbstick into an index in ControllerButtonPrompts.
    /// </summary>
    /// <returns>
    /// An index in ControllerButtonPrompts.
    /// 9 = default
    /// 10 = up
    /// 11 = left
    /// 12 = right
    /// 13 = down
    /// </returns>
    int ThumbstickInputConverter()
    {
        if (Input.GetAxis("Vertical") == 1)
            return 10;
        else if (Input.GetAxis("Horizontal") == -1)
            return 11;
        else if (Input.GetAxis("Horizontal") == 1)
            return 12;
        else if (Input.GetAxis("Vertical") == -1)
            return 13;
        else
            return 9;
    }

}
