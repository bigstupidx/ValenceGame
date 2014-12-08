using UnityEngine;
using System.Collections;

public class Instuction_Script : MonoBehaviour {

	public GUISkin mySkin;
	public bool display=true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1))
		{
			if(display==true){display=false;}
			else if(display==false){display=true;}
		}
	}

	void OnGUI () {
		GUI.skin = mySkin;

		if(display)
		{
			GUI.Box (new Rect (200, 200, 250, 230), "WASD - Movement\nSpace - Jump\nRight Click - Absorb elements\nLeft Click - Fire elements\n" +
				"Mouse Scroll Wheel - Choose element\n Tab - Choose Reaction\nR - React\nQ - Vent\nE - Read\nF1 - Open and close instructions");

			GUI.Box(new Rect(250,400,150,30),"Press F1 to exit");
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
