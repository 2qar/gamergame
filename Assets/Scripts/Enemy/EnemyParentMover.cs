using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentMover : MonoBehaviour
{
    public int speed = 5;

	void Start () {
		
	}
	
	void FixedUpdate ()
    {
        transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
    }

}