using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes stuff move faster or slower relative to the player's speed.
/// </summary>
public class RelativeSpeed : MonoBehaviour
{
    GameObject player;
    PlayerManager playerMan;
    ParticleSystem particles;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMan = player.GetComponent<PlayerManager>();
        particles = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        particles.playbackSpeed = playerMan.Speed;
	}
}
