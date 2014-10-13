using UnityEngine;
using System.Collections;

public class deleteEffect : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        this.Destroy(this.gameObject, this.GetComponentInChildren<ParticleSystem>().duration);
	}
}
