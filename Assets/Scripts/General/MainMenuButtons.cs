using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // The UI buttons on the main menu
    public Text[] buttons = new Text[3];
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
        changeSelectedElement();
        pressButton();
	}

    void changeSelectedElement()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (index == 0)
                Index = 2;
            else
                Index--;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (index == 2)
                Index = 0;
            else
                Index++;
    }

    void pressButton()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            if (index == 0)
                // Load the game
                SceneManager.LoadScene(1);
            else if (index == 1)
                // Open the options
                Debug.Log("Options");
            else if (index == 2)
                // Quit the game somehow
                //Application.Quit;
                Debug.Log("Quit");
    }

}