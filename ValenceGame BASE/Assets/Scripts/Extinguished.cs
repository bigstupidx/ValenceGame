using UnityEngine;
using System.Collections;

public class Extinguished : MonoBehaviour {
    public ParticleSystem effect;

	public int particleDamage;

	void Start() {
		particleDamage = 0;
	}

	void OnParticleCollision(GameObject other){
		if (other.name == "Firewall") {
           // other.GetComponent<FireOut>().health -= 1 * other.GetComponent<GunScript>().sprayDamage;
				//how do i get the damage from the gun??? extinguished is attached to the particle system,
					//and fireout is attached to the firewall...

			//another try
			other.GetComponent<FireOut>().health -= 1 * particleDamage;

            //Destroy(other);
		}
        //Instantiate(effect, , q);
	}
}
