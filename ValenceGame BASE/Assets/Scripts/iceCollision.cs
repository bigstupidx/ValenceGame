using UnityEngine;
using System.Collections;

public class iceCollision : MonoBehaviour
{
	public GameObject partner;

    // Use this for initialization
    void Start ()
    {
    
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    void OnCollisionStay (Collision col)
    {

		if (col.gameObject.tag == "Weight"){

       		Vector3 newPos1 = this.gameObject.transform.position;
        	Vector3 newPos2 = partner.transform.position;
			// Rate at which the platforms move
            // The original box is ruled by gravity so that effects the rate also
            // Rates below 0.1 can cause problems where the platform only moves once
            newPos1.y -= 0.3f * Time.deltaTime;
			newPos2.y += 0.3f * Time.deltaTime;;
            if (newPos1.y <= 1) {
                newPos1.y = 1;
                newPos2.y = partner.transform.position.y;
            }
            if (newPos2.y <= 1) {
                newPos2.y = 1;
                newPos1.y = partner.transform.position.y;
            }
            this.gameObject.transform.position = newPos1;
            partner.transform.position = newPos2;
        }
    }
}
