using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	public float bulletSpeed = 5;

    //Rigidbody2D rb;


	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position += transform.right * bulletSpeed * Time.deltaTime;
	}

}
