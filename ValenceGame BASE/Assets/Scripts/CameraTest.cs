using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour {

	public bool isNear=false;
	public bool pickUp=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// isNear
		float distance = 2.0f;
		GameObject player = GameObject.Find ("Player");
		GameObject box = GameObject.Find ("Box");
		GameObject cam = GameObject.Find ("Main Camera");
		if ((player.transform.position - box.transform.position).magnitude < distance) {
			isNear = true;
		} else {
			isNear = false;
		}

		// Hitting E and picking up
		if (Input.GetKeyDown(KeyCode.E))
		{   
			if(pickUp)
			{
				pickUp=false;
				box.rigidbody.useGravity=true;
			}
			else if (isNear)
			{
				pickUp = true;
			}
		}

		// After pick up
		Vector3 newPos;
		newPos.x = player.transform.position.x + (player.transform.forward.x * 5);
		newPos.z = player.transform.position.z + (player.transform.forward.z * 5);
		newPos.y = player.transform.position.y + (cam.transform.forward.y * 8);
		if(newPos.y<=1){newPos.y=1;}
		//print (newPos.y);

		if (pickUp) {
			box.transform.position=newPos;
			box.rigidbody.useGravity=false;
		}

	}

	void OnGUI() {

		GameObject player = GameObject.Find ("Player");
		GameObject box = GameObject.Find ("Box");

		// Trigger prompt text when close to note
		if (isNear) {
			showText();
		}

		if (pickUp) {

		}

	}

	void showText() {
		GUI.Label(new Rect(Screen.width/2 - 60, 80, Screen.width, Screen.height), "<size=24>Press 'E' to pick up.</size>");
	}
}
