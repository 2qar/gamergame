using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    // The amount of health the boss has
    private int health = 100;
    // The health value for everything else to use
    public int Health
    {
        get { return health; }
        set
        {
            // Play te damage effect that makes the boss flash white and play a lil damage noise
            StartCoroutine(damageEffect());
            // Update the health
            health = value;
        }
    }

    // <BOSS COLLIDERS>
    // The normal damage colliders
    BoxCollider2D[] wings;
    // The boss' weak point
    BoxCollider2D critPoint;

    // <SPRITERENDERERS>
    SpriteRenderer mainBody;
    SpriteRenderer weakSpot;

    // The player's management script; stores the player's damage
    private PlayerManager playerMan;
    // The player
    private GameObject player;

    // normal movement behavior stuff
    private bool normalBehavior = false;
    private bool goUp;
    float boundary = 3.5f;
    float rate = .05f;

    // Use this for initialization
    void Start ()
    {
        // Get the colliders on the wings of the boss
        wings = gameObject.GetComponents<BoxCollider2D>();
        // Get the main collider where the boss takes more damage if hit
        critPoint = gameObject.GetComponentInChildren<BoxCollider2D>();

        // Get the spriterenderers for...
        // This object
        mainBody = gameObject.GetComponent<SpriteRenderer>();
        // The weak point 
        weakSpot = gameObject.GetComponentInChildren<SpriteRenderer>();

        // Get the player
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the player manager
        playerMan = player.GetComponent<PlayerManager>();

        // Start normal behavior
        //StartCoroutine(normalBehavior());
        normalBehavior = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(health);
	}

    private void FixedUpdate()
    {
        if(normalBehavior)
        {
            float smoothStuff = Mathf.SmoothStep(gameObject.transform.position.y, boundary, rate);
            float currentPos = gameObject.transform.position.y;
            /*
            if (gameObject.transform.position.y < boundary - .5)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, smoothStuff, gameObject.transform.position.z);
            else
            {
                boundary *= -1;
            }
            */
            // Set which way to go
            if (currentPos < 3)
                goUp = true;
            else if (currentPos > -3)
                goUp = false;

            if (goUp)
                boundary = 3.5f;
            else
                boundary = -3.5f;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, smoothStuff, gameObject.transform.position.z);
        }
    }

    /// <summary>
    /// Called by the player's bullet on collision with the boss,
    /// applies damage to the boss based on which spot the bullet hit.
    /// </summary>
    /// <param name="critHit">Did the player's bullet hit the boss' weak spot?</param>
    void takeDamage(bool critHit)
    {
        // If the player hit the boss' weak spot,
        if (critHit)
        {
            // Take extra damage
            Health -= playerMan.playerDamage * 2;
            // Return so it doesn't move on to applying more damage
            return;
        }
        // Apply the normal amount of damage; this is only reachable if the critHit test doesn't pass
        Health -= playerMan.playerDamage;
    }

    /// <summary>
    /// Make the given spriterenderer, sr, flash white for a quick second
    /// </summary>
    /// <param name="sr">The given spriterenderer.</param>
    IEnumerator damageFlash(SpriteRenderer sr)
    {
        // Store the color that the object originally was
        //Color originalColor = sr.color;

        // Set the object's color to white
        sr.color = new Color(1, 1, 1);
        // Wait for a tiny lil bit
        yield return new WaitForSeconds(.05f);
        // Restore the object's original color
        sr.color = Color.red;

        // Exit the method
        yield break;
    }

    /// <summary>
    /// Plays damageFlash on both the main body of the boss and the weak spot.
    /// </summary>
    IEnumerator damageEffect()
    {
        // Play the effect on the main body
        StartCoroutine(damageFlash(mainBody));
        // Play the effect on the weak spot
        //StartCoroutine(damageFlash(weakSpot));
        //maybe play a hit sound?
        yield break;
    }

    /// <summary>
    /// Bounce between the top and the bottom of the screen,
    /// shooting large bullets in 3-bullet bursts.
    /// </summary>
    /*IEnumerator normalBehavior()
    {
        while(true)
        {
            float smoothStuff = Mathf.SmoothStep(gameObject.transform.position.y, boundary, rate);
            if (gameObject.transform.position.y < boundary)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, smoothStuff, gameObject.transform.position.z);
            else
                boundary *= -1;
        }
    }*/

}
