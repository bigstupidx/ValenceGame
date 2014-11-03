using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : Chemical.Compound {


//	public Water() : base("H2O") {
//
//		atoms = new Dictionary<Chemical.Element, int>();
//		atoms.Add (new Hydrogen(), 2);
//		atoms.Add (new Oxygen (), 1);
//	}



	public override int damage (string obstacleName) {

		if(obstacleName == "Firewall") {
			return 2;
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
        compoundName = "Water";
        formula = "H2O";
        state = stateOfMatter.liquid;

        atoms = new Dictionary<Chemical.Element, int>();
		atoms.Add (new Hydrogen(), 2);
		atoms.Add (new Oxygen (), 1);
	}
    
	// Update is called once per frame
	public override void Update () {
//	
	}

    public override void init()
    {
        state = stateOfMatter.liquid;

    }
}
