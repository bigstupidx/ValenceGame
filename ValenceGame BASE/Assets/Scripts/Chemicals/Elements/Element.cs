using UnityEngine;
using System.Collections;

namespace Chemical {

	public abstract class Element {

		protected string symbol;
		protected int valenceElecs;

		protected Element(string s, int vE) {
			symbol = s; 
			valenceElecs = vE;
		}

		public string getSymbol() {
			return symbol;
		}

		public int getValence() {
			return valenceElecs;
		}

		// Use this for initialization
		//public abstract void Start ();
		
		// Update is called once per frame
		//public abstract void Update ();
	}

}