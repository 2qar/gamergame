using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollider : MonoBehaviour 
{
	// Timer vars
	float explodeTime = .5f;

	// Collision vars
	bool isTouchingPlayer = false;
	bool lastTouchedPlayer = false;

	// Player Object
	GameObject player;

	// Use this for initialization
	void Start () 
	{
		// Get player object
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// If the minecollider collides with the player, start timer
		if (other.gameObject.tag == "Player") {
			isTouchingPlayer = true;
			lastTouchedPlayer = true;
			StartCoroutine ("MyCoroutine");
		} 
		// If the object collided with isn't the player,
		// set var to indicate that
		else 
		{
			StopCoroutine (MyCoroutine ());
			lastTouchedPlayer = false;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		// If a collision box is exited and
		// the last object touched was the player,
		// the minecollider isn't touching the player
		if (lastTouchedPlayer) 
		{
			StopCoroutine (MyCoroutine());
			lastTouchedPlayer = false;
			isTouchingPlayer = false;
		}
	}

	private IEnumerator MyCoroutine()
	{
		while(isTouchingPlayer)
		{
			// Wait 5 seconds, then destroy player
			yield return new WaitForSeconds(explodeTime);
			// Check again for collision with player
			// so they don't blow up when they're
			// outside of the mine's range
			if (!isTouchingPlayer) yield break;
			// Blow em up
			Destroy (transform.parent.gameObject);
			Destroy(gameObject);
			Destroy(player);
		}
	}
}