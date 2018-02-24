using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour
{
    // Time that the animation of moving back to 1 scale starts
    float startTime;
    // How long this animation should last
    float duration = 1f;

    // Maximum size that the text can be.
    float maxSize = 1.2f;

    // The text component of the score text
    Text scoreText;

    // Original green of the text
    Color originalColor = new Color(0, 1, 0);
    // Green to shift to when the player gets money
    public Color newColor = new Color(.2f, 1, .23f);

    // Get a reference to the gamecontroller to update the text based on the player's score
    private GameController controller;

	// Use this for initialization
	void Start ()
    {
        // Get the score text
        scoreText = gameObject.GetComponent<Text>();

        // Initialize starttime
        startTime = Time.time;

        // Get the gamecontroller
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Update the score text to be the player's money
        scoreText.text = controller.Money.ToString();
        ResetTextSize();
        ResetTextColor();
	}

    /// <summary>
    /// Slowly changes the scale of the text back to 1.
    /// </summary>
    void ResetTextSize()
    {
        float t = (Time.time - startTime) / duration;
        Vector3 smoothScale = new Vector3(Mathf.SmoothStep(transform.localScale.x, 1, t),
                                        Mathf.SmoothStep(transform.localScale.y, 1, t), transform.localScale.z);
        transform.localScale = smoothScale;
    }

    /// <summary>
    /// Increases the text size by fac and flash the text a different shade of green.
    /// </summary>
    /// <param name="fac">
    /// Amount to increase the text size by.
    /// </param>
    void IncreaseTextSize(float fac)
    {
        if(transform.localScale.x <= maxSize)
            transform.localScale += new Vector3(fac, fac, 0);
    }

    /// <summary>
    /// Slowly shifts the text's color back to the original color.
    /// </summary>
    void ResetTextColor()
    {
        float t = (Time.time - startTime) / duration;
        Color smoothColor = new Color(Mathf.SmoothStep(scoreText.color.r, originalColor.r, t),
                                      Mathf.SmoothStep(scoreText.color.g, originalColor.g, t),
                                      Mathf.SmoothStep(scoreText.color.b, originalColor.b, t));
        scoreText.color = smoothColor;
    }

    /// <summary>
    /// Changes the color of the text to a lighter green.
    /// </summary>
    public void ChangeTextColor(Color col)
    {
        scoreText.color = col;
    }

    /// <summary>
    /// Changes the color of the text and increases its size by fac.
    /// </summary>
    /// <param name="fac">
    /// Amount to increase the text size by.
    /// </param>
    public void PrettyTextEffect(float fac, Color col)
    {
        // update the start time
        startTime = Time.time;
        IncreaseTextSize(fac);
        ChangeTextColor(col);
    }

}
