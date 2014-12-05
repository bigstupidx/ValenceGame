using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tank {
    // Name of compound stored in tank
//    public string name;
	public Chemical.Compound substance;
    // Rate at which a compound will be consumed
    public int rate;
    // Current amount of compound stored in tank
    public int capacity;
    // true if tank should be actively selected
    public bool isActive;

	/*
    public Tank(string n, int rt, int cp, bool act)
    {
        name = n;
        rate = rt;
        capacity = cp;
        isActive = act;
    }
    */
	public Tank(Chemical.Compound s, int rt, int cp, bool act)
	{
		substance = s;
		rate = rt;
		capacity = cp;
		isActive = act;
	}

	public Tank(Chemical.Compound s, int rt)
    : this(s, rt, 0, false)
    {}
}

public class GunScript : MonoBehaviour
{
    public const int SHOOT_RATE = 1;

    public int power;
    //never used in GunScript

    public bool isEmitting;

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

//    public string chemToShootName;
    public Chemical.Compound chemToShoot;

    public Chemical.Reaction activeReact;
    public Chemical.Reaction[] reactions;
    private int reactIndex;

    public GameObject shootEffect;
	public ParticleSystem pyro;


	//audio components
	public AudioSource gunAudio;
//	public AudioSource reactLoop;
//	public AudioClip ACreactStart;
//	public AudioClip ACreactLoop;
//	public AudioClip ACreactEnd;
//	public AudioClip ACflameStart;
//	public AudioClip ACflameLoop;
//	public AudioClip ACflameEnd;
//	public AudioClip ACvacuumStart;
//	public AudioClip ACvacuumLoop;
//	public AudioClip ACvacuumEnd;
//	public AudioClip ACvent;
	public bool startReactLoop;
	public bool startVacuumLoop;
	public bool startFlameLoop;
	public bool startShootLoop;
//	public bool endReact;

	public AudioSource[] audios;
		//0		gunAudio
		//1		vac start
		//2		vac loop
		//3		vac end
		//4		react start
		//5		react loop
		//6		react end
		//7		flame start
		//8		flame loop
		//9		flame end
		//10	liquid spray
		//11	gas spray
		//12	vent
		//13	footsteps
		//14	error
		//15


    //used for different particle emitters
    public GameObject absorbEffect;        //for compound 1

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
        isEmitting = false;
        combineCap = 0;
        cursorName = " ";

        reactTanks = new Tank[] {reactTank1, reactTank2, reactTank3};
        prodTanks = new Tank[] {prodTank1, prodTank2, prodTank3};
        allTanks = new Tank[] {reactTank1, reactTank2, reactTank3, prodTank1, prodTank2, prodTank3};

        reactions = this.gameObject.GetComponent<reactionMasterList>().initReactionList();
        reactIndex = 0;
        sprayDamage = 0;
        //absorbEffect = GameObject.Find("AbsorbLiquid");
        //absorbEffect.particleSystem.Play();
//		pyro.particleSystem.Play ();

		startReactLoop = false;
		startFlameLoop = false;
		startVacuumLoop = false;
		startShootLoop = false;

