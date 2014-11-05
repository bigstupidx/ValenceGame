using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tank {
    // Name of compound stored in tank
    public string name;
    // Rate at which a compound will be consumed
    public int rate;
    // Current amount of compound stored in tank
    public int capacity;
    // true if tank should be actively selected
    public bool isActive;

    public Tank(string n, int rt, int cp, bool act)
    {
        name = n;
        rate = rt;
        capacity = cp;
        isActive = act;
    }

    public Tank(string n, int rt)
    : this(n, rt, 0, false)
    {}
}

public class GunScript : MonoBehaviour
{
    public const int SHOOT_RATE = 1;

    public int power;
    //never used in GunScript

    public bool isEmitting;
    public bool isEmpty;
    public bool reactSelected;    //was 'isEquation' //is there a specific equation that is being used to get elements
        //what does this do?
    public bool eqBalanced;        //was 'balanced'

    public GameObject effect;   //HARDCODE

    // Tanks for reactants
    public Tank reactTank1;
    public Tank reactTank2;
    public Tank reactTank3;

    // Tanks for products
    public Tank prodTank1;
    public Tank prodTank2;
    public Tank prodTank3;

    public Tank[] reactTanks;
    public Tank[] prodTanks;
    public Tank[] allTanks;

    //compound capacities in tank
    public int combineCap;    
    private int fullCap = 400; //needs to be private to be accessed  by gui

    //compond names and compound rates will need to be set by the reaction
    //compound names
    public string cursorName;    //was "elemName"

    public int sprayDamage;        //to use with different elements and objects

    public string chemToShootName;
    public Chemical.Compound chemToShoot;

    public Chemical.Reaction activeReact;
    public Chemical.Reaction[] reactions;
    private int reactIndex;

    public GameObject shootEffect;

    //used for different particle emitters
    public GameObject absorb1;        //for compound 1
    public GameObject absorb2;        //for compound 2

    public int getFullCap()  //So GUI can know the maximum capacity in order to scale the gui to screen
    {
        return fullCap;
    }

    public Tank[] getActiveTanks()
    {
        return Array.FindAll(allTanks, x => x.isActive);
    }

    public Tank[] getActiveNonemptyTanks()
    {
        return Array.FindAll(allTanks, x => x.isActive && x.capacity > 0);
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

        reactTanks = new Tank[] {reactTank1, reactTank2, reactTank3};
        prodTanks = new Tank[] {prodTank1, prodTank2, prodTank3};
        allTanks = new Tank[] {reactTank1, reactTank2, reactTank3, prodTank1, prodTank2, prodTank3};

        reactions = this.gameObject.GetComponent<reactionMasterList>().initReactionList();
        reactIndex = 0;
        sprayDamage = 0;
    }
    public void Vent()
    {
        for (int i = 0; i < allTanks.Length; i++)
        {
            allTanks[i].capacity = 0;
            allTanks[i].name = "";
        }
        chemToShootName = "";
        chemToShoot = null;
        shootEffect = null;

        Chemical.Compound[] components = this.gameObject.GetComponents<Chemical.Compound> ();
        for (int i = 0; i < components.Length; i++)
        {
            Destroy(components[i]);
        }
    }

