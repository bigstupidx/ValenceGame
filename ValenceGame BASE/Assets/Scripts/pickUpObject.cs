using UnityEngine;
using System.Collections;

public class pickUpObject : MonoBehaviour
{

    public bool isNear = false;
    public bool pickUp = false;

	private GameObject player;
	private GameObject cam;
	private GameObject box;

    // Use this for initialization
    void Start ()
	{       
		player = GameObject.Find ("Player");    
		cam = GameObject.Find ("Main Camera");
		//box = GameObject.Find ("Box");
    }
    
    // Update is called once per frame
    void Update ()
    {
        // isNear
        float distance = 2.0f;
        //GameObject box = GameObject.Find ("Box"); // Object you want to move!!! You should be able to change the string and it should work with whatever

        if ((player.transform.position - this.gameObject.transform.position).magnitude < distance) {
            isNear = true;
        } else {
            isNear = false;
        }

        // Hitting E and picking up and dropping
        if (Input.GetKeyDown (KeyCode.E)) {   
            if (pickUp) {
                pickUp = false;
				this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            } else if (isNear) {
                pickUp = true;
            }
        }

        // After pick up
        Vector3 newPos;
        newPos.x = player.transform.position.x + (player.transform.forward.x * 2);
        newPos.z = player.transform.position.z + (player.transform.forward.z * 2);
        newPos.y = player.transform.position.y + (cam.transform.forward.y * 6);

        if (newPos.y <= 1) {
            newPos.y = 1;
        }

        if (pickUp) {
			this.gameObject.transform.position = newPos;
			this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

    }

    void OnGUI ()
    {
        // Trigger prompt text when close to object
        if (isNear) {
            showText ();
        }
    }

    void showText ()
    {
        GUI.Label (new Rect (Screen.width / 2 - 60, 80, Screen.width, Screen.height), "<size=24>Press 'E' to pick up.</size>");
    }
}
