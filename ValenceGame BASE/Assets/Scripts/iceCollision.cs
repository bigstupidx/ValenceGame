using UnityEngine;
using System.Collections;

public class iceCollision : MonoBehaviour
{
	public GameObject partner;
	public bool weighedDown;
	//private Vector3 resetPosition;
	public float resetY;

    // Use this for initialization
    void Start ()
    {
		//resetPosition = this.transform.position;
    }
    
    // Update is called once per frame
    void Update ()
    {
		if (weighedDown==false && partner.GetComponent<iceCollision>().weighedDown==false) {
			reset();
		}

		// Stop platforms from going through the floor, sets limits
		if (this.gameObject.transform.position.y<1) {
			Vector3 newPos1 = this.gameObject.transform.position;
			Vector3 newPos2 = partner.transform.position;

			newPos1.y = 1;
			newPos2.y = 8;

			this.gameObject.transform.position = newPos1;
			partner.transform.position = newPos2;
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
            newPos1.y -= 0.5f * Time.deltaTime;
			newPos2.y += 0.5f * Time.deltaTime;

            this.gameObject.transform.position = newPos1;
            partner.transform.position = newPos2;
        }
    }

	void reset()
	{
		if (this.transform.position.y == resetY) {
						// do nothing
		} 

		else if (this.transform.position.y < resetY) {
			Vector3 newPos1 = this.gameObject.transform.position;
			Vector3 newPos2 = partner.transform.position;
			
			newPos1.y += 0.3f * Time.deltaTime;
			newPos2.y -= 0.3f * Time.deltaTime;

			this.gameObject.transform.position = newPos1;
			partner.transform.position = newPos2;
		}
	}

	void OnCollisionExit(Collision col){
		weighedDown = false;
	}
}
