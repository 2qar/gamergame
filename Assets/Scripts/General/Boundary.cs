using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour 
{
	void OnTriggerExit2D (Collider2D other)
	{
        if(other.gameObject.tag != "Player")
        {
            if (other.transform.parent != null)
                Destroy(other.transform.parent.gameObject);
            if (other.transform.name == "Enemy")
                Destroy(other.transform.Find("Booster").gameObject);
            Destroy(other.gameObject);
        }
    }
}
