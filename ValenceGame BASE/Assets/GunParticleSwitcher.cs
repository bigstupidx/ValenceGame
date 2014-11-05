using UnityEngine;
using System.Collections;

public class GunParticleSwitcher : MonoBehaviour {
    public GameObject particleSystem;
    public Color c;

    public void Start()
    {
        particleSystem = GameObject.Find("End");
    }

    public GameObject setParticleSystem(Chemical.Compound.stateOfMatter state, Color color)
    {
        if (state == Chemical.Compound.stateOfMatter.liquid)
        {
            GameObject.Find("LiquidParticles").GetComponent<ParticleSystem>().startColor = color;

            return GameObject.Find("LiquidParticles");

        }
        else if (state == Chemical.Compound.stateOfMatter.gas)
        {
            GameObject.Find("GasParticles").GetComponent<ParticleSystem>().startColor = color;

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
