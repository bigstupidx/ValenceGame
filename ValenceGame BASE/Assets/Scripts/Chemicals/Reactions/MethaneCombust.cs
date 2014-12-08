using UnityEngine;
using System.Collections;

public class MethaneCombust : Chemical.Reaction {

	// Use this for initialization
   public override void Start()
    {
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

        noteText = @"This is the Thermal Reaction lab. Some of the pipes burst in the explosions, but I think the ice machine over there is still functional...

I don’t know what use it is without water though. To be honest, I don’t know why I’m working here... I don’t know anything about chemistry. 

What I DO know is you can set stuff on fire with this: 

☐ CH4 + ☐ O2 -> ☐ CO2 + ☐ H2O.";

	}
	
	// Update is called once per frame
   public override void Update()
   {
	
	}
}
