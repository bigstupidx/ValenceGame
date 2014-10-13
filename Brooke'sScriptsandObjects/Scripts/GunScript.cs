using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	public int power;
	public int numclicks;
	
    public GameObject emitter;
	
    public bool isEmitting;
	public bool isEmpty;
    public bool isEquation;     //is there a specific equation that is being used to get elements
    public bool balanced;

    public GameObject effect;

    public int capacity;
	public int Elem1capacity;
    public int Elem2capacity;

	const int fullCap = 400;

    public string elemName;
    public string elem1Name;
    public string elem2Name;

    public int rateElem1;
    public int rateElem2;

    //can only absorb 2 things (for now)
    public GameObject absorb1;
    public GameObject shoot1;
    public GameObject absorb2;
    public GameObject shoot2;

	// Use this for initialization
	void Start () {
		isEmpty = true;
		isEmitting = false;
		capacity = 0;
        elemName = " ";
	}
	
	// Update is called once per frame
	void Update () {
        //venting
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Elem1capacity = 0;
            Elem2capacity = 0;
        }
        //identifying elements
        Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        Physics.Raycast(ray1, out hit, 1000f);

        if (hit.transform.tag == "Absorbable")
        {
            if (hit.distance < 8)
            {
                elemName = hit.transform.GetComponent<ElementScript>().name;
            }
        }
        else
        {
            elemName = " ";
        }


		//absorbing
		if (Input.GetMouseButton(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
			
			RaycastHit hit2;
			Physics.Raycast(ray, out hit2, 1000f);
			
			if (hit2.transform.tag == "Absorbable")
			{
				if (hit2.distance < 8)
				{
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3(hit2.normal.x - ray.direction.x, hit2.normal.y - ray.direction.y, hit2.normal.z - ray.direction.z);
					Instantiate(effect, hit2.point, q);
					isEmpty = false;

                    if (isEquation && balanced)
                    {
                        if (hit2.transform.GetComponent<ElementScript>().name == elem1Name)
                        {
                            if (Elem1capacity < fullCap)
                            {
                                Elem1capacity += 1;
                            }
                        }
                        if (hit2.transform.GetComponent<ElementScript>().name == elem2Name)
                        {
                            if (Elem2capacity < fullCap)
                            {
                                Elem2capacity += 1;
                            }
                        }
                    }
                    else if (!isEquation)
                    {
                        elem1Name = hit2.transform.GetComponent<ElementScript>().name;
                        if (Elem1capacity < fullCap)
                        {
                            Elem1capacity += 1;
                        }
                    }
				}
			}
		}


		//shooting
        if (balanced || !isEquation)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                numclicks++;
                if (!isEmitting && !isEmpty)	//will need to code the absorb mechanic
                {
                    isEmitting = true;
                    emitter.particleSystem.Play();
                }
            }
            if (Input.GetButton("Fire1"))
            {
                if (isEmitting && !isEmpty)
                {
                    if (Elem1capacity > 0 && Elem2capacity > 0)
                    {
                        isEmpty = false;
                        isEmitting = true;
                        emitter.particleSystem.Play();
                        Elem1capacity -= 1 * rateElem1;
                        Elem2capacity -= 1 * rateElem2;
                    }
                    else
                    {
                        isEmpty = true;
                        isEmitting = false;
                        emitter.particleSystem.Stop();
                    }
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (isEmitting)
                {
                    isEmitting = false;
                    emitter.particleSystem.Stop();

                    if (Elem1capacity <= 0 && Elem2capacity <= 0)
                    {
                        isEmpty = true;
                    }
                }
            }
        }
	}
}
