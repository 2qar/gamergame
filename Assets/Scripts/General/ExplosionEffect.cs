using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes a lil explosion effect.
/// </summary>
public class ExplosionEffect : MonoBehaviour
{
    SpriteRenderer sr;
	// Use this for initialization
	void Start ()
    {
        // Get the SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
        // Play the effect
        StartCoroutine(effect(sr));

        // Move the explosion above the rest of the objects in the scene so it renders in front
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
	}

	/// <summary>
    /// Changes the color from white quickly to black, giving an explosion effect
    /// </summary>
    /// <param name="sr">The spriterenderer to apply the effect to.</param>
    IEnumerator effect(SpriteRenderer sr)
    {
        // Wait for half a second
        yield return new WaitForSeconds(0.02f);
        // Change the color to black
        sr.color = new Color(0, 0, 0, 255);
        // Wait for a fifth of a second
        yield return new WaitForSeconds(0.02f);
        // Disappear
        Destroy(gameObject);
    }
}
