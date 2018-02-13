using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntrance : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		// Rotate the cube thingy
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            // -Teleport the player to the shop,
            // maybe play an animation or something before sending the player to the shop
            // -To teleport to shop, set player position and camera position
            // to the position of a shop set up somewhere in the level
            // -Every time the player enters the shop,
            // generate a random set of items for them to pick from
            Debug.Log("teleport to shop");
            // Get rid of the shop portal
            Destroy(gameObject);
        }
    }

    void teleportToShop()
    {
        // Change the player's position to the shop position minus an offset
        // Change the camera's position to the shop position
        // Generate a random pool of items for the player to pick from
    }

}
