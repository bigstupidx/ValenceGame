using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class H2 : Chemical.Compound {



//	public H2() : base("H2") {
		
//		atoms = new Dictionary<Chemical.Element, int>();
//		atoms.Add (new Hydrogen(), 2);
//		
//	}


	public override int damage (string obstacleName) {
		
		return 0;
	}
	
	public override int heal (string obstacleName) {
		
		return 0;
	}

	/*
	public override void interact() {

	}
	
	public override bool augment() {

		return false;
	}
	
	public override bool remove() {

		return false;
	}

	*/

	public override void Start() {
		formula = "H2";

		atoms = new Dictionary<Chemical.Element, int>();
		atoms.Add (new Hydrogen(), 2);
	}

	public override void Update() {}

}
