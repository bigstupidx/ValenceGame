using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //editable
    public Vector3 cameraSpeed;
    public int moveSpeed;
    public int buffer; //"sensitivity" to camera motion

    public CharacterController controller;

    //noteditable
    public Vector2 mousePos;
    public int screenW;
    public int screenH;

    public float xrat;
    public float yrat;

    public Vector3 rot;
	
	void Start () {
        screenW = Screen.width;
        screenH = Screen.height;
	}
	
	void Update () {
        //-------------camera rotation------------------//
        //Note: rotation along x-axis in 3D space makes camera look up and down. 
        //Rotation along y-axis in 3D space makes camera look side to side. 
        //Rotation along z axis (not used) makes it look like the viewer is turning upside down. Think head tilting to the side.

        rot = this.transform.localEulerAngles;

        //mouse position relative to center of screen
        mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - (screenW / 2);
        mousePos.y = mousePos.y - (screenH / 2);

        if (Mathf.Sqrt((mousePos.x * mousePos.x) + (mousePos.y * mousePos.y)) > buffer)//if distance of mouse from center is certain amount
        { 
            xrat = Mathf.Cos(mousePos.x / Mathf.Sqrt((mousePos.x * mousePos.x) + (mousePos.y * mousePos.y))) - (Mathf.PI / 2);
            yrat = Mathf.Sin(mousePos.y / Mathf.Sqrt((mousePos.x * mousePos.x) + (mousePos.y * mousePos.y)));
           
            rot.x -= cameraSpeed.y * yrat;

            if (mousePos.x > 0) //because cosine goes from 0 to pi, when mouseX is negative, cosine will still be positive.
            {
                rot.y -= (cameraSpeed.x * (xrat));
            }
            else
            {
                rot.y -= (cameraSpeed.x * (- xrat));
            }
        }

        this.transform.localEulerAngles = rot;

        //-----------end camera rotation--------------//
        //---------------Movement------------------//

        //CharacterController controller = GameObject.Find("Player").GetComponent<CharacterController>();

        Vector3 moveDirection = Vector3.zero;
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(this.transform.forward.x * Input.GetAxis("Horizontal"), 0, this.transform.forward.z * Input.GetAxis("Vertical"));
            moveDirection *= moveSpeed;
        }
        moveDirection.y += (Physics.gravity.y * (float)9.81 * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

	}
}
