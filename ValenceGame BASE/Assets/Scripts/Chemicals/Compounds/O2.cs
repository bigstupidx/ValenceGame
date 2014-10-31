using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class O2 : Chemical.Compound {

//	public O2() : base("O2") {
		//
//		atoms = new Dictionary<Chemical.Element, int>();
//		atoms.Add (new Oxygen(), 2);
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
	
	
	// Use this for initialization
	public override void Start () {
        compoundName = "O2";
        formula = "O2";
        state = stateOfMatter.gas;

		atoms = new Dictionary<Chemical.Element, int>();
		atoms.Add (new Oxygen(), 2);
	}
	
	// Update is called once per frame
	public override void Update () {

	}

    public override void init()
    {
        state = stateOfMatter.gas;

    }
}
