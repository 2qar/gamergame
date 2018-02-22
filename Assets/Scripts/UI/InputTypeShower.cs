using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTypeShower : MonoBehaviour
{
    // WASD keys 
    public Image[] KeyboardKeys;

    // Controller
    public Image Controller;

    private Color Inactive = new Color(1f, 1f, 1f, .7f);
    private Color Active = new Color(1f, 1f, 1f, 1f);

    private Color ControllerColor;
    private Color KeyboardColor;

    private float startTime;
    private float duration = .3f;

	// Use this for initialization
	void Start ()
    {
        // Set the controller to inactive by default
        ControllerColor = Inactive;
        // Set the keyboard to active by default
        KeyboardColor = Active;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float t = (Time.time - startTime) / duration;
        Debug.Log(KeyboardColor.a);
        /*
        foreach (Image Key in KeyboardKeys)
            Key.color = Mathf.SmoothStep(Key.color.a, KeyboardColor.a, t);
            */
	}

    /*
    public void UseController()
    {
        StopAllCoroutines();
    }

    public void UseKeyboard()
    {
        StopAllCoroutines();
    }
    */

}
