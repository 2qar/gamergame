using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMineExplode : MonoBehaviour
{
    CircleCollider2D collider;
    public GameObject mineBlast;

	// Use this for initialization
	void Start ()
    {
        // Get the mine collider
        collider = GetComponent<CircleCollider2D>();
	}

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Enemy" || collision.gameObject.name == "Mine")

    }*/
    private void OnDestroy()
    {
        // Create a blast
        Instantiate(mineBlast, transform.position, transform.rotation);
    }
}
