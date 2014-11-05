using UnityEngine;
using System.Collections;

[System.Serializable]
public class ElementScript : MonoBehaviour {
    
	public string name;
	public Chemical.Compound compound;

//	[SerializeField]
//	public Chemical.Compound Compound {
//		set {
//			if(value == H2()) {
//				compound = new H2();
//			}
		//
		//	compound = value;
//		}
//		get {
//			return compound;		
//		}
//	}
	public ParticleSystem absorb;
    public ParticleSystem shoot;

	void Start() {
		if (name == "H2") {
			compound = new H2();
		}
		if (name == "O2") {
			compound = new O2();
		}
		if (name == "Water" || name == "H2O") {
			compound = new H2O();
		}
		if (name == "Methane" || name == "CH4") {
			compound = new Methane();
		}
	}
}
