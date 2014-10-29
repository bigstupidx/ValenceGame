using UnityEngine;
using System.Collections;

public class Extinguished : MonoBehaviour {
	//CHANGE NAME OF THIS!

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

//		if(other.name == "IceCube") {
//			Vector3 shrink = new Vector3(0.9, 0.9, 0.9);
//			Vector3 aPosition = new Vector3(1, 1, 1);
//			other.transform.localScale = Vector3.Scale(other.transform.localScale, new Vector3(0.95F, 0.95F, 0.95F));

//			if(other.transform.localScale.x < 0.2F) {
//				other.gameObject.SetActive(false);
//			}
//			Vector3 scale = other.transform.localScale;
//			other.transform.localScale.x = scale.x - 0.1F;
//			other.transform.localScale.y = scale.y - 0.1F;
//			other.transform.localScale.z = scale.z - 0.1F;
//		}
        //Instantiate(effect, , q);
	}
}
