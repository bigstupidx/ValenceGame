using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour {
    public bool coll;

	void OnControllerColliderHit(ControllerColliderHit col){
        if (col.transform.name == "Player")
        {
            coll = true;
            Application.LoadLevel("Level1");
        }
    }
}
