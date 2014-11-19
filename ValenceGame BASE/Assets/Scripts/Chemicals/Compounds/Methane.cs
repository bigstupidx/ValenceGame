using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Methane : Chemical.Compound
{



    //	public Methane() : base("CH4") {
    //		
    //		atoms = new Dictionary<Chemical.Element, int>();
    //		atoms.Add (new Carbon(), 1);
    //		atoms.Add (new Hydrogen (), 4);
    //	}



    public override int damage(string obstacleName)
    {

        if (obstacleName == "Firewall")
        {
            return -2;
        }

        return 0;
    }

    public override int heal(string obstacleName)
    {

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

    public override void Start()
    {
        color = Color.green;
        compoundName = "Methane";
        formula = "CH4";
        state = stateOfMatter.gas;

        atoms = new Dictionary<Chemical.Element, int>();
        atoms.Add(new Carbon(), 1);
        atoms.Add(new Hydrogen(), 4);

    }

    public override void Update() { }

    public override void init()
    {
        state = stateOfMatter.gas;
        color = Color.green;

    }

}