    public void selectTank(Tank selectTank)
    {
        for(int i = 0; i < allTanks.Length; i++)
        {
            Tank tank = allTanks[i];
            if (tank != selectTank)
            {
                tank.isActive = false;
            }
            else
            {
                tank.isActive = !tank.isActive;
            }
        }

        Chemical.Compound[] components = this.gameObject.GetComponents<Chemical.Compound> ();
        for (int i = 0; i < components.Length; i++)
        {
            Destroy(components[i]);
        }

        if (selectTank.name != "")
        {
            chemToShootName = selectTank.name;
            
            chemToShoot = this.gameObject.AddComponent(chemToShootName) as Chemical.Compound;
            chemToShoot.init();
            
            shootEffect = GameObject.Find("ShootGun").GetComponent<GunParticleSwitcher>().setParticleSystem(chemToShoot.state, chemToShoot.color);
            
        }
        else
        {
            chemToShootName = "";
            chemToShoot = null;
            shootEffect = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //venting------------------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vent();
        }

        //identifying elements------------------------------------------------------
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

        //Tank selection---------------------------------------------------------------------
        //Sets what is shot out based on what tank is selected
        //if tank is empty with no assigned value, will set what is shot to null
        if (Input.GetKeyDown("1"))
        {
            selectTank(reactTank1);
        }
        if (Input.GetKeyDown("2"))
        {
            selectTank(reactTank2);
        }
        if (Input.GetKeyDown("3"))
        {
            selectTank(reactTank3);
        }
        if (Input.GetKeyDown ("4")) 
        {
            selectTank(prodTank1);
        }
        if (Input.GetKeyDown("5"))
        {
            selectTank(prodTank2);
        }
        if (Input.GetKeyDown("6"))
        {
            selectTank(prodTank3);
        }

        //reaction selection-----------------------------------------------------------------
        if(Input.GetAxis("Mouse ScrollWheel") > 0) //scrollup
        {
            ++reactIndex;
            if(reactIndex > reactions.Length - 1){
                reactIndex = 0;
            }
            if (reactions[reactIndex].unlocked == true)
            {
                activeReact = reactions[reactIndex];
            }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)//scrolldown
        {
            --reactIndex;
            if(reactIndex < 0){
                reactIndex = reactions.Length - 1;
            }
            if (reactions[reactIndex].unlocked == true)
            {
                activeReact = reactions[reactIndex];
            }
           
        }
        //need way to "lock" reaction when selected

        //initializing for new reaction -----------------------------------------------
        if (activeReact != null)
        {   //active reaction isnt current reaction
            if (reactTank1.name != activeReact.Reactant1.getFormula())
            {
                Vent();

                if (activeReact.Reactant1 != null) 
                    reactTank1.name = activeReact.Reactant1.getFormula();
                else reactTank1.name = "";
                
                if (activeReact.Reactant2 != null) 
                    reactTank2.name = activeReact.Reactant2.getFormula();
                else reactTank2.name = "";
                
                if (activeReact.Reactant3 != null) 
                    reactTank3.name = activeReact.Reactant3.getFormula();
                else reactTank3.name = "";
                
                if (activeReact.Product1 != null) 
                    prodTank1.name = activeReact.Product1.getFormula();
                else prodTank1.name = "";
                
                if (activeReact.Product2 != null)
                    prodTank2.name = activeReact.Product2.getFormula();
                else prodTank2.name = "";
                
                if (activeReact.Product3 != null) 
                    prodTank3.name = activeReact.Product3.getFormula();
                else prodTank3.name = "";
            }
        }

        //absorbing----------------------------------------------------------------------------
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

                    if (activeReact != null)
                    {
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank1.name)
                        {
                            if (reactTank1.capacity < fullCap)
                            {
                                reactTank1.capacity += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;
                            }
                        }
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank2.name)
                        {
                            if (reactTank2.capacity < fullCap)
                            {
                                reactTank2.capacity += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;
                            }
                        }
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank3.name)
                        {
                            if (reactTank3.capacity < fullCap)
                            {
                                reactTank3.capacity += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;
                            }
                        }
                    }
                    else     //if there is no active reaction, must select tank to fill
                    {
                        Tank[] activeTanks = getActiveTanks();
                        for(int i = 0; i < activeTanks.Length; ++i){
                            if (activeTanks[i].name == "")  //can absorb anything into tank1
                            {
                                activeTanks[i].name = hit2.transform.GetComponent<Chemical.Compound>().getFormula();
                            
                                activeTanks[i].capacity += 2;

                                Instantiate(effect, hit2.point, q);
                                isEmpty = false;

                                break;
                            }
                        }
                        for (int i = 0; i < activeTanks.Length; ++i)
                        {
                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeTanks[i].name)
                            {
                                if (activeTanks[i].capacity < fullCap)
                                {
                                    activeTanks[i].capacity += 2;

                                    Instantiate(effect, hit2.point, q);
                                    isEmpty = false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        //shooting-------------------------------------------------------
        if (Input.GetButtonDown("Fire1"))
        {
            Tank[] activeNonemptyTanks = getActiveNonemptyTanks();
            if (activeNonemptyTanks.Length > 0 && !isEmitting)    //will need to code the absorb mechanic
            {
                isEmitting = true;
                shootEffect.particleSystem.Play();
            }
        }
        if (Input.GetButton("Fire1"))
        {
            // Look for active tanks that are not empty and fire those
            Tank[] activeNonemptyTanks = getActiveNonemptyTanks();
            Debug.Log(string.Format("Active Tanks: {0}", getActiveTanks().Length));
            Debug.Log(string.Format("Active Nonempty Tanks: {0}", activeNonemptyTanks.Length));
            if (isEmitting && activeNonemptyTanks.Length > 0)
            {
                isEmpty = false;
                isEmitting = true;
                //shootEffect.particleSystem.Play();
                //reactTank1.capacity -= SHOOT_RATE;
                for (int i = 0; i < activeNonemptyTanks.Length; i++)
                {
                    Tank tank = activeNonemptyTanks[i];
                    tank.capacity -= SHOOT_RATE;
                }
            }
            else
            {
                isEmpty = true;
                isEmitting = false;
                if(shootEffect != null)
                {
                    shootEffect.particleSystem.Stop();
                }
            }
        }
        if (Input.GetButtonUp("Fire1")) //stop emitting
        {
            if (isEmitting)
            {
                isEmitting = false;
                shootEffect.particleSystem.Stop();

                Tank[] nonemptyTanks = Array.FindAll(allTanks, x => x.capacity > 0);
                if (nonemptyTanks.Length == 0)
                {
                    isEmpty = true;
                }
            }
        }

        //identifying objects (for damaging)
        /*if (this.gameObject.GetComponent<Chemical.Compound>() != null && !isEmpty)
        {
            sprayDamage = chemToShoot.damage(hit.transform.name);
            //this will be used in script Extinguished

            //shootEffect.GetComponent<Extinguished>().particleDamage = sprayDamage;
        }*/

        

    }
}