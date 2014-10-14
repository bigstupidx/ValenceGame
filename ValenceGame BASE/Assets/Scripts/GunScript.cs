using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	public int power;
		//never used in GunScript
	public int numclicks;
		//what is this for?
	
    public GameObject emitter;
	
    public bool isEmitting;
	public bool isEmpty;
	"public bool isEquation;     //is there a specific equation that is being used to get elements
		//what does this do?
	public bool eqBalanced;		//was 'balanced'

    public GameObject effect;

	//compound capacities in tank
	public int combineCap;		//was 'capacity'
	public int tank1Cap;		//was 'elem1capacity'
	public int tank2Cap;		//was 'elem2capacity'
	public int tank3Cap;
	const int fullCap = 400;

	//compound names
    public string cursorName;	//was "elemName"
	public string tank1Name;	//was 'elem1Name'
	public string tank2Name;	//was 'elem2Name'

	//compound use rates
	public int tank1Rate;		//was 'rateElem1'
	public int tank2Rate;		//was 'rateElem2'

	public int sprayDamage;		//to use with different elements and objects

	public Chemical.Compound prodChem;

	public Chemical.Reaction activeReact;



	//can only absorb 2 things (for now)
    "public GameObject absorb1;
    "public GameObject shoot1;
    "public GameObject absorb2;
    "public GameObject shoot2;


	// Use this for initialization
	void Start () {
		isEmpty = true;
		isEmitting = false;
		eqBalanced = false;
		combineCap = 0;
		cursorName = " ";
		tank1Name = "O2";	//not brookes. MUST REMOVE HARD CODE
		tank2Name = "H2";	//not brookes. 
		tank1Rate = 1;		//not brookes. 
		tank2Rate = 2;		//not brookes. 
		//perhaps include tankElem3 at some point?

		sprayDamage = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //venting
        if (Input.GetKeyDown(KeyCode.Q))
        {
			tank1Cap = 0;
			tank2Cap = 0;
        }
        //identifying elements
        Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        Physics.Raycast(ray1, out hit, 1000f);

        if (hit.transform.tag == "Absorbable")
        {
            if (hit.distance < 8)
            {
				cursorName = hit.transform.name;
				'elemName = hit.transform.GetComponent<ElementScript>().name;
					//i suppose she sets elementScripts name for each object it's attached to
            }
        }
        else
        {
			cursorName = " ";
        }

		if (eqBalanced) {
			prodChem = new Water();		//obviously change this to not be hardcoded water, but current compound
				//THIS DOESN'T WORK WITH MONOBEHAVIOURS! must "AddComponent()", but don't know how that works.
				//I'm going to try and make Compounds just a regular class.
			//works with Compounds being a regular class! :)


			activeReact = new WaterReac();

		}

		//identifying objects (for damaging)
		if(prodChem != null && !isEmpty) {

			//foreach(Chemical.Compound comp in activeReact.Products) {
			//	sprayDamage = comp.damage(hit.transform.name);
					//so maybe just give property to emitter, and detect hit on object?

			//	emitter.GetComponent<Extinguished>().particleDamage = sprayDamage;
			//}


			sprayDamage = prodChem.damage(hit.transform.name);
				//this will be used in script Extinguished

			emitter.GetComponent<Extinguished>().particleDamage = sprayDamage;
		}

		//absorbing
		if (Input.GetMouseButton(1) && eqBalanced)
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
					if (hit.transform.name == tank1Name)
                    {
						if (tank1Cap < fullCap)
                        {
							tank1Cap += 1;
                        }
                    }
					if (hit.transform.name == tank2Name)
                    {
						if (tank2Cap < fullCap)
                        {
							tank2Cap += 1;
                        }
                    }

					

					'if (isEquation && balanced)
                    '{
                    '    if (hit2.transform.GetComponent<ElementScript>().name == elem1Name)
							//where is elem1name set?
                    '    {
                    '        if (Elem1capacity < fullCap)
                    '        {
                    '            Elem1capacity += 1;
                    '        }
                    '    }
                    '    if (hit2.transform.GetComponent<ElementScript>().name == elem2Name)
                    '    {
                    '        if (Elem2capacity < fullCap)
                    '        {
                    '            Elem2capacity += 1;
                    '        }
                    '    }
                    '}
                    'else if (!isEquation)
                    '{
                    '    elem1Name = hit2.transform.GetComponent<ElementScript>().name;
                    '    if (Elem1capacity < fullCap)
                    '    {
                    '        Elem1capacity += 1;
                    '    }
                    '}

				}
			}
		}


		//shooting
        if (eqBalanced)
			'if (balanced || !isEquation)
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
					if (tank1Cap > 0 && tank2Cap > 0)
                    {
                        isEmpty = false;
                        isEmitting = true;
                        emitter.particleSystem.Play();
						tank1Cap -= 1 * tank1Rate;
						tank2Cap -= 1 * tank2Rate;
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

					if (tank1Cap <= 0 && tank1Cap <= 0)
                    {
                        isEmpty = true;
                    }
                }
            }
        }
	}
}
