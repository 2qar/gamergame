using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextAnimator : MonoBehaviour
{
    // Rotation factor
    float rotation = 0f;
    // Check whether to rotate left or not
    bool rotateLeft = true;

	// Update is called once per frame
	void Update ()
    {
        // Update the rotation variable
        rotateText();
        // Apply rotation to the text
        transform.rotation = new Quaternion(transform.rotation.x, 0, rotation, 360);
	}

    // Handles increasing and decreasing the rotation variable
    void rotateText()
    {
        // If the text is supposed to rotate left and hasn't made it far enough yet,
        if (rotateLeft && rotation < 25)
            // Keep rotating
            rotation += .1f;
        // If it has made it far enough left,
        else
            // No more rotating left
            rotateLeft = false;
        // If the text is supposed to rotate right and it hasn't made it far enough yet,
        if (!rotateLeft && rotation > -15)
            // Rotate right
            rotation -= .1f;
        // If it has made it,
        else
            // Start rotating left
            rotateLeft = true;
    }

}
