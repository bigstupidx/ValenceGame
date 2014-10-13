using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterReac : Chemical.Reaction {
	
	
	public WaterReac() : base("Water Maker") {
		
		reactants = new Dictionary<Chemical.Compound, int>();
		reactants.Add (new H2 (), 2);
		reactants.Add (new O2 (), 1);

		products = new Dictionary<Chemical.Compound, int>();
		reactants.Add (new Water (), 2);
	}
	
	
	

	
	
}