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
                // If the player isn't already at full health,
                if(controller.playerMan.Health != controller.playerMan.MaxHealth)
                    // Heal them
                    controller.playerMan.Health++;
                break;
            // Max Health Increase Pickup
            case 2:
                controller.playerMan.MaxHealth++;
                break;
            // Player Damage Increase Pickup
            case 3:
                controller.playerMan.playerDamage++;
                break;
            // Max Speed Increase Pickup
            case 4:
                controller.playerMan.MaxSpeed += 2.5f;
                controller.playerMan.PoweredUp = true;
                break;
            // Bullet Size Increase Pickup
            case 5:
                // Increase the size of the player's bullet if their bullet isn't too big
                if(playerBullet.transform.localScale.y < 1.3)
                    playerBullet.transform.localScale += new Vector3(.2f, .2f, 0);
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
        //Color itemColor = new Color();
        switch (itemNum)
        {
            // Health powerup
            case 1:
                return Color.red;
            // Max health powerup
            case 2:
                return Color.magenta;
            // Damage powerup
            case 3:
                return Color.green;
            // Max speed increase powerup
            case 4:
                return Color.blue;
            // Bullet size increase powerup
            case 5:
                return Color.yellow;
        }
        // if itemNum isn't 1-5 for some reason, make the item non-existant
        return new Color();
    }

    /// <summary>
    /// Set up the powerup by assigning what powerup it will be and giving it the appropriate color.
    /// </summary>
    void createPowerup(string name)
    {
        // Assign what powerup it will be
        assignNum(name);
        // Pick the color of the powerup
        sr.color = colorPicker(itemNum);
    }

}