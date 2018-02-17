using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add a different sound effect for shooting mines

/// <summary>
/// Shoots bullets when the player presses the bullet shooty button.
/// </summary>
public class PlayerFire : MonoBehaviour 
{
    // Gun variables n stuff
	float nextFire;
	public float fireRate = 2;
    public Vector3 bulletOffset = new Vector3(.2f, 0, 0);

    // Bullet objects
    public GameObject bullet;
    public GameObject mine;

    // Object that makes a lil noise
    public GameObject bulletSound;

    // Object that manages the game stuff
	public GameController gameController;

    // Update is called once per frame
    void Update () 
	{
		// If the player presses the shoot key,
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown("Fire1")) && Time.time >= nextFire) 
		{
            // SHOOT
			fireWeapon (gameController.playerMan.Weapon);
		}
	}

    /// <summary>
    /// Instantiates certain projectiles in certain amounts based on the
    /// weapon parameter.
    /// </summary>
    /// <param name="weapon">The weapon that the player currently has.</param>
	private void fireWeapon(int weapon)
	{
        // * * * * * * * * * * * *
        // * Player Weapon Checks *
        // * * * * * * * * * * * *

        // Weapon #1: Single Fire
        // Fires a single bullet projectile
        if (weapon == 1)
            Instantiate(bullet, transform.position + bulletOffset, transform.rotation);

        // Weapon #2: Triple Fire
        // Same as normal fire, but
        // fires an extra bullet on each side
        else if (weapon == 2)
            for (int reps = 15; reps >= -15; reps -= 15)
                Instantiate(bullet, transform.position + bulletOffset, Quaternion.Euler(new Vector3(0, 0, reps)));

        // Weapon #3: Mine
        // Fire a slow-moving mine that explodes after x seconds
        // OR on contact w/ enemy
        else if (weapon == 3)
            Instantiate(mine, transform.position + bulletOffset, transform.rotation);
        else
            Debug.Log("NO WEAPON MADE FOR WEAPON VAR #" + weapon);
		
		// Set cooldown for next shot
		nextFire = Time.time + fireRate;

        // If the game isn't in a wait period, like in a shop,
        if(!gameController.waitBeforeWave)
            // Subtract fuel when they fire
            gameController.playerMan.Speed -= .1f;

        // Make a lil noise
        GameObject noise = (GameObject)Instantiate(bulletSound, transform.position, transform.rotation);
        // Destroy it after so there's no garbage sitting around
        Destroy(noise, .5f);
	}
}