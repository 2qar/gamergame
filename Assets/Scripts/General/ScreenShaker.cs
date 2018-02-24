using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: Fix the bug where the particle systems for the stars suck and disappear if the camera moves too far away from them or something

public class ScreenShaker : MonoBehaviour
{
    float smoothTime = .3f;
    float currentVelocity = 0f;

    float startTime;
    float duration = .5f;

    // Stores whether the player is inside the shop or not;
    // needed to adjust the origin point to go back to in ResetCameraPosition
    private bool inShop = false;
    public bool InShop
    {
        get { return inShop; }
        set
        {
            if(value)
                origin = new Vector3(0, -100, -10);
            else
                origin = new Vector3(0, 0, -10);
            inShop = value;
        }
    }

    // Origin point for the camera to return too after shaking;
    // changes based on whether the player is in the shop or not.
    private Vector3 origin = new Vector3(0, 0, -10);

	// Update is called once per frame
	void Update ()
    {
        ResetCameraPosition();
	}

    /// <summary>
    /// Shakes the camera inside of a unit circle with a radius of strength.
    /// </summary>
    /// <param name="strength">
    /// The radius of the unit circle to generate a point inside, effectively strength.
    /// </param>
    public void ShakeCamera(float strength)
    {
        // Gets the start time so the camera can start resetting
        startTime = Time.time;
        // Generate a random new position for the camera to move to
        Vector3 newPos = Random.insideUnitCircle * strength;
        // Update the new position's z to -10 so the camera doesn't move forward and make everything invisible
        newPos.z = -10;
        // Set the camera's position to this smoothed position
        transform.position += newPos;
    }

    /// <summary>
    /// Constantly moves the camera back to the origin.
    /// </summary>
    private void ResetCameraPosition()
    {
        // Time stuff
        float t = (Time.time - startTime) / duration;
        // Smooth movement between 0,0 and the current position the camera is in
        Vector3 pos = new Vector3(Mathf.SmoothStep(transform.position.x, origin.x, t), 
                                  Mathf.SmoothStep(transform.position.y, origin.y, t), 
                                  -10);
        // Slowly reset the camera
        transform.position = pos;
    }

}
