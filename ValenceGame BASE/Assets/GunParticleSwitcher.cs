using UnityEngine;
using System.Collections;

public class GunParticleSwitcher : MonoBehaviour {
    public GameObject particleSystem;

    public void Start()
    {
        particleSystem = GameObject.Find("End");
    }

    public GameObject setParticleSystem(Chemical.Compound.stateOfMatter state)
    {
        if (state == Chemical.Compound.stateOfMatter.liquid)
        {
            return GameObject.Find("LiquidParticles");

        }
        else if (state == Chemical.Compound.stateOfMatter.gas)
        {
            return GameObject.Find("GasParticles");

        }
        else if (state == Chemical.Compound.stateOfMatter.solid)
        {
            return particleSystem;

        }
        else
        {
            return particleSystem;

        }
        /*switch (state)
        {
            case Chemical.Compound.stateOfMatter.gas:
                return GameObject.Find("GasParticles");
            case Chemical.Compound.stateOfMatter.liquid:
                return GameObject.Find("LiquidParticles");
            case Chemical.Compound.stateOfMatter.solid:
                return particleSystem;
            default:
                return particleSystem;
        }*/
        //return GameObject.Find("LiquidParticles");
    }
}
