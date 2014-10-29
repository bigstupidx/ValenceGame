using UnityEngine;
using System.Collections;

public class iceCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){

		GameObject scale1 = GameObject.Find ("Scale1");
		GameObject scale2 = GameObject.Find ("Scale2");
		GameObject box = GameObject.Find ("Box");
		GameObject partner=null;

		if(this.gameObject==scale1){partner=scale2;}
		else if(this.gameObject==scale2){partner=scale1;}

		Vector3 newPos1 = this.gameObject.transform.position;
		Vector3 newPos2 = partner.transform.position;

		if (col.gameObject == box) {
			newPos1.y-=0.1f;
			newPos2.y+=0.1f;
			if(newPos1.y<=1){newPos1.y=1;}
			if(newPos2.y<=1){newPos2.y=1;}
			this.gameObject.transform.position=newPos1;
			partner.transform.position=newPos2;
		}
	}
}
