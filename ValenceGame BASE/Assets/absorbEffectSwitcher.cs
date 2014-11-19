using UnityEngine;
using System.Collections;

public class absorbEffectSwitcher : MonoBehaviour {
    public GameObject particleSystem;

    public GameObject switchEffect(Chemical.Compound compound)
    {
        if (compound.state == Chemical.Compound.stateOfMatter.liquid)
        {
            GameObject.Find("AbsorbLiquid").GetComponent<ParticleSystem>().startColor = compound.color;

            return GameObject.Find("AbsorbLiquid");

        }
        else if (compound.state == Chemical.Compound.stateOfMatter.gas)
        {
            GameObject.Find("AbsorbGas").GetComponent<ParticleSystem>().startColor = compound.color;

            return GameObject.Find("AbsorbGas");


        }
        else if (compound.state == Chemical.Compound.stateOfMatter.solid)
        {
            return particleSystem;

        }
        else
        {
            return particleSystem;

        }
    }

	// Use this for initialization
	void Start () {
        particleSystem = GameObject.Find("AbsorbLiquid");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
