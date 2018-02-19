using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Makes the text on the attached object sway left and right.
/// </summary>
public class TitleTextAnimator : MonoBehaviour
{
    // Check whether to rotate left or not
    bool rotateLeft = false;

    // How long the text should rotate
    private float duration = 3.5f;

    // The time the text starts rotating
    private float startTime;

    private void Start()
    {
        // Set the text to be rotated to the left by default just in case it isn't already set up
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 4);
        // Start the loop
        StartCoroutine(rotateText());
    }

    private void FixedUpdate()
    {
        float t = (Time.time - startTime) / duration;
        float smoothStuff;
        if (rotateLeft)
            smoothStuff = Mathf.SmoothStep(-4, 4, t);
        else
            smoothStuff = Mathf.SmoothStep(4, -4, t);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, smoothStuff);
    }

    /// <summary>
    /// Set up the start time and rotateleft variable.
    /// Calls itself at the end so this goes on forever.
    /// </summary>
    IEnumerator rotateText()
    {
        startTime = Time.time;
        yield return new WaitForSeconds(duration);
        rotateLeft = !rotateLeft;
        StartCoroutine(rotateText());
        yield break;
    }

}
