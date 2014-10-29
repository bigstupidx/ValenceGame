using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public int power;
    //never used in GunScript
    public int numclicks;
    //what is this for?

    //public GameObject emitter;  //HARDCODE

    public bool isEmitting;
    public bool isEmpty;
    public bool reactSelected;    //was 'isEquation' //is there a specific equation that is being used to get elements

    public bool eqBalanced;		//was 'balanced'

    public GameObject effect;   //HARDCODE

    //compound capacities in tank
    public int combineCap;		//was 'capacity'
    public int tank1Cap;		//was 'elem1capacity'
    public int tank2Cap;		//was 'elem2capacity'
    public int tank3Cap;
    private int fullCap = 400; //needs to be private to be accessed  by gui

    //compond names and compound rates will need to be set by the reaction
    //compound names
    public string cursorName;	//was "elemName"
    public string tank1Name;	//was 'elem1Name'
    public string tank2Name;	//was 'elem2Name'

    //compound use rates
    public int tank1Rate;		//was 'rateElem1'
    public int tank2Rate;		//was 'rateElem2'

    public int sprayDamage;		//to use with different elements and objects

    public string chemToShootName;
    public Chemical.Compound chemToShoot;
   	public Chemical.Reaction activeReact;
    public GameObject shootEffect;

    //used for different particle emitters
    public GameObject absorb1;		//for compound 1
    public GameObject absorb2;		//for compound 2

    public int getFullCap()  //So GUI can know the maximum capacity in order to scale the gui to screen
    {
        return fullCap;
    }

    // Use this for initialization
    void Start()
    {
        isEmpty = true;
        isEmitting = false;
        eqBalanced = false;
        reactSelected = false;
        combineCap = 0;
        cursorName = " ";
        tank1Rate = 2;		//unless reaction needed, no need to adjust rates
        tank2Rate = 0;		//can only absorb one element when reaction not selected
        //perhaps include tankElem3 at some point?

        sprayDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }
        else
        {
            cursorName = " ";
        }

        if (eqBalanced && this.gameObject.GetComponent<Chemical.Compound>() == null)
        {
            //SEMI-HARDCODE NEEDS TO BE CHANGED WHEN PRODUCT SELECTION IMPLEMENTED
            chemToShootName = activeReact.Product1.CompoundName;

            chemToShoot = this.gameObject.AddComponent(chemToShootName) as Chemical.Compound;

            //shootEffect = Instantiate(this.gameObject.GetComponent<Chemical.Compound>().shooter, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            shootEffect = GameObject.Find("ShootGun").GetComponent<GunParticleSwitcher>().setParticleSystem(chemToShoot.shooter);

        }

        //identifying objects (for damaging)
        if (this.gameObject.GetComponent<Chemical.Compound>() != null && !isEmpty)
        {
            sprayDamage = chemToShoot.damage(hit.transform.name);
            //this will be used in script Extinguished

            shootEffect.GetComponent<Extinguished>().particleDamage = sprayDamage;
        }

        //absorbing
        if (Input.GetMouseButton(1) && (eqBalanced || !reactSelected))
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

                    if (reactSelected && activeReact != null)
                    {	//specific absorb if specific reaction selected
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeReact.Reactant1.getFormula())
                        {
                            tank1Name = activeReact.Reactant1.getFormula();
                            tank1Rate = activeReact.ReactCoeff1;
                            if (tank1Cap < fullCap)
                            {
                                tank1Cap += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;

                            }
                        }
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeReact.Reactant2.getFormula())
                        {
                            tank2Name = activeReact.Reactant2.getFormula();
                            tank2Rate = activeReact.ReactCoeff2;

                            if (tank2Cap < fullCap)
                            {
                                tank2Cap += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;
                            }
                        }
                    }
                    else if (!reactSelected)
                    {	//to absorb anything without specific reaction selected
                        if (tank1Cap == 0)  //can absorb anything into tank1
                        {
                            tank1Name = hit2.transform.GetComponent<Chemical.Compound>().getFormula();
                            tank1Cap += 2;

                            Instantiate(effect, hit2.point, q);
                            isEmpty = false;
                        }
                        else if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == tank1Name)
                        {
                            if (tank1Cap < fullCap)
                            {
                                tank1Cap += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;
                            }
                        }
                    }
                }
            }
        }


        //shooting
        //if (eqBalanced)
        if (eqBalanced || !reactSelected)	//to allow to shoot if no reaction selected
        {
            if (Input.GetButtonDown("Fire1"))
            {
                numclicks++;
                if (!isEmitting && !isEmpty)	//will need to code the absorb mechanic
                {
                    isEmitting = true;
                    shootEffect.particleSystem.Play();
                }
            }
            if (Input.GetButton("Fire1"))
            {
                if (isEmitting && !isEmpty)
                {
                    if (reactSelected && activeReact != null) //is using more than one element
                    {
                        if (tank1Cap > 0 && tank2Cap > 0)
                        {
                            isEmpty = false;
                            isEmitting = true;
                            shootEffect.particleSystem.Play();
                            tank1Cap -= 1 * tank1Rate;
                            tank2Cap -= 1 * tank2Rate;
                        }
                        else
                        {
                            isEmpty = true;
                            isEmitting = false;
                            shootEffect.particleSystem.Stop();
                        }
                    }
                    else //not a reaction, just single compound
                    {
                        if (tank1Cap > 0)
                        {
                            isEmpty = false;
                            isEmitting = true;
                            shootEffect.particleSystem.Play();
                            tank1Cap -= 1 * tank1Rate;
                        }
                        else
                        {
                            isEmpty = true;
                            isEmitting = false;
                            shootEffect.particleSystem.Stop();
                        }
                    }
                }
            }
            if (Input.GetButtonUp("Fire1")) //stop emitting
            {
                if (isEmitting)
                {
                    isEmitting = false;
                    shootEffect.particleSystem.Stop();

                    if (tank1Cap <= 0 && tank1Cap <= 0)
                    {
                        isEmpty = true;
                    }
                }
            }
        }
    }
}
