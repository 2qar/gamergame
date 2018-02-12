using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour 
{
    // The explosion itself
    private ParticleSystem explosion;
    // Array that will hold all of the particles
    private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[50];
    // Stores how many particles currently exist
    int particlesAlive;
    // Stores the speed that these particles will be travelling at
    private float velocity;

    // The player gameobject / manager
    private GameObject player;
    private PlayerManager playerMan;

	// Use this for initialization
	void Start () 
    {
        // Get the particle system
        explosion = gameObject.GetComponent<ParticleSystem>();

        // Get player components
        player = GameObject.FindGameObjectWithTag("Player");
        playerMan = player.GetComponent<PlayerManager>();
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();

        // Enable collision between the player and the particles
        explosion.trigger.SetCollider(0, playerCollider);
        // Get how many particles there are and store each particle
        StartCoroutine(getParticlesAlive());
	}
	
	// Update is called once per frame
	void Update () 
    {
        // If the object has been alive for more than 1 second and there aren't any particles left,
        if (Time.time > 1 && explosion.particleCount == 0)
            // Destroy it
            Destroy(gameObject);
        // Set the velocity of the particles based on the player's movement speed
        velocity = 2 + (playerMan.Speed / 10);
        // Get the velocity module of the particle system
        var velocityOverLifetime = explosion.velocityOverLifetime;
        // Enable it for use in code
        velocityOverLifetime.enabled = true;
        // Set the velocity on this component to be the velocity set from before
        velocityOverLifetime.x = -1 * (velocity);
	}

    // TODO: Fix the initializeParticles method so that every single particle
    // that collides with the player isn't just increasing their speed
    private void OnParticleCollision(GameObject other)
    {
        if(playerMan.Speed < 25.0)
            playerMan.Speed += .1f;
    }

    IEnumerator getParticlesAlive()
    {
        // Wait a lil bit for the particles to exist
        yield return new WaitForSeconds(.01f);
        // Get how many particles are alive, and store the particles in an array
        particlesAlive = explosion.GetParticles(particles);
        // Report back how many particles exist
        Debug.Log(particlesAlive);
        // Set up these particles
        initializeParticles();
    }

    void initializeParticles()
    {
        // report back that the program has made it to this method
        Debug.Log("initializing");
        // Run through each particle that exists
        for (int pos = 0; pos < particlesAlive; pos++)
        {
            // Pick a random num that will determine color
            int color = (int)Random.Range(1f, 4f);
            if (color == 1)
                particles[pos].color = new Color(255, 0, 255);
            else if (color == 2)
                particles[pos].color = new Color(0, 255, 255);
            else
                particles[pos].color = new Color(0, 0, 255);
        }
    }

}
