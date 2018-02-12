using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int Index
    {
        get { return index; }
        set
        {
            index = value;
            RectTransform selectorPos = (RectTransform)selector.transform;
            RectTransform buttonPos = (RectTransform)UIElements[index].transform;
            selectorPos.anchoredPosition = new Vector2(0, buttonPos.anchoredPosition.y - 50);
        }
    }

    // List of supported resolutions 
    private int[][] resolutions =
    {
        new int[] {640, 360},
        new int[] {1024, 576},
        new int[] {1280, 720},
        new int[] {1920, 1080}
    };
    // Index for the resolution list
    int resIndex = 0;
    int ResIndex
    {
        get { return resIndex; }
        set
        {
            resIndex = value;
            // Update the text element
            UIElements[0].text = "< " + resolutions[resIndex][0] + "x" + resolutions[resIndex][1] + " >";
        }
    }
    // False by default
    bool fullscreen = false;
    // Keep track of what value the player wants it at
    int fullscreenIndex = 0;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        selectorPositionUpdate();
        resolutionSelector();
        getButtonInput();
        fullscreenSelector();
	}

    void selectorPositionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (index == 0)
                Index = 2;
            else
                Index--;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (index == 2)
                Index = 0;
            else
                Index++;
    }

    void getButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
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
    }

    void resolutionSelector()
    {
        if (index == 0)
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                if (resIndex == 0)
                    ResIndex = 3;
                else
                    ResIndex--;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                if (resIndex == 3)
                    ResIndex = 0;
                else
                    ResIndex++;
    }

    void fullscreenSelector()
    {
        if (index == 1)
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                if (fullscreenIndex == 0)
                    fullscreenIndex = 1;
                else
                    fullscreenIndex--;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
