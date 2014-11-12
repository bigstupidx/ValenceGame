using UnityEngine;
using System.Collections;

public class FireOut : MonoBehaviour {
    
    public GameObject effect;

    public int health;

    void Update()
    {
        if (health <= 0) Destroy(this.gameObject);

        
    }

    void OnParticleCollision(GameObject other){
        
        string firingsub = GameObject.Find ("Player").GetComponent<GunScript>().chemToShoot.Formula;

        if(firingsub == "O2") {
//          Vector3 shrink = new Vector3(0.9, 0.9, 0.9);
//          Vector3 aPosition = new Vector3(1, 1, 1);
//          this.gameObject.transform.localScale = Vector3.Scale(this.gameObject.transform.localScale, new Vector3(0.99F, 0.99F, 0.99F));
//          Debug.Log (Time.deltaTime);
            this.gameObject.transform.localScale = Vector3.Scale(this.gameObject.transform.localScale, new Vector3((1F + Time.deltaTime), (1F + Time.deltaTime), (1F + Time.deltaTime)));
            
//            if(this.gameObject.transform.localScale.x < 0.25F) {
//                this.gameObject.gameObject.SetActive(false);
//            }
        }
        
        if(firingsub == "H2O") {
//          Vector3 shrink = new Vector3(0.9, 0.9, 0.9);
//          Vector3 aPosition = new Vector3(1, 1, 1);
//          this.gameObject.transform.localScale = Vector3.Scale(this.gameObject.transform.localScale, new Vector3(0.99F, 0.99F, 0.99F));
//            Debug.Log (Time.deltaTime);
//            this.gameObject.transform.localScale = Vector3.Scale(this.gameObject.transform.localScale, new Vector3((0.90F - Time.deltaTime), (0.90F - Time.deltaTime), (0.90F - Time.deltaTime)));
                        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z * (1F - Time.deltaTime));
            
            if(this.gameObject.transform.localScale.z < 0.2F) {
                Destroy(this.gameObject.gameObject);
            }
            //          Vector3 scale = other.transform.localScale;
            //          other.transform.localScale.x = scale.x - 0.1F;
            //          other.transform.localScale.y = scale.y - 0.1F;
            //          other.transform.localScale.z = scale.z - 0.1F;
        }
        
//        string firingsub = GameObject.Find ("Player").GetComponent<GunScript>().chemToShoot.Formula;
    }

    void OnDestroy()
    {
        /*Instantiate(effect, new Vector3(this.transform.position.x, this.transform.position.y - (float)4.5, this.transform.position.z), Quaternion.identity);

        Destroy(effect, 20);*/
    }
}
