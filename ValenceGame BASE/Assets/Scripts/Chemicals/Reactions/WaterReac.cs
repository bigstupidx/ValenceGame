using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterReac : Chemical.Reaction {
	
	
//	public WaterReac() : base("Water Maker") {
		
//		reactants = new Dictionary<Chemical.Compound, int>();
//		reactants.Add (new H2 (), 2);
//		reactants.Add (new O2 (), 1);

//		products = new Dictionary<Chemical.Compound, int>();
//		reactants.Add (new Water (), 2);


//		reactant1 = this.holder.AddComponent<H2> ();
//		rCoeff1 = 2;
//		reactant2 = this.holder.AddComponent<O2> ();
//		rCoeff2 = 1;
//		reactant3 = null;
//		rCoeff3 = 0;
//		product1 = this.holder.AddComponent<Water> ();
//		pCoeff1 = 2;
//		product2 = null;
//		pCoeff2 = 0;
//		product3 = null;
//		pCoeff3 = 0;
//
//		energyType = energy.Refreshing;
//	}



	public override void Start () {

		reactName = "Water Fusion";

		reactant1 = this.gameObject.AddComponent<H2> ();
		rCoeff1 = 2;
		reactant2 = this.gameObject.AddComponent<O2> ();
		rCoeff2 = 1;
		reactant3 = null;
		rCoeff3 = 0;
//		reactant3 = this.gameObject.AddComponent<CarbDiox>();
//		rCoeff3 = 2;
//		reactant2 = null;
//		rCoeff2 = 0;
//		reactant3 = this.gameObject.AddComponent<O2> ();
//		rCoeff3 = 1;
		product1 = this.gameObject.AddComponent<H2O> ();
		pCoeff1 = 2;
		product2 = null;
		pCoeff2 = 0;
		product3 = null;
		pCoeff3 = 0;
		
		energyType = energy.Refreshing;

        noteText = @"<color=black>This is the hydrogen and oxygen lab. You’ll need to utilize all your resources to make it out of here in one piece. 
I know it’s still a prototype, but maybe the Catalyst can help you. Its database is still incomplete, so you’ll have to program it yourself. I don’t have much time, but I’ll leave you with this unbalanced equation to get you started...

☐ H2 + ☐ O2 -> ☐ H2O

Just in case you forgot, chemistry is not magic. Elements must be combined in certain proportions to react - they cannot violate the laws of conservation of mass. 
In each reaction, there must be the same amount of each element on both sides of the equation. 

Large numbers multiply across the entire compound, while subscripts only apply to the element they are attached to. 
For example: A2 + 2B2 -> 2A2B.</color>";
	}
	
	// Update is called once per frame
	public override void Update () {
		//	
	}
	
	
	

	
	
}