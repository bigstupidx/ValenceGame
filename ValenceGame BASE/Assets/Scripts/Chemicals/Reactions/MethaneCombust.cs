using UnityEngine;
using System.Collections;

public class MethaneCombust : Chemical.Reaction {

	// Use this for initialization
   public override void Start()
    {
		unlocked = true;

		reactName = "Methane Combustion";
		
		reactant1 = this.gameObject.AddComponent<Methane> ();
		rCoeff1 = 1;
		reactant2 = this.gameObject.AddComponent<O2> ();
		rCoeff2 = 2;
		reactant3 = null;
		rCoeff3 = 0;
		product1 = this.gameObject.AddComponent<H2O> ();
		pCoeff1 = 2;
		product2 = this.gameObject.AddComponent<CarbDiox>();
		pCoeff2 = 1;
//		product3 = this.gameObject.AddComponent<O2>();
//		pCoeff3 = 2;
		product3 = null;
		pCoeff3 = 0;
		
		energyType = energy.Combust;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
