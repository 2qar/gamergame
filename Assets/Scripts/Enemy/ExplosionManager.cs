using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make a little bar below the player's health that fills up when picking up particles, when the bar fills, heal 1 HP
    // Maybe make this an end of level powerup?
// TODO: Make a powerup at the end of the level that increases the time that particles linger
// TODO: Make the particles award more than just speed to the player

/// <summary>
/// Handles all of the particles that enemies spit out when they blow up.
/// </summary>
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

    // Sound to make when the player picks up a particle
    public GameObject particleNoise;

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

        // Set the object up to be destroyed once the particles have disappeared
        Destroy(gameObject, explosion.startLifetime);
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Set the velocity of the particles based on the player's movement speed
        velocity = 2 + (playerMan.Speed / 10);
        // Get the velocity module of the particle system
        var velocityOverLifetime = explosion.velocityOverLifetime;
        // Enable it for use in code
        velocityOverLifetime.enabled = true;
        // Set the velocity on this component to be the velocity set from before
        velocityOverLifetime.x = -1 * (velocity);
	}

    private void OnParticleCollision(GameObject other)
    {
        // If the player isn't already at their max speed,
        if(playerMan.Speed < playerMan.MaxSpeed)
            // Add to their speed
            playerMan.Speed += .1f;

        // Play the particle sound
        GameObject sound = Instantiate(particleNoise, transform.position, transform.rotation);
        // Destroy the sound object after it's done playing
        Destroy(sound, .2f);
    }

    /// <summary>
    /// Grabs all of the particles that were created and stuffs them into an array
    /// before running a method to set up these particles.
    /// </summary>
    IEnumerator getParticlesAlive()
    {
        // Wait a lil bit for the particles to exist
        yield return new WaitForSeconds(.01f);
        // Get how many particles are alive, and store the particles in an array
        particlesAlive = explosion.GetParticles(particles);
        // Report back how many particles exist
        //Debug.Log(particlesAlive);
        // Set up these particles
        initializeParticles();
    }

    // TODO: Make this method actually work before you write documentation you knob
    /// <summary>
    /// Assigns each individual particle a random color that will determine their
    /// behavior when they collide with the player.
    /// </summary>
    void initializeParticles()
    {
        // report back that the program has made it to this method
        //Debug.Log("initializing");
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
