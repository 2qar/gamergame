using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Make sprites for the items

// Note to self: Next time, make a single object that gives itself a color and name
// instead of having a bunch of different prefabs for each of the different powerups

/// <summary>
/// Handles the individual items, their effects, and the player purchasing them.
/// </summary>
public class ShopItemManager : MonoBehaviour 
{
    // The price of the item
    private int price;

    // The item itself
    private int itemNum;

    // The shop management object
    private ShopManager manager;

    // The game controller script
    private GameController controller;

    // The object that the player copies when they fire a bullet
    public GameObject playerBullet;

    // The item description thingy
    private Text itemText;

    // Used to change the color
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () 
    {
        // Get the shopmanager script off of the object
        manager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        // Find the gamecontroller object and get its GameController script
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // Get the spriterenderer
        sr = gameObject.GetComponent<SpriteRenderer>();

        // Get the text that's attached to the item
        itemText = gameObject.GetComponentInChildren<Text>();

        // Set up the powerup
        createPowerup(gameObject.name);
	}

    private void FixedUpdate()
    {
        // Keep the price up to date with the shopmanager's price
        price = manager.price;
        // Update the text
        itemText.text = initializeText();
    }

    /// <summary>
    /// Assign the item a number for use later when giving the player their item.
    /// </summary>
    /// <param name="name">Name of the item.</param>
    void assignNum(string name)
    {
        itemNum = (int)Random.Range(1, 6);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            // Destroy the bullet
            Destroy(other.gameObject);
            // If the player has enough cash money,
            if (controller.money >= price)
            {
                // Give them the item
                giveItem();
                // Take money from the player 
                controller.money -= price;
                // Destroy the object
                Destroy(gameObject);
                // Increase the price of all the items at the shop
                manager.price *= 2;
            }
            else
                // Tell the player that they don't have enough money
                Debug.Log("Not enough money: " + controller.money + " (" + price + " needed)");
        }
    }

    /// <summary>
    /// Apply the effects of the item based on the number that was assigned earlier
    /// </summary>
    void giveItem()
    {
        switch(itemNum)
        {
            // Health Pickup
            case 1:
                if(controller.playerMan.Health != controller.playerMan.MaxHealth)
                    controller.playerMan.Health++;
                Debug.Log("heal player");
                break;
            // Max Health Increase Pickup
            case 2:
                controller.playerMan.MaxHealth++;
                Debug.Log("increase max player health");
                break;
            // Player Damage Increase Pickup
            case 3:
                Debug.Log("increase player damage");
                break;
            // Max Speed Increase Pickup
            case 4:
                controller.playerMan.MaxSpeed += 5f;
                controller.playerMan.PoweredUp = true;
                break;
            // Bullet Size Increase Pickup
            case 5:
                // Increase the size of the player's bullet
                playerBullet.transform.localScale += new Vector3(1f, 1f, 0);
                Debug.Log("increase player bullet size or something");
                break;
        }
    }
    
    /// <summary>
    /// Set up the text for the powerup.
    /// </summary>
    /// <param name="itemNum">The assigned item number.</param>
    string initializeText()
    {
        string text = "";
        switch(itemNum)
        {
            case 1:
                text = "Health: ";
                break;
            case 2:
                text = "Max Health: ";
                break;
            case 3:
                text = "Damage: ";
                break;
            case 4:
                text = "Speed: ";
                break;
            case 5:
                text = "Bullet: ";
                break;
        }
        text += price;
        return text;
    }
    
    /// <summary>
    /// Pick the color for the item.
    /// </summary>
    /// <param name="itemNum">The assigned item number.</param>
    Color colorPicker(int itemNum)
    {
        Color itemColor = new Color();
        switch (itemNum)
        {
            case 1:
                itemColor = new Color(246, 5, 5, 255);
                break;
            case 2:
                itemColor = new Color(255, 0, 90, 255);
                break;
            case 3:
                itemColor = new Color(124, 253, 0, 255);
                break;
            case 4:
                itemColor = new Color(0, 71, 253, 255);
                break;
            case 5:
                itemColor = new Color(251, 255, 0, 255);
                break;
        }
        return itemColor;
    }

    /// <summary>
    /// Set up the powerup.
    /// </summary>
    void createPowerup(string name)
    {
        // Assign what powerup it will be
        assignNum(name);
        Debug.Log(itemNum);
        // Pick the color of the powerup
        Debug.Log(colorPicker(itemNum));
        sr.color = colorPicker(itemNum);
    }

}