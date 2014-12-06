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
	public int s;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		react1.color = Color.clear;
		react2.color = Color.clear;
		//react3.color = Color.black;
		prod1.color = Color.clear;
		prod2.color = Color.clear;
		//prod3.color = Color.black;
		highlight1.color = Color.clear;
		highlight2.color = Color.clear;
		highlight3.color = Color.clear;
		highlight4.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
		clearUnusedFrames ();
		if (player.GetComponent<GunScript> ().reactTank1.isActive) {
			highlight1.color = Color.yellow;
		}
		else{
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
		if (player.GetComponent<GunScript> ().prodTank2.isActive && player.GetComponent<GunScript> ().activeReact.Product2 != null) {
			highlight4.color = Color.yellow;
		}
		else{
			highlight4.color = Color.clear;
		}
		if(Input.GetKeyDown(KeyCode.Tab)){
			highlight1.color = Color.clear;
			highlight2.color = Color.clear;
			highlight3.color = Color.clear;
			highlight4.color = Color.clear;
		}
	}
	private void clearUnusedFrames(){
		if (player.GetComponent<GunScript> ().activeReact.Reactant1 != null) {
			react1.color = Color.white;
		} 
		else {
			react1.color = Color.clear;
		}
		if (player.GetComponent<GunScript> ().activeReact.Reactant2 != null) {
			react2.color = Color.white;
		}
		else {
			react2.color = Color.clear;
		}
		if (player.GetComponent<GunScript> ().activeReact.Product1 != null) {
			prod1.color = Color.white;
		}
		else {
			prod1.color = Color.clear;
		}
		if (player.GetComponent<GunScript> ().activeReact.Product2 != null) {
			prod2.color = Color.white;
		}
		else {
			prod2.color = Color.clear;
		}
		//} else {
			//react1.color = Color.white;
		//}
	}
}
