using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make the particle system start once the entrance is full size
// FIXME: In the built version of the game, the entrance square renders behind all of the particle


/// <summary>
/// Moves the player and camera down to the little shop area when the player
/// shoots the entrance and also stops enemies from spawning and sets up the shop.
/// </summary>
public class ShopEntrance : MonoBehaviour
{
    // The shop manager that will be enabled on entrance
    private GameObject shopManager;

    // Rotation speed
    public float rotateFactor;

    // Rotation speed in a nicer to use format
    private Vector3 rotation;

    // The scene's camera
    public Camera mainCam;
    // The player
    public GameObject player;

    // Get the GameController
    private GameObject gameController;
    private GameController controller;

    // Store the amount of times the object has been hit by a bullet
    bool shot = false;

    // The collider for the entrance
    private BoxCollider2D collider;

    // The time when the object was created
    float startTime;

    // How long should it take for the entrance to increase to full size?
    float duration = 2.0f;

    // Use this for initialization
    void Start () 
    {
        // Get the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the main camera in the scene
        mainCam = Camera.main;

        // Initialize nicer version of rotation speed to use
        rotation = new Vector3(0, 0, rotateFactor);

        // Get the GameController object
        gameController = GameObject.FindGameObjectWithTag("GameController");
        // Get the script off the gamecontroller object
        controller = gameController.GetComponent<GameController>();

        // Find the shopmanager
        shopManager = GameObject.FindGameObjectWithTag("ShopManager");

        // Get the collider
        collider = gameObject.GetComponent<BoxCollider2D>();

        // Set the start time
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Rotate the ship portal thingy by <rotateFactor>
        transform.Rotate(rotation);
	}

    private void FixedUpdate()
    {
        // fancy math stuff to make it take duration seconds increase in size
        float time = (Time.time - startTime) / duration;
        // the current size of the cube to use
        float size = Mathf.SmoothStep(0, 1, time);
        // Update the size of the cube
        transform.localScale = new Vector3(size, size, 1);
        // If it's full size,
        if (transform.localScale.x >= 1)
            // Enable the collider
            collider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerBullet" && !shot)
        {
            shot = true;
            teleportToShop();
            // Destroy the player's bullet
            Destroy(other.gameObject);
            // TODO: Maybe suck in particles and wait before teleporting the player to shop???
            // -When the player shoots the moving shop entrance object, hide the spriterenderer and
            // enable a wind component, sucking the player in
            // Get rid of the shop portal
            // -Enable the shopmanager particle system so it starts spitting stuff out
            //Destroy the parent object
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Teleport the player and the camera to the shop,
    /// stop spawning enemies into the level, 
    /// and set up the items in the shop.
    /// </summary>
    void teleportToShop()
    {
        // Get all of the enemies in the area
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Run through and kill all of them
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
        
        // Stop the enemy spawning
        controller.StopAllCoroutines();
        // Start a wait that runs until the player leaves the shop
        controller.StartCoroutine(controller.shopWait());

        // Change the camera's position to the shop position
        mainCam.transform.position = new Vector3(0, -100, -10);
        // Change the player's position to the shop position minus an offset
        player.transform.position = new Vector3(-6.05f, -100, transform.position.z);

        // Set up the shop
        shopManager.SendMessage("setUpShop");
    }

}
