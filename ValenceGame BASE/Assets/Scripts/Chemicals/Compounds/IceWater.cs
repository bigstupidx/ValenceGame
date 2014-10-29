using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceWater : Chemical.Compound {
	
	
	//	public Water() : base("H2O") {
	//
	//		atoms = new Dictionary<Chemical.Element, int>();
	//		atoms.Add (new Hydrogen(), 2);
	//		atoms.Add (new Oxygen (), 1);
	//	}
	
	
	
	public override int damage (string obstacleName) {
		
		if(obstacleName == "Firewall") {
			return 1;
		}
		
		return 0;
	}
	
	public override int heal (string obstacleName) {
		
		if(obstacleName == "Firewall") {
			return 0;
		}
		
		return 0;
	}
	
	
	/*
	public override void interact() {
		/*
		use "is()" to check type!
		 *
	}

	public override bool augment() {
		/*
		use "is()" to check type!
		 *
		return false;
	}

	public override bool remove() {
		/*
		use "is()" to check type!
		 *
		return false;
	}

	*/
	
	// Use this for initialization
	public override void Start () {
		compoundName = "IceWater";
		formula = "H2O";
		
		atoms = new Dictionary<Chemical.Element, int>();
		atoms.Add (new Hydrogen(), 2);
		atoms.Add (new Oxygen (), 1);
		
		
	}

	void OnParticleCollision(GameObject other){
		if(other.tag == "WaterGunEmitter") {
			//			Vector3 shrink = new Vector3(0.9, 0.9, 0.9);
			//			Vector3 aPosition = new Vector3(1, 1, 1);
			this.gameObject.transform.localScale = Vector3.Scale(this.gameObject.transform.localScale, new Vector3(0.95F, 0.95F, 0.95F));
			
			if(this.gameObject.transform.localScale.x < 0.25F) {
				this.gameObject.gameObject.SetActive(false);
			}
			//			Vector3 scale = other.transform.localScale;
			//			other.transform.localScale.x = scale.x - 0.1F;
			//			other.transform.localScale.y = scale.y - 0.1F;
			//			other.transform.localScale.z = scale.z - 0.1F;
		}
		//Instantiate(effect, , q);
	}
	
	// Update is called once per frame
	public override void Update () {
		//	
	}
}