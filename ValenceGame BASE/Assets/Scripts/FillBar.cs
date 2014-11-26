using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillBar : MonoBehaviour {
    public Scrollbar fillBar1;
    public Scrollbar fillBar2;
    public Scrollbar fillBar3;
    public GameObject player;
    public float react1;
    public float react2;
    public float react3;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        react1 = (float)player.GetComponent<GunScript>().reactTank1.capacity;
        fillBar1.size = react1 / 400f;
        react2 = (float)player.GetComponent<GunScript>().reactTank2.capacity;
        fillBar2.size = react2 / 400f;
        react3 = (float)player.GetComponent<GunScript>().reactTank3.capacity;
        fillBar3.size = react3 / 400f;
	}
}
