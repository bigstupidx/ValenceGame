using UnityEngine;
using System.Collections;

public class CustomFillbar : MonoBehaviour {
	private int fullCap;
	public Texture2D texture1;
	public Material material;
	public GameObject player;	
	public string productName;
	public float revealOffset;

	const int relScreenW = 640; //relative values for screen size
	const int relScreenH = 400;
	public float ratW;
	public float ratH;
	private float full;

	// Use this for initialization
	void Start () {
		fullCap = player.GetComponent<GunScript>().getFullCap();
		ratH = (float)Screen.height / (float)relScreenH;
		ratW = (float)Screen.width / (float)relScreenW;
		full = 100;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (Event.current.type.Equals (EventType.Repaint)) {
			Rect box = new Rect(10 * ratW, (Screen.height) - 130*ratH, 200, 200);
			Graphics.DrawTexture(box, texture1, material);
		}
	}

	void Update(){

		float amount = 6/100;
		if (amount == 0) {
			amount = 0.1f;
		}
		revealOffset = 1f - (float)(Time.timeSinceLevelLoad)%10 / 10.1F;
		material.SetFloat("_Cutoff", revealOffset);//Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x));
		//material.SetFloat ("_Cutoff", amount);
	}
}
