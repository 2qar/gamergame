using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles movement and behavior of all the projectiles that the player and enemies spit out.
/// </summary>
public class BulletManager : MonoBehaviour
{
    // Speed the projectile will travel at
    public float bulletSpeed = 5;
    // Offset of the bullet explosion
    //Vector3 bulletOffset = new Vector3(.6f, 0);

    // Projectile explosions
    public GameObject mineBlast;
    public GameObject bulletBlast;

    // Mine explode sound effect
    public GameObject mineExplodeNoise;

    // The gamecontroller
    private GameController controller;

    // Stores whether the object is a mine or not
    private bool isMine = false;

    // Update is called once per frame
    private void Start()
    {
        // Get the gamecontroller
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // When the player or the enemy fires a bullet,
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "EnemyBullet")
            // Make a lil explosion
            Instantiate(bulletBlast, transform.position, transform.rotation);
        // Lower the speed a little bit if the bullet is actually a mine
		if (gameObject.name == "PlayerMine") 
		{
			bulletSpeed = 4;
            // TODO: Use mathf.smoothstep or mathf.smoothdamp to make the mine slowly go to zero speed after it's been moving for like 2 seconds
		}

        // Check to see if the projectile is a mine, and update if true
        if (gameObject.name.Contains("PlayerMine"))
            isMine = true;
    }
    void FixedUpdate () 
	{
        // If the projectile isn't a mine,
        if (!isMine)
            // Move normally based on rotation
            transform.position += transform.right * bulletSpeed * Time.deltaTime;
        // If it is a mine,
        else
            // Move right
            transform.position += new Vector3(1 * bulletSpeed * Time.deltaTime, 0, 0);

        // If the projectile is a mine,
        if (isMine)
            // Rotate constantly
            gameObject.transform.Rotate(new Vector3(0, 0, -3f));
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // <EFFECTS STUFF>
        // If the object being destroyed is a mine,
		if (gameObject.name.Contains("PlayerMine"))
        {
            // Create the object that handles the explosion
            Instantiate(mineBlast, transform.position, transform.rotation);
            // Create the explosion sound effect
            GameObject explosionSound = Instantiate(mineExplodeNoise, transform.position, transform.rotation);
            // Destroy it after it has finished playing
            Destroy(explosionSound, .6f);
            // If the thing that the mine collided with was an enemy's bullet,
            if (collision.gameObject.tag == "EnemyBullet")
                // Destroy it
                Destroy(collision.gameObject);
            // Destroy the mine
            Destroy(gameObject);
        }
        // If the object isn't a mine,
        else if(gameObject.tag == "PlayerBullet" || gameObject.tag == "EnemyBullet")
            // Make a lil poof instead of a mine explosion
            Instantiate(bulletBlast, transform.position, transform.rotation);
            

        // <BOSS STUFF>
        // If the current bullet is a player bullet and the boss actually exists,
        if (gameObject.tag == "PlayerBullet" && controller.bossIsAlive)
        {
            // If the player is hitting the boss' normal collider, 
            if (collision.gameObject.name.Contains("Boss1"))
                // Do a normal amount of damage
                controller.bossMan.SendMessage("takeDamage", false);
            // If the player hit the boss' crit point,
            else if (collision.gameObject.name.Contains("CritPoint"))
                // Do double damage
                controller.bossMan.SendMessage("takeDamage", true);
            // Get rid of the player's bullet
            Destroy(gameObject);
        }
            
    }

}
