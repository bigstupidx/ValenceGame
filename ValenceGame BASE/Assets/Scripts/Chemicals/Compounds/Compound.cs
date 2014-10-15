using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Element;

namespace Chemical {

	//[System.Serializable]
	public abstract class Compound {

		protected string formula;
		protected Dictionary<Element, int> atoms;
		//protected GameObject accessor;

		//need frozen + hot properties


		protected Compound(string f) {
			formula = f;

		}


		public string getFormula() {
			return formula;
		}

		public int getNumAtoms(Element elem) {
			return atoms [elem];
		}


		public abstract int damage (string obstacleName);		//"damages" the object
			//pass the name of the object that the ray is pointing at
			//this function will return damage if the compound removes the object
				//otherwise returns 0
		
		public abstract int heal (string obstacleName);
			//pass the name of the object that the ray is pointing at
			//this function will return NEGATIVE damage if the compound feeds or increases the object
				//otherwise returns 0


		//YOU KNOW, MAYBE WE JUST NEED ONE FUNCTION, AND RETURN - damage FOR HEALING


		/*
		public abstract void interact ();	
			//can't seem to make these protected...
			//to enclose remove and augment? maybe not needed.

		public abstract bool remove ();		//"damages" the object
			//pass the object that the ray is pointing at
			//this function will return true if the compound removes the object

		public abstract bool augment ();
			//this function will return true if the compound increases/grows the object
		*/




		/*

		properties for each compound

		GIVES PROPERTIES OF ELEMENT TO OBJECT IT'S ATTACHED TO

			water puts out fires, dilutes acids and bases, slowly melts ice
			acid eats away at stuff, neutralizes bases
			o2 increases fires
			h2 floats, explodes
			methane catches fire
			co2 puts out fires, displaces gas

			lead + sulphuric acid = battery
		 */


		/*
			interact with:

			public GameObject player;
			player.GetComponent
		 */


		//public bool testMethod() {
	//		return accessor.GetComponent<GunScript>().eqBalanced;
	//	}

		// Use this for initialization
		//public abstract void Start ();
		
		// Update is called once per frame
		//public abstract void Update ();
	}
}
