using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

	float nextFire;
	public float fireRate = 2;
    public Vector3 bulletOffset = new Vector3(.2f, 0, 0);

    public GameObject bullet;
	public GameController gameController;
	
	// Update is called once per frame
	void Update () 
	{
		/*if (Input.GetKeyDown (KeyCode.Space) && Time.time >= nextFire && gameController.weapon == 1) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (bullet, transform.position + bulletOffset, transform.rotation);
		}*/
		if (Input.GetKeyDown (KeyCode.Space) && Time.time >= nextFire && gameController.weapon == 2) 
		{
			nextFire = Time.time + fireRate;
            for (int reps = 15; reps >= -15; reps -= 15)
                Instantiate(bullet, transform.position + bulletOffset, Quaternion.Euler(new Vector3(0, 0, reps)));
        }
        else if(Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position + bulletOffset, transform.rotation);
        }
	}
}
