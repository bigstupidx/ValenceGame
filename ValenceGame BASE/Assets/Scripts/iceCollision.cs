using UnityEngine;
using System.Collections;

public class iceCollision : MonoBehaviour
{
	public GameObject partner;
	public bool weighedDown;
	//private Vector3 resetPosition;
	public float resetY;
    public float lowerY;

	public bool init;

	public float speed;
    // Use this for initialization
    void Start ()
	{
		speed = 0.3f;
		init = true;
    }
    
    // Update is called once per frame
    void Update ()
    {
		if(init){
			if (weighedDown==false && partner.GetComponent<iceCollision>().weighedDown==false) {
				reset();
			}

			if (this.gameObject.transform.position.y < lowerY) {
				Vector3 newPos1 = this.gameObject.transform.position;
				Vector3 newPos2 = partner.transform.position;
				
				newPos1.y = lowerY;
				newPos2.y = 10;
				
				this.gameObject.transform.position = newPos1;
				partner.transform.position = newPos2;
			}
		}else {
			if (weighedDown==false && partner.GetComponent<iceCollision>().weighedDown==false) {
				reset();
			}

			// Stop platforms from going through the floor, sets limits
			if (this.gameObject.transform.position.y < lowerY) {
				Vector3 newPos1 = this.gameObject.transform.position;
				Vector3 newPos2 = partner.transform.position;

				newPos1.y = lowerY;
				newPos2.y = 10;

				this.gameObject.transform.position = newPos1;
				partner.transform.position = newPos2;
			}
		}
    }

    void OnCollisionStay (Collision col)
    {
		if (col.gameObject.tag == "Weight"){
			weighedDown = true;

       		Vector3 newPos1 = this.gameObject.transform.position;
        	Vector3 newPos2 = partner.transform.position;
			// Rate at which the platforms move
            // The original box is ruled by gravity so that effects the rate also
            // Rates below 0.1 can cause problems where the platform only moves once
            newPos1.y -= speed * Time.deltaTime;
			newPos2.y += speed * Time.deltaTime;

            this.gameObject.transform.position = newPos1;
            partner.transform.position = newPos2;
        }
    }

	void reset()
	{
		if(init){
			if(this.transform.position.y == resetY){

			}
			else if(this.transform.position.y < resetY){

				Vector3 newPos1 = this.gameObject.transform.position;
				Vector3 newPos2 = partner.transform.position;
				
				newPos1.y += 5 * Time.deltaTime;
				newPos2.y -= 5 * Time.deltaTime;
				
				this.gameObject.transform.position = newPos1;
				partner.transform.position = newPos2;
			}
		}else {
			if (this.transform.position.y == resetY) {
							// do nothing
			} 

			else if (this.transform.position.y < resetY) {
				Vector3 newPos1 = this.gameObject.transform.position;
				Vector3 newPos2 = partner.transform.position;
				
				newPos1.y += speed * Time.deltaTime;
				newPos2.y -= speed * Time.deltaTime;

				this.gameObject.transform.position = newPos1;
				partner.transform.position = newPos2;
			}
		}
	}

	void OnCollisionExit(Collision col){
		weighedDown = false;
	}
}
