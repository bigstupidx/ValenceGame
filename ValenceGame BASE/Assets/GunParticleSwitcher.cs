using UnityEngine;
using System.Collections;

public class GunParticleSwitcher : MonoBehaviour {
    private GameObject particleSystem;

    public GameObject setParticleSystem(GameObject parSys)
    {
        particleSystem = parSys;

        Instantiate(particleSystem, new Vector3(0, 0, 0), Quaternion.identity);

        return particleSystem;
    }
}
