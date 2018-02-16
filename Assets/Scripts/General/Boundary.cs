using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deletes any object that leave the collider this script is attached to.
/// Nice for making computers not slowly melt as more and more objects are created.
/// </summary>
public class Boundary : MonoBehaviour 
{
    // When an object leaves the boundary area, 
	void OnTriggerExit2D (Collider2D other)
	{
        // If the object is not the player,
        if(other.gameObject.tag != "Player")
        {
            // If the object has a parent object,
            if (other.transform.parent != null)
                // Destroy it too
                Destroy(other.transform.parent.gameObject);
            
            // Destroy the object that left the boundary
            Destroy(other.gameObject);
        }
    }
}
