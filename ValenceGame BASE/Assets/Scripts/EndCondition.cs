using UnityEngine;
using System.Collections;

public class EndCondition : MonoBehaviour {
    public bool coll;

	void OnControllerColliderHit(ControllerColliderHit col){
        if (col.transform.name == "Level1")
        {
            coll = true;
            Application.LoadLevel("Level1");
        }
        if (col.transform.name == "End")
        {
            coll = true;
            Application.Quit();
        }
    }
}
