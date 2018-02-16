using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Maybe find a way around using this dumb tiny script??? Or just remove this comment if you're too lazy to do that
// Maybe a solution to this involves rotating the spriterenderer thingy instead of the transform

/// <summary>
/// Handles moving an object containing a particle system that shouldn't be rotated when
/// there is rotation happening.
/// </summary>
public class EnemyParentMover : MonoBehaviour
{
    // The speed to move at 
    public int speed = 5;
	
	void FixedUpdate ()
    {
        // Move the transform left at a rate of speed
        transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
    }

}