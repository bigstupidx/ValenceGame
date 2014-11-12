using UnityEngine;
using System.Collections;

public class MakeIce : MonoBehaviour {
	public GameObject player;
	public GameObject ice;
	public GameObject target;

	public int capacity;

	// Use this for initialization
	void Start () {
		capacity = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other){
		if (player.GetComponent<GunScript> ().chemToShootName == "H2O") {
			if(capacity < maxCapacity){	
				capacity += 1;
				//Instantiate(ice, new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z), Quaternion.identity);
			}else if(capacity >= maxCapacity){
				Instantiate(ice, new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z), Quaternion.identity);
				capacity = 0;
			}
		}
	}
}
