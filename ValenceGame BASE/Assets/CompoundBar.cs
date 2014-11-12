using UnityEngine;
using System.Collections;

public class CompoundBar : MonoBehaviour
{

        // Use this for initialization
        void Start ()
        {
    
        }
    
        // Update is called once per frame
        void Update ()
        {
            float revealOffset = 1f - (float)(Time.timeSinceLevelLoad % 10) / 10.1F;
            gameObject.renderer.material.SetFloat("_Cutoff", revealOffset);//Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x));
        }
}

