using UnityEngine;
using System.Collections;

public class CatchFire : MonoBehaviour {

    public bool coll;
    public GameObject effect;

	void OnControllerColliderHit(ControllerColliderHit col){
        if (col.transform.name == "Firewall")
        {
            coll = true;
            Instantiate(effect, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        }
    }

    void Update()
    {
        if (coll == true)
        {
            effect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }
}
