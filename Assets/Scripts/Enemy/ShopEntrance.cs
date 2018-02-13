using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntrance : MonoBehaviour
{
    // Rotation speed
    public float rotateFactor;

    // Rotation speed in a nicer to use format
    private Vector3 rotation;

    // The scene's camera
    public Camera mainCam;
    // The player
    public GameObject player;

	// Use this for initialization
	void Start () 
    {
        // Initialize nicer version of rotation speed to use
        rotation = new Vector3(0, 0, rotateFactor);
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Rotate the ship portal thingy by <rotateFactor>
        transform.Rotate(rotation);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            // -Teleport the player to the shop,
            // maybe play an animation or something before sending the player to the shop
            // -To teleport to shop, set player position and camera position
            // to the position of a shop set up somewhere in the level
            // -Every time the player enters the shop,
            // generate a random set of items for them to pick from
            //Debug.Log("teleport to shop");
            teleportToShop();
            // Destroy the player's bullet
            Destroy(other.gameObject);
            // TODO: Maybe suck in particles and wait before teleporting the player to shop???
            // -When the player shoots the moving shop entrance object, hide the spriterenderer and
            // enable a wind component, sucking the player in
            // Get rid of the shop portal
            //Destroy the parent object
            Destroy(gameObject);
        }
    }

    void teleportToShop()
    {
        // Get all of the enemies in the area and kill them

        // Change the camera's position to the shop position
        mainCam.transform.position = new Vector3(0, -15, -10);
        // Change the player's position to the shop position minus an offset
        player.transform.position = new Vector3(-6.05f, -15, transform.position.z);
        // Generate a random pool of items for the player to pick from

        // TODO: Make a shop to teleport to :^)
    }

}
