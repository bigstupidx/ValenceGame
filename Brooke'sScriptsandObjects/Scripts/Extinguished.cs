using UnityEngine;
using System.Collections;

public class Extinguished : MonoBehaviour {
    public ParticleSystem effect;

	void OnParticleCollision(GameObject other){
		if (other.name == "Firewall") {
            other.GetComponent<FireOut>().health -= 1;
            //Destroy(other);
		}
        //Instantiate(effect, , q);
	}
}
