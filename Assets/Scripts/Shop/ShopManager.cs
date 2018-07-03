using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: Fix bug where the shoptext breaks by just putting it on another canvas and disabling the canvas instead of the gameobject
// FIXME: I think the bug where stuff spawns twice when the player leaves the shop is still there

/// <summary>
/// Handles:
/// - Global item price
/// - Spawning a random set of items when the player enters the shop
/// - Moving the player and the camera back to the normal position when the player goes to the exit
/// </summary>
public class ShopManager : MonoBehaviour 
{
    // The shop exit collider
    private Collider2D exitCollider;

    // The screen shake script on the camera
    private ScreenShaker shaker;

    // The price of the items, updates when player buys stuff
    public int price = 50;

    // UI text stuff that needs to be hidden
    public GameObject[] textElements = new GameObject[2];

    // Randomly pick from this list when the player enters the shop
    //public GameObject[] items = new GameObject[5];
    public GameObject item;

    // Store each of the items generated
    private GameObject[] items = new GameObject[6];

    // *Stuff that's gonna get moved back*
    // The player 
    private GameObject player;
    // The main camera in the scene
    private Camera mainCam;

    // The gamecontroller
    private GameController controller;

    // Stores whether the player is in the shop or not
    private bool inShop;

    private void Start()
    {
        // Get the exit collider
        exitCollider = gameObject.GetComponent<BoxCollider2D>();

        // Get the screen shake script
        shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShaker>();

        // Get the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the main camera in the scene
        mainCam = Camera.main;

        // Get the gamecontroller to reset the waves
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// Generate the items in the shop
    /// </summary>
    void generateItems()
    {
        // Goes to each item slot position and creates a random powerup for the player to buy
        for (int pos = -2; pos <= 2; pos+= 2)
        {
            // Create the randomly selected powerup at the current position and store it so it can be deleted later
            items[pos + 2] = (GameObject)Instantiate(item, new Vector3(6, pos - 100, 0), transform.rotation);
        }
    }

    /// <summary>
    /// Show the shop text and hide the level text, just in case it's active.
    /// </summary>
    void textSetup()
    {
        // Show the ShopText
        textElements[0].SetActive(true);
        // Hide the level text just incase it still exists
        textElements[1].SetActive(false);
    }

    /// <summary>
    /// Set up all of the stuff in the shop.
    /// </summary>
    void setUpShop()
    {
        // Let the gamecontroller know that the player is in the shop
        controller.IsPlayerInShop = true;
        // Update inShop so that the exit can check for collisions with the player
        inShop = true;
        // Let the script know that the player is in the shop now
        shaker.InShop = true;
        // Generate the items for the player to buy
        generateItems();
        // Set up all the text
        textSetup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the exit detects a collision from the player and the player is in the shop,
        if (collision.gameObject.name == "Player" && inShop)
        {
            // Leave the shop
            exitShop();
        }
    }

    /// <summary>
    /// Move the player and camera back into the play area,
    /// start spawning enemies again,
    /// and show the level text.
    /// </summary>
    void exitShop()
    {
        // Let the gamecontroller know that the player isn't in the shop anymore
        controller.IsPlayerInShop = false;
        // Update the var so the player isn't in the shop
        inShop = false;
        // Disable the collider so the player can't somehow trigger this method again
        exitCollider.enabled = false;

        // Let the script know that the player left the shop
        shaker.InShop = false;
        // Move the player back to their original position
        player.transform.position = new Vector2(-6.05f, 0f);
        // Move the camera back to its original position
        mainCam.transform.position = new Vector3(0, 0, -10);

        // Delete the powerups that weren't purchased
        foreach (GameObject powerup in items)
            Destroy(powerup);

        // Hide the shop text
        textElements[0].SetActive(false);
        // Stop waiting
        controller.StopCoroutine("shopWait");

        // Disable wait variable
        controller.waitBeforeWave = false;
        // Start spawning enemies again
        //StartCoroutine(controller.spawnEnemies());

        // Enable the collider again for when the player comes back to the shop
        exitCollider.enabled = true;
    }

}
