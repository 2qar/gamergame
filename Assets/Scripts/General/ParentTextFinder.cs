using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Find any object using this script and give it a shadow, then remove any trace of this dumb script

public class ParentTextFinder : MonoBehaviour
{
    GameObject parentTextObj;
    Text parentText;
    Text objText;
	// Use this for initialization
	void Start ()
    {
        parentTextObj = GameObject.FindGameObjectWithTag(gameObject.name.Substring(0, name.Length - 2));
        parentText = parentTextObj.GetComponent<Text>();
        objText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        objText.text = parentText.text;
        objText.fontSize = parentText.fontSize;
        objText.gameObject.SetActive(parentText.isActiveAndEnabled);
	}
}
