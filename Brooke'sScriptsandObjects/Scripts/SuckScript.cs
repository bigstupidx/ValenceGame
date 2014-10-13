using UnityEngine;
using System.Collections;

public class SuckScript : MonoBehaviour
{

    public GameObject effect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000f);

            if (hit.transform.tag == "Absorbable")
            {
                if (hit.distance < 8)
                {
                    Quaternion q = Quaternion.identity;
                    q.eulerAngles = new Vector3(hit.normal.x - ray.direction.x, hit.normal.y - ray.direction.y, hit.normal.z - ray.direction.z);
                    Instantiate(effect, hit.point, q);
                }
            }
        }
    }
}