using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chemical {

	public abstract class Reaction {
	    // A reaction consumes one or more Reactants and creates one or more Products

		protected string reactName;
		//public string ReactName {
		//	get {
		//		return reactName;
		//	}
		//}
		protected Dictionary<Chemical.Compound, int> reactants;	//contains correct coeff ratios
			//want coeffs given compounds
			//want list of compounds
		//-> maybe use accessors?
		protected Dictionary<Chemical.Compound, int> products;	//contains correct coeff ratios
		public Dictionary<Chemical.Compound, int>.KeyCollection Products {
			get {
				return products.Keys;
			}
		
		}


		protected enum energy {
			Exotherm, Endotherm, Refreshing, None
		};


		protected Reaction(string rN) {
			reactName = rN;
		}

		public string getReactName() {
			return reactName;
		}
	
	}
}