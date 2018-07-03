using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Make the item re-roll if it's an item the player can't benefit from right now; health powerup at full health, bullet size powerup at max bullet size

/// <summary>
/// Handles the individual items, their effects, and the player purchasing them.
/// </summary>
public class ShopItemManager : MonoBehaviour 
{
    // Sprites for the shop items
    public Sprite[] items = new Sprite[7];

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

    // Get a reference to the score text manager; gonna make it flash red when the player tries to buy something they can't afford
    private ScoreTextManager scoreTextMan;

	// Use this for initialization
	void Start () 
    {
        // Get the shopmanager script off of the object
        manager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        // Find the gamecontroller object and get its GameController script
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // Get the spriterenderer
        sr = gameObject.GetComponent<SpriteRenderer>();

        // Get the scoretextmanager
        scoreTextMan = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<ScoreTextManager>();

        // Get the text that's attached to the item
        itemText = gameObject.GetComponentInChildren<Text>();

        // Set up the powerup
        createPowerup(gameObject.name);
	}

    /// <summary>
    /// Assign the item a number for use later when giving the player their item.
    /// </summary>
    /// <param name="name">Name of the item.</param>
    void assignNum(string name)
    {
        itemNum = (int)Random.Range(1, 6);
    }

    void assignPrice()
    {
        switch (itemNum)
        {
            // Health item
            case 1:
                price = 50;
                break;
            // Max health item
            case 2:
                price = 50;
                break;
            // Health item
            case 3:
                price = 50;
                break;
            // Speed item
            case 4:
                price = 75;
                break;
            // Bullet size item
            case 5:
                price = 75;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            // Destroy the bullet
            Destroy(other.gameObject);
            // If the player has enough cash money,
            if (controller.Money >= price)
            {
                // Give them the item
                giveItem();
                // Take money from the player 
                controller.Money -= price;
                // Destroy the object
                Destroy(gameObject);
                // Increase the price of all the items at the shop
                manager.price *= 2;
            }
            else
                // Tell the player that they don't have enough money by flashing their money red
                scoreTextMan.PrettyTextEffect(.1f, Color.red);
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
                givePlayerHealth();
                break;
            // Health pickup
            case 2:
                givePlayerHealth();
                break;
            // Health Pickup
            case 3:
                givePlayerHealth();
                break;
            // Max Speed Increase Pickup
            case 4:
                controller.playerMan.MaxSpeed += 1f;
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

    void givePlayerHealth()
    {
        // If the player isn't already at full health,
        if (controller.playerMan.Health != controller.playerMan.MaxHealth)
            // Heal them
            controller.playerMan.Health++;
        else
            // Add to the player's max health
            controller.playerMan.MaxHealth++;
    }

    /// <summary>
    /// Picks a sprite or sprites for the item to be based on the item's assigned number.
    /// </summary>
    /// <param name="item">
    /// The item's assigned number based on it's name
    /// </param>
    /// <returns>
    /// Sprite(s) picked for the item.
    /// </returns>
    Sprite[] spritePicker(int item)
    {
        switch(item)
        {
            // Health sprites
            case 1:
                return new Sprite[] { items[5], items[6]};
            // health sprite
            case 2:
                return new Sprite[] { items[5], items[6] };
            // Health sprites
            case 3:
                return new Sprite[] { items[5], items[6] };
            // Speed sprite
            case 4:
                return new Sprite[] { items[3] };
            // Bullet size sprites
            case 5:
                return new Sprite[] { items[0], items[1] };
        }
        // if the assigned item number somehow isn't in that switch, hand back the sprite the item has right now
        return new Sprite[] { sr.sprite };
    }

    /// <summary>
    /// Animates a sprite if given more than one sprite, or gives the item its sprite if given one.
    /// </summary>
    /// <param name="sprites">
    /// Sprites to animate the item with or set the item to.
    /// </param>
    IEnumerator animateSprite(Sprite[] sprites)
    {
        // If sprites holds more than one sprite,
        if(sprites.Length > 1)
        {
            sr.sprite = sprites[0];
            yield return new WaitForSeconds(.5f);
            sr.sprite = sprites[1];
            yield return new WaitForSeconds(.5f);
            StartCoroutine(animateSprite(sprites));
            yield break;
        }
        // If sprites only contains one sprite,
        sr.sprite = sprites[0];
        yield break;
    }

    /// <summary>
    /// Set up the powerup by assigning what powerup it will be and giving it the appropriate color.
    /// </summary>
    void createPowerup(string name)
    {
        // Assign what powerup it will be
        assignNum(name);
        // Set the price of the item
        assignPrice();
        // Update the text to show this price
        itemText.text = price.ToString();
        // Pick the sprite
        StartCoroutine(animateSprite(spritePicker(itemNum)));
    }

}