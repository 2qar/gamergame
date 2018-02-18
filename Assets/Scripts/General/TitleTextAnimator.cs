using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Use Mathf.Smoothstep to make this a lil bit smoother.

/// <summary>
/// Makes the text on the attached object sway left and right.
/// </summary>
public class TitleTextAnimator : MonoBehaviour
{
    // Actual rotation factor
    public float rotationFactor = .1f;
    // Rotation factor
    float rotation = 0f;
    // Check whether to rotate left or not
    bool rotateLeft = true;

	// Update is called once per frame
	void Update ()
    {
        // Update the rotation variable
        rotateText();
	}

    private void FixedUpdate()
    {
        // Rotate the gameobject by the rotation factor.
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, rotation / 3));
    }

    // Handles increasing and decreasing the rotation variable
    void rotateText()
    {
        // If the text is supposed to rotate left and hasn't made it far enough yet,
        if (rotateLeft && rotation < 25)
            // Keep rotating
            rotation += rotationFactor;
        // If it has made it far enough left,
        else
            // No more rotating left
            rotateLeft = false;
        // If the text is supposed to rotate right and it hasn't made it far enough yet,
        if (!rotateLeft && rotation > -25)
            // Rotate right
            rotation -= rotationFactor;
        // If it has made it,
        else
            // Start rotating left
            rotateLeft = true;
    }

}
