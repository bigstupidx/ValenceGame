using UnityEngine;
using System.Collections;

public class Instuction_Script : MonoBehaviour {

	public GUISkin mySkin;
	bool display=true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
		{
			if(display==true){display=false;}
			else if(display==false){display=true;}
		}
	}

	void OnGUI () {
		GUI.skin = mySkin;

		if(display)
		{
			GUI.Box (new Rect (200, 200, 250, 200), "WASD - Movement\nSpace - Jump\nRight Click - Absorb elements\nLeft Click - Fire elements\n" +
				"Mouse Scroll Wheel - Choose reaction\n1-6 - Choose tank to shoot\nR - React\nQ - Vent\nE - Read\nI - Open and close instructions");

			GUI.Box(new Rect(275,370,100,30),"Press I to exit");
		}
	}
}
/* WASD for movement
right click to absorb elements
left click to fire elements
mouse scroll wheel to choose reaction?
1 - 6 to choose tank to shoot?
r to react
q to vent
e to read
*/
