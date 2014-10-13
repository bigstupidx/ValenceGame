using UnityEngine;
using System.Collections;

public class FireOut : MonoBehaviour {
    
    public GameObject effect;

    public int health;

    void Update()
    {
        if (health <= 0) Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        /*Instantiate(effect, new Vector3(this.transform.position.x, this.transform.position.y - (float)4.5, this.transform.position.z), Quaternion.identity);

        Destroy(effect, 20);*/
    }
}
