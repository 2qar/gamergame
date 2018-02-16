using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour 
{
    // The price of the items, updates when player buys stuff
    public int price = 50;

    // UI text stuff that needs to be hidden
    public GameObject[] textElements = new GameObject[2];

    // Randomly pick from this list when the player enters the shop
    public GameObject[] items = new GameObject[5];

    // *Stuff that's gonna get moved back*
    // The player 
    private GameObject player;
    // The main camera in the scene
    private Camera mainCam;

    // The gamecontroller
    private GameController controller;

    private void Start()
    {
        // Get the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the main camera in the scene
        mainCam = Camera.main;

        // Get the gamecontroller to reset the waves
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void generateItems()
    {
        // Goes to each item slot position and creates a random powerup for the player to buy
        for (int pos = -2; pos <= 2; pos+= 2)
        {
            // Get a random number that will represent an item from the list
            int randomNum = Random.Range(0, items.Length);
            // Create the randomly selected powerup at the current position
            Instantiate(items[randomNum], new Vector3(6, pos - 15, 0), transform.rotation);
        }
    }

    // Set the text to the opposite state it's currently in
    void textSetup()
    {
        // Show the ShopText
        textElements[0].SetActive(true);
        // Hide the level text just incase it still exists
        textElements[1].SetActive(false);
    }

    // Set up all of the stuff in the shop
    void setUpShop()
    {
        // Generate the items for the player to buy
        generateItems();
        // Set up all the text
        textSetup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the little exit thingy,
        if (collision.gameObject.name == "Player")
        {
            // Leave the shop
            exitShop();
        }
    }

    /// <summary>
    /// Move the player back into the play area w/ the camera and
    /// set up the level text and start spawning enemies again
    /// </summary>
    void exitShop()
    {
        // Move the player back to their original position
        player.transform.position = new Vector2(-6.05f, 0f);
        // Move the camera back to its original position
        mainCam.transform.position = new Vector3(0, 0, -10);
        // Hide the shop text
        textElements[0].SetActive(false);
        // Stop waiting
        controller.StopCoroutine("shopWait");
        // Disable wait variable
        //controller.waitBeforeWave = false;
        // Start spawning enemies again
        StartCoroutine(controller.spawnEnemies());
    }

}
