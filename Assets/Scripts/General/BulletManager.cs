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

    // List of objects that should have an explosion on collision
    //string[] objects = {"Player", "Enemy", "Mine"};

    // Projectile explosions
    public GameObject mineBlast;
    public GameObject bulletBlast;

    // Update is called once per frame
    private void Start()
    {
        // When the player or the enemy fires a bullet,
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "EnemyBullet")
            // Make a lil explosion
            Instantiate(bulletBlast, transform.position, transform.rotation);
        // Lower the speed a little bit if the bullet is actually a mine
		if (gameObject.name == "PlayerMine") 
		{
			bulletSpeed = 4;
			mineMovement ();
		}
    }
    void FixedUpdate () 
	{
        // Move the bullet
		//if(gameObject.tag == "PlayerBullet")
		transform.position += transform.right * bulletSpeed * Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object being destroyed is a mine,
		if (gameObject.tag != "PlayerBullet" && gameObject.tag != "EnemyBullet")
            // Create the object that handles the explosion
            Instantiate(mineBlast, transform.position, transform.rotation);
        // If the object isn't a mine,
        else
            // Make a lil poof instead of a mine explosion
            Instantiate(bulletBlast, transform.position, transform.rotation);
    }

    /// <summary>
    /// Moves the player's mine at a decreasing rate,
    /// eventually blowing up once it hits something or gets to 0 speed.
    /// </summary>
	IEnumerator mineMovement()
	{
		float waitTime = Time.time + 4;
		while (Time.time < waitTime)
			transform.position += transform.right * bulletSpeed * Time.deltaTime * ((waitTime - Time.time) / 3);
		Instantiate(mineBlast, transform.position, transform.rotation);
		yield break;
	}

    /*bool collisionChecker(Collision2D other)
    {
        // Run through all of accepted objects
        for (int pos = 0; pos < objects.Length; pos++)
            // If the object collided with is one of these objects,
            if (other.gameObject.name == objects[pos])
                return true;
        // If the object isn't one on the list,
        return false;
    }*/
}
