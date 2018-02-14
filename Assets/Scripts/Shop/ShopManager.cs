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
}
