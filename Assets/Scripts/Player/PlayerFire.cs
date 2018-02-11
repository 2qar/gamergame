using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    // Gun variables n stuff
	float nextFire;
	public float fireRate = 2;
    public Vector3 bulletOffset = new Vector3(.2f, 0, 0);

    // Bullet objects
    public GameObject bullet;
    public GameObject mine;

    // Object that manages the game stuff
	public GameController gameController;
	
	// Update is called once per frame
	void Update () 
	{
		// If the player presses the shoot key,
		if (Input.GetKeyDown (KeyCode.Space) && Time.time >= nextFire) 
		{
            // SHOOT
			fireWeapon (gameController.playerMan.Weapon);
		}
	}

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

        // Subtract some fuel so the player doesn't spam their weapon
        gameController.playerMan.Speed -= .1f;
	}
}