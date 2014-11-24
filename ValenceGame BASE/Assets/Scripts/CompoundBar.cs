using UnityEngine;
using System.Collections;

public class CompoundBar : MonoBehaviour
{

        // 0.0 to 1.0, determines proportional fill of bar
        public float offset;

        // 0 to 360, determines angle at which bar starts
        public float startAngle;

        // 0 to 360, determines angle at which bar ends
        public float endAngle;

        private float totalAngle;

        // Determine orientation of compound bar based on initial parameters
        void Start ()
        {
            totalAngle = endAngle - startAngle;

            // If angle is backwards, reflect bar so it fills the other way
            Debug.Log(string.Format("Scale before: {0}", gameObject.transform.localScale));
            if (totalAngle < 0f)
            {
                gameObject.transform.localScale = new Vector3(
                    gameObject.transform.localScale.x,
                    gameObject.transform.localScale.y * -1.0f,
                    gameObject.transform.localScale.z);

                Debug.Log(string.Format("Scale after: {0}", gameObject.transform.localScale));
            }

            // Rotate bar so it fills at the right angle
            if(totalAngle < 0f)
            {
                gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, startAngle - 180f));
            }
            else
            {
                gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, startAngle));
            }
        }
    
        // Update is called once per frame
        void Update ()
        {

            float revealOffset = 1f - (Mathf.Abs(totalAngle) / 360f) * ((float)(Time.timeSinceLevelLoad % 3) / 3f);
            gameObject.renderer.material.SetFloat("_Cutoff", revealOffset);
        }
}

