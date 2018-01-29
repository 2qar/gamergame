using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Lower the speed a little bit if the bullet is actually a mine
        if (gameObject.name == "PlayerMine")
            bulletSpeed = 4;
    }
    void FixedUpdate () 
	{
        // Move the bullet
		transform.position += transform.right * bulletSpeed * Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
            // If the object being destroyed is a mine,
            if (gameObject.name == "PlayerMine")
                // Create the object that handles the explosion
                Instantiate(mineBlast, transform.position, transform.rotation);
            // If the object isn't a mine,
            else
                // Make a lil poof instead of a mine explosion
                Instantiate(bulletBlast, transform.position, transform.rotation);
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
