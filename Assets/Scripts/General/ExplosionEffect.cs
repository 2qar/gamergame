using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	// Explosion effect
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