		audios = GetComponents<AudioSource>();
		Debug.Log (audios.Length);
    }
    public void Vent()
    {
        for (int i = 0; i < allTanks.Length; i++)
        {
            allTanks[i].capacity = 0;
//            allTanks[i].name = "";
			allTanks[i].substance = null;
        }
//        chemToShootName = "";
        chemToShoot = null;
        shootEffect = null;

        Chemical.Compound[] components = this.gameObject.GetComponents<Chemical.Compound> ();
        for (int i = 0; i < components.Length; i++)
        {
            Destroy(components[i]);
        }

		//audio
		//gunAudio.PlayOneShot(ACvent);
		audios[12].Play();
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

//        if (selectTank.name != "")
        if (selectTank.substance != null)
        {
            string chemToShootName = selectTank.substance.CompoundName;
            
            chemToShoot = this.gameObject.AddComponent(chemToShootName) as Chemical.Compound;
            chemToShoot.init();
            
            shootEffect = GameObject.Find("ShootGun").GetComponent<GunParticleSwitcher>().setParticleSystem(chemToShoot.state, chemToShoot.color);
            
        }
        else
        {
//            chemToShootName = "";
            chemToShoot = null;
            shootEffect = null;
        }
    }

	//Flamethrowing method
	void ejectFire() {
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

        // React Button
        if (Input.GetKey("r") && activeReact != null)
        {
            bool canReact = true;

            // Check that all needed reactant tanks have the necessary amounts of reactant
            if (activeReact.Reactant1 != null && reactTank1.capacity < activeReact.ReactCoeff1)
                canReact = false;
            if (activeReact.Reactant2 != null && reactTank2.capacity < activeReact.ReactCoeff2)
                canReact = false;
            if (activeReact.Reactant3 != null && reactTank3.capacity < activeReact.ReactCoeff3)
                canReact = false;

            // Check that product tanks can hold more product
//            Debug.Log(prodTank1.capacity + activeReact.ProdCoeff1);
            if (activeReact.Product1 != null && prodTank1.capacity + activeReact.ProdCoeff1 > fullCap)
                canReact = false;
            if (activeReact.Product2 != null && prodTank2.capacity + activeReact.ProdCoeff2 > fullCap)
                canReact = false;
            if (activeReact.Product3 != null && prodTank3.capacity + activeReact.ProdCoeff3 > fullCap)
                canReact = false;

            if (canReact)
            {
				//audio
				if (activeReact.EnergyType == Chemical.Reaction.energy.Combust) {
					//ejectFire();
					pyro.particleSystem.Play();

					if(!startFlameLoop) {
						//					gunAudio.PlayOneShot(ACreactStart);
						audios[7].Play ();
						startFlameLoop = true;
					}

					//gunAudio.PlayOneShot(ACflameLoop);
					//startFlameLoop = true;
					if(!audios[8].isPlaying && !audios[7].isPlaying) {
						audios[8].Play ();
					}
				}



				if(!startReactLoop && !startFlameLoop) {
//					gunAudio.PlayOneShot(ACreactStart);
					audios[4].Play ();
					startReactLoop = true;
				}
				if(startReactLoop && !startFlameLoop) {
					//					reactLoop.Play ();
					//gunAudio.PlayOneShot(ACreactLoop);
					if(!audios[5].isPlaying && !audios[4].isPlaying) {
						audios[5].Play ();
					}
				}
				if(startReactLoop) {
//					reactLoop.Play ();
					//gunAudio.PlayOneShot(ACreactLoop);
					//audios[5].Play ();
				}


                // Consume reactants
                if (activeReact.Reactant1 != null)
                {
                    reactTank1.capacity -= activeReact.ReactCoeff1;
                }
                if (activeReact.Reactant2 != null)
                {
                    reactTank2.capacity -= activeReact.ReactCoeff2;
                }
                if (activeReact.Reactant3 != null)
                {
                    reactTank3.capacity -= activeReact.ReactCoeff3;
                }

//				if (activeReact.EnergyType == Chemical.Reaction.energy.Combust) {
//					//ejectFire();
//					pyro.particleSystem.Play();
//					//gunAudio.PlayOneShot(ACflameLoop);
//					startFlameLoop = true;
//					audios[8].Play ();
//				}


                // Generate products
                if (activeReact.Product1 != null)
                {
                    prodTank1.capacity += activeReact.ProdCoeff1;
                }
                if (activeReact.Product2 != null)
                {
                    prodTank2.capacity += activeReact.ProdCoeff2;
                }
                if (activeReact.Product3 != null)
                {
                    prodTank3.capacity += activeReact.ProdCoeff3;
                }
            }
			else {
				audios[5].Stop ();
				audios[8].Stop ();
				pyro.particleSystem.Stop();
//				reactLoop.Stop ();
				if(startFlameLoop) {
					startFlameLoop = false;
					audios[9].Play ();
				}
				if(startReactLoop) {
					startReactLoop = false;
					//gunAudio.PlayOneShot(ACreactEnd);
					audios[6].Play ();
				}
			}
        }
		else {
			audios[5].Stop ();
			audios[8].Stop ();
			pyro.particleSystem.Stop();
//			reactLoop.Stop ();
//			startReactLoop = false;
//			gunAudio.PlayOneShot(reactEnd);
			if(startFlameLoop) {
				startFlameLoop = false;
				audios[9].Play ();
			}
			if(startReactLoop) {
				startReactLoop = false;
				//gunAudio.PlayOneShot(ACreactEnd);
				audios[6].Play ();
			}
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
//			if(reactTank1.substance == null) {
//
//			}
            if (reactTank1.substance != activeReact.Reactant1)
            {
                Vent();

                if (activeReact.Reactant1 != null) 
					reactTank1.substance = activeReact.Reactant1;
                else reactTank1.substance = null;
                
                if (activeReact.Reactant2 != null) 
					reactTank2.substance = activeReact.Reactant2;
				else reactTank2.substance = null;
                
                if (activeReact.Reactant3 != null) 
					reactTank3.substance = activeReact.Reactant3;
				else reactTank3.substance = null;
                
                if (activeReact.Product1 != null) 
					prodTank1.substance = activeReact.Product1;
				else prodTank1.substance = null;
                
                if (activeReact.Product2 != null)
					prodTank2.substance = activeReact.Product2;
				else prodTank2.substance = null;
                
                if (activeReact.Product3 != null) 
					prodTank3.substance = activeReact.Product3;
				else prodTank3.substance = null;
            }
        }

        //absorbing----------------------------------------------------------------------------
        if (Input.GetMouseButtonDown(1))
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
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank1.substance.Formula)
                        {
                            if (reactTank1.capacity < fullCap)
                            {
                                absorbEffect = this.GetComponent<absorbEffectSwitcher>().switchEffect(reactTank1.substance);
                                absorbEffect.particleSystem.Play();
                                reactTank1.capacity += 2;

								if(!startVacuumLoop) {
									audios[1].Play ();
									startVacuumLoop = true;
								}
								if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
									audios[2].Play ();
                                }
                            }
							else {
								//if(!audios[14].isPlaying) {
									audios[14].Play ();
								//}
							}
                        }
                        if (reactTank2.substance != null)
                        {

                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank2.substance.Formula)
                            {
                                if (reactTank2.capacity < fullCap)
                                {
                                    reactTank2.capacity += 2;
                                    absorbEffect = this.GetComponent<absorbEffectSwitcher>().switchEffect(reactTank2.substance);
                                    absorbEffect.particleSystem.Play();

									if(!startVacuumLoop) {
										audios[1].Play ();
										startVacuumLoop = true;
									}
									if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
										audios[2].Play ();
									}
                                }
								else {
									//if(!audios[14].isPlaying) {
										audios[14].Play ();
									//}
								}
                            }
                        }
						else {
							//if(!audios[14].isPlaying) {
								audios[14].Play ();
							//}
						}
                        if (reactTank3.substance != null)
                        {
                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank3.substance.Formula)
                            {
                                if (reactTank3.capacity < fullCap)
                                {
                                    reactTank3.capacity += 2;

                                    absorbEffect = this.GetComponent<absorbEffectSwitcher>().switchEffect(reactTank1.substance);
                                    absorbEffect.particleSystem.Play();

									if(!startVacuumLoop) {
										audios[1].Play ();
										startVacuumLoop = true;
									}
									if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
										audios[2].Play ();
									}
                                }
								else {
									//if(!audios[14].isPlaying) {
										audios[14].Play ();
									//}
								}
                            }
                        }
						else {
							//if(!audios[14].isPlaying) {
								audios[14].Play ();
							//}
						}
                    }
                    else     //if there is no active reaction, must select tank to fill
                    {
                        Tank[] activeTanks = getActiveTanks();
                        for (int i = 0; i < activeTanks.Length; ++i)
                        {
                            if (activeTanks[i].substance == null)  //can absorb anything into tank1
                            {
                                activeTanks[i].substance = hit2.transform.GetComponent<Chemical.Compound>();

                                activeTanks[i].capacity += 2;

                                absorbEffect = this.GetComponent<absorbEffectSwitcher>().switchEffect(activeTanks[i].substance);
                                absorbEffect.particleSystem.Play();

								if(!startVacuumLoop) {
									audios[1].Play ();
									startVacuumLoop = true;
								}
								if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
									audios[2].Play ();
								}

                                break;
                            }
                        }
                        for (int i = 0; i < activeTanks.Length; ++i)
                        {
                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeTanks[i].substance.Formula)
                            {
                                if (activeTanks[i].capacity < fullCap)
                                {
                                    activeTanks[i].capacity += 2;

                                    absorbEffect = this.GetComponent<absorbEffectSwitcher>().switchEffect(activeTanks[i].substance);
                                    absorbEffect.particleSystem.Play();

									if(!startVacuumLoop) {
										audios[1].Play ();
										startVacuumLoop = true;
									}
									if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
										audios[2].Play ();
									}
                                }
								else {
									//if(!audios[14].isPlaying) {
										audios[14].Play ();
									//}
								}
                                break;
                            }
							else {
								//if(!audios[14].isPlaying) {
									audios[14].Play ();
								//}
							}
                        }
                    }
                }
            }
        }
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
                        if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank1.substance.Formula)
                        {
                            if (reactTank1.capacity < fullCap)
                            {
                                reactTank1.capacity += 2;

								if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
									audios[2].Play ();
								}
                            }
                            else
                            {
                                absorbEffect.particleSystem.Stop();

								audios[2].Stop ();
								if(startVacuumLoop) {
									startVacuumLoop = false;
									audios[3].Play ();
								}

                            }
                        }
                        if (reactTank2.substance != null)
                        {

                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank2.substance.Formula)
                            {
                                if (reactTank2.capacity < fullCap)
                                {
                                    reactTank2.capacity += 2;

									if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
										audios[2].Play ();
									}
                                    
                                }
                                else
                                {
                                    absorbEffect.particleSystem.Stop();

									audios[2].Stop ();
									if(startVacuumLoop) {
										startVacuumLoop = false;
										audios[3].Play ();
									}

                                }
                            }
                        }
                        if (reactTank3.substance != null)
                        {
                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank3.substance.Formula)
                            {
                                if (reactTank3.capacity < fullCap)
                                {
                                    reactTank3.capacity += 2;

									if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
										audios[2].Play ();
									}

                                }
                                else
                                {
                                    absorbEffect.particleSystem.Stop();

									audios[2].Stop ();
									if(startVacuumLoop) {
										startVacuumLoop = false;
										audios[3].Play ();
									}

                                }
                            }
                        }
                    }
                    else     //if there is no active reaction, must select tank to fill
                    {
                        Tank[] activeTanks = getActiveTanks();
                        for (int i = 0; i < activeTanks.Length; ++i)
                        {
                            if (activeTanks[i].substance == null)  //can absorb anything into tank1
                            {
                                activeTanks[i].substance = hit2.transform.GetComponent<Chemical.Compound>();

                                activeTanks[i].capacity += 2;

								if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
									audios[2].Play ();
								}

                                break;
                            }
                        }
                        for (int i = 0; i < activeTanks.Length; ++i)
                        {
                            if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeTanks[i].substance.Formula)
                            {
                                if (activeTanks[i].capacity < fullCap)
                                {
                                    activeTanks[i].capacity += 2;

									if(startVacuumLoop && !audios[2].isPlaying && !audios[1].isPlaying) {
										audios[2].Play ();
									}
                                }
                                else
                                {
                                    absorbEffect.particleSystem.Stop();

									audios[2].Stop ();
									if(startVacuumLoop) {
										startVacuumLoop = false;
										audios[3].Play ();
									}

                                }
                                break;
                            }
                        }
                    }
                }
            }else{
				absorbEffect.particleSystem.Stop();
				
				audios[2].Stop ();
				if(startVacuumLoop) {
					startVacuumLoop = false;
					audios[3].Play ();
				}
			}
        }
        if(Input.GetMouseButtonUp(1))
        {
            absorbEffect.particleSystem.Stop();

			audios[2].Stop ();
			if(startVacuumLoop) {
				startVacuumLoop = false;
				audios[3].Play ();
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

				if(chemToShoot.state == Chemical.Compound.stateOfMatter.liquid) {
					if(!audios[10].isPlaying && !audios[11].isPlaying) {
						audios[10].Play ();
					}
				}
				if(chemToShoot.state == Chemical.Compound.stateOfMatter.gas) {
					if(!audios[11].isPlaying && !audios[10].isPlaying) {
						audios[11].Play ();
					}
				}

				Debug.Log("shooting effect: " + shootEffect.name);
            }
			else {
				//if(!audios[14].isPlaying) {
					audios[14].Play ();
				//}
			}
        }
        if (Input.GetButton("Fire1"))
        {
            // Look for active tanks that are not empty and fire those
            Tank[] activeNonemptyTanks = getActiveNonemptyTanks();
//            Debug.Log(string.Format("Active Tanks: {0}", getActiveTanks().Length));
//            Debug.Log(string.Format("Active Nonempty Tanks: {0}", activeNonemptyTanks.Length));
            if (isEmitting && activeNonemptyTanks.Length > 0)
            {
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
                isEmitting = false;
                if(shootEffect != null)
                {
                    shootEffect.particleSystem.Stop();
					audios[10].Stop ();
					audios[11].Stop ();
                }
            }
        }
        if (Input.GetButtonUp("Fire1")) //stop emitting
        {
            if (isEmitting)
            {
                isEmitting = false;
                shootEffect.particleSystem.Stop();
				audios[10].Stop ();
				audios[11].Stop ();

                Tank[] nonemptyTanks = Array.FindAll(allTanks, x => x.capacity > 0);
                if (nonemptyTanks.Length == 0)
                {
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