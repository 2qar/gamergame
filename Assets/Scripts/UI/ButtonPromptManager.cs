using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptManager : MonoBehaviour
{
    // The manager for the UI stuffs
    private MainMenuButtons buttons;
    // The other manager for UI stuffs
    public OptionsMenuManager options;

    public bool mainMenuButton = false;

    // The locations next to each UI button to move to
    public int[] buttonPromptPositions = new int[3];

    // Current index in the list of positions for the buttons to be at
    int index;
    int Index
    {
        get { return index; }
        set
        {   if(index != value)
                startTime = Time.time;
            index = value;
        }
    }

    // Start time of movement
    float startTime;

    // Duration of movement
    float duration;

    // The recttransform, used to get the anchored position
    RectTransform pos;

	// Use this for initialization
	void Start ()
    {
        // Try to get the main UI thingy
        try
        {
            buttons = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainMenuButtons>();
        }
        // If that doesn't work,
        catch
        {
            // Get the other UI manager thingy
            try
            {
                options = GameObject.FindGameObjectWithTag("OptionsUI").GetComponent<OptionsMenuManager>();
            }
            // and if that doesnt work,
            catch
            {
                // do nothing hahahaa
            }
        }

        
        // Try to steal the duration from the main UI thingy
        try
        {
            duration = buttons.duration;
        }
        // If that doesn't work,
        catch
        {
            // Try stealing it from the other thing
            try
            {
                duration = options.duration;
            }
            // and if that doesn't work somehow,
            catch
            {
                // do nothing hehehehe
            }
        }

        // get the recttransform to change the position later
        pos = (RectTransform)gameObject.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mainMenuButton)
            Index = buttons.index;
        else
            Index = options.Index;
        float t = (Time.time - startTime) / duration;
        if(pos != null)
        {
            float smoothStuff = Mathf.SmoothStep(pos.anchoredPosition.x, buttonPromptPositions[Index], t);
            pos.anchoredPosition = new Vector2(smoothStuff, pos.anchoredPosition.y);
        }
    }

}
