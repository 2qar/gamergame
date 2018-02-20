using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: The screen shakes more in the shop than it does when it's in the normal play area
// TODO: Write code for ShakeCamera so that the new position isn't a super small number that would result in a boring movement; make a minimum value
// TODO: Add parameters to ShakeCamera so that different objects calling it can input different shake strength and stuff

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
                origin = new Vector3(0, -15, -10);
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
    /// Moves the camera to a random new position inside a circle;
    /// when called multiple times, it can create a shaking effect
    /// </summary>
    public void ShakeCamera()
    {
        // Gets the start time so the camera can start resetting
        startTime = Time.time;
        // Generate a random new position for the camera to move to
        Vector2 newPos = Random.insideUnitCircle;
        newPos.x *= 3;
        newPos.y *= 3;
        // Store this position, but smooothed
        Vector3 smoothNewPos = new Vector3(Mathf.SmoothDamp(transform.position.x, newPos.x, ref currentVelocity, smoothTime),
                                           Mathf.SmoothDamp(transform.position.y, newPos.y, ref currentVelocity, smoothTime), transform.position.z);
        // Set the camera's position to this smoothed position
        transform.position = smoothNewPos;
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
                                  transform.position.z);
        // Slowly reset the camera
        transform.position = pos;
    }

}
