using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Finish this dumb monobehaviour you big sillyhead
// TODO: Make explosion damage scale with the player's damage

public class PlayerMineExplode : MonoBehaviour
{
    // Get the explosion's collider
    CircleCollider2D collider;

	// Use this for initialization
	void Start ()
    {
        // Get the mine collider
        collider = GetComponent<CircleCollider2D>();
	}

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the explosion collided with an enemy,
        if(collision.gameObject.tag == "Enemy")
        {
            // If the enemy is a normal enemy,
            if(collision.gameObject.name.Contains("Enemy"))
            {
                // Get it's management script
                EnemyManager2 enemyMan = collision.gameObject.GetComponent<EnemyManager2>();
                // Set the health to 0, maybe tweak this later so it takes health based on player damage?
                enemyMan.Health = 0;
            }
            // If the enemy is a mine or an asteroid or something,
            else
            {
                // Get the management script
                EnemyManager1 enemyMan = collision.gameObject.GetComponent<EnemyManager1>();
                // Update it's health, it doesn't need health but it's a property so changing the value makes it do a specific thing
                enemyMan.Health--;
            }
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the explosion collided with an enemy,
        if (collision.gameObject.tag == "Enemy")
        {
            // If the enemy is a normal enemy,
            if (collision.gameObject.name.Contains("Enemy"))
            {
                // Get it's management script
                EnemyManager2 enemyMan = collision.gameObject.GetComponent<EnemyManager2>();
                // Set the health to 0, maybe tweak this later so it takes health based on player damage?
                enemyMan.Health = 0;
            }
            // If the enemy is a mine or an asteroid or something,
            else
            {
                // Get the management script
                EnemyManager1 enemyMan = collision.gameObject.GetComponent<EnemyManager1>();
                // Update it's health, it doesn't need health but it's a property so changing the value makes it do a specific thing
                enemyMan.Health--;
            }
        }
    }

}
