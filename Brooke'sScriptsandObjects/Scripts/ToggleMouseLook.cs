using UnityEngine;
using System.Collections;

public class ToggleMouseLook : MonoBehaviour {

    public bool lookOn;
	// Use this for initialization
	void Start () {
        lookOn = true;
	}
	
	// Update is called once per frame
	void Update () {
    	if(Input.GetKeyUp(KeyCode.I)){
            if (lookOn)
            {
                GameObject.Find("Player").GetComponent<MouseLook>().enabled = false;
                lookOn = false;
            }
            else
            {
                GameObject.Find("Player").GetComponent<MouseLook>().enabled = true;
                lookOn = true;
            }
        }
	}
}
