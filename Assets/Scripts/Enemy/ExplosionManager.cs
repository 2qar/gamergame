using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour 
{
    //private ParticleSystem explosion;

    //private GameObject player;

	// Use this for initialization
	void Start () 
    {
        // Get the particle system
        ParticleSystem explosion = gameObject.GetComponent<ParticleSystem>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        explosion.trigger.SetCollider(0, playerCollider);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Player")
            Destroy(gameObject);
    }
}
