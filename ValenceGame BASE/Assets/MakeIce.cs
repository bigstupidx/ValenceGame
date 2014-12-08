using UnityEngine;
using System.Collections;

public class MakeIce : MonoBehaviour {
	public GameObject player;
	public GameObject ice;
	public GameObject target;
    public AudioSource iceCrash;

	public int capacity;
	public int maxCapacity;

	// Use this for initialization
	void Start () {
		capacity = 0;
        iceCrash = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other){
		if(GameObject.Find("Ice") == null && GameObject.Find("Ice(Clone)") == null){
			if (player.GetComponent<GunScript> ().chemToShoot.Formula == "H2O") {
				if(capacity < maxCapacity){	
					capacity += 1;
					//Instantiate(ice, new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z), Quaternion.identity);
				}else if(capacity >= maxCapacity){

                    iceCrash.Play();

					Instantiate(ice, new Vector3(target.transform.position.x, target.transform.position.y - 10, target.transform.position.z), Quaternion.identity);
                    GameObject.Find("IceCube").transform.position = new Vector3(target.transform.position.x, target.transform.position.y+1, target.transform.position.z);
					capacity = 0;
				}
			}
		}
	}
}
