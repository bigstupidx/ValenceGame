using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Selection : MonoBehaviour {
	public GameObject player;
	public Image react1;
	public Image react2;
	public Image react3;
	public Image prod1;
	public Image prod2;
	public Image prod3;
	public Image highlight1;
	public Image highlight2;
	public Image highlight3;
	public Image highlight4;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		react1.color = Color.white;
		react2.color = Color.white;
		//react3.color = Color.black;
		prod1.color = Color.white;
		prod2.color = Color.white;
		//prod3.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<GunScript> ().reactTank1.isActive) {
			react1.color = Color.white;
			highlight1.color = Color.yellow;
		}
		else{
			react1.color = Color.white;
			highlight1.color = Color.clear;
		}
		if (player.GetComponent<GunScript> ().reactTank2.isActive) {
			highlight2.color = Color.yellow;
		}
		else{
			highlight2.color = Color.clear;
		}
		if (player.GetComponent<GunScript> ().prodTank1.isActive) {
			highlight3.color = Color.yellow;
		}
		else{
			highlight3.color = Color.clear;
		}
		if (player.GetComponent<GunScript> ().prodTank2.isActive) {
			highlight4.color = Color.yellow;
		}
		else{
			highlight4.color = Color.clear;
		}
	}
}
