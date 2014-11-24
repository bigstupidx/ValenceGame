using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour
{

    public Material reactFillbar1;
    public Texture2D texture1;

    public Texture2D cursorPic;
    
    public float reactLength1 = 0;
    public float reactLength2 = 0;
    public float reactLength3 = 0;
    public float prodLength1 = 0;
    public float prodLength2 = 0;
    public float prodLength3 = 0;

    public float length;
    
    private int fullCap;
    
    const int relScreenW = 640; //relative values for screen size
    const int relScreenH = 400;
    
    private Vector3 scale;
    public float ratW;
    public float ratH;
    
    public GUISkin mySkin;
    public Font font;
    public Texture texture;
    public Texture fillTexture;
    public Texture highlightTexture;
    public Material mat; 
    public GameObject player;
    public Chemical.Reaction activeReaction;
    
    private GUIStyle guiStyle;
    
    public string productName;

    void Start()
    {
        cursorPic = (Texture2D)Resources.Load("cursor");
        guiStyle = new GUIStyle();
        guiStyle.richText = true;
        guiStyle.fontSize = 24;     //how to change wrt width/height?
        guiStyle.normal.textColor = Color.white;
        
        fullCap = player.GetComponent<GunScript>().getFullCap();
    
        productName = "";
    }
    
    void OnGUI()
    {

        /*scale.y = (float)Screen.height / (float)relScreenH;
        scale.x = scale.y; 
        scale.z = 1;

        float scaleX = (float)Screen.width / (float)relScreenW;
        Matrix4x4 m = GUI.matrix;

        GUI.matrix = Matrix4x4.TRS(new Vector3((scaleX-scale.y)/2 * relScreenW, 0, 0), Quaternion.identity, scale);

        reactLength1 = (float)player.GetComponent<GunScript>().tank1Cap * (((float)Screen.height - 100) / (float)fullCap); //is relative to screen size
        reactLength2 = (float)player.GetComponent<GunScript>().tank2Cap * (((float)Screen.height - 100) / (float)fullCap);

        GUI.skin = mySkin;

        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), cursorPic);
        if (player.GetComponent<GunScript>().eqBalanced)
        //if (player.GetComponent<GunScript>().balanced || !player.GetComponent<GunScript>().isEquation)
        {
            GUI.Box(new Rect(10, (Screen.height - 10) - reactLength1, 30, reactLength1), "", "redbar");
            GUI.Box(new Rect(50, (Screen.height - (30 * player.GetComponent<GunScript>().tank1Rate)), 30, (30 * player.GetComponent<GunScript>().tank1Rate) - 10), "", "whitebar");
            GUI.Box(new Rect(110, (Screen.height - 10) - reactLength2, 30, reactLength2), "", "bluebar");
            GUI.Box(new Rect(160, (Screen.height - (30 * player.GetComponent<GunScript>().tank2Rate)), 30, (30 * player.GetComponent<GunScript>().tank2Rate) - 10), "", "whitebar");

            GUI.Box(new Rect(10, 10, 50, 50), "   H2O", "whitebar");
        }

        GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 - 60, 50, 50), player.GetComponent<GunScript>().cursorName, guiStyle);

        GUI.matrix = m;
        */

        activeReaction = player.GetComponent<GunScript>().activeReact;
        if(activeReaction != null)
        {
            List<Chemical.Compound> activeReactants = activeReaction.Reactants;
            List<Chemical.Compound> activeProducts = activeReaction.Products;
        }

        ratH = (float)Screen.height / (float)relScreenH;
        ratW = (float)Screen.width / (float)relScreenW;

        if (Event.current.type.Equals(EventType.Repaint))
        {
            Rect temp = new Rect(10 * ratW, (Screen.height) - 130 * ratH, 200 * ratW, 30 * ratH);
            Rect temp2 = new Rect(10 * ratW, (Screen.height) - 130 * ratH, 200 * ratW, 30 * ratH);
            Rect pos = new Rect(10 * ratW, (Screen.height) - 130 * ratH, 200, 200);
            Graphics.DrawTexture(temp2, texture1, reactFillbar1);
        }
        
        reactLength1 = (float)player.GetComponent<GunScript>().reactTank1.capacity * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap); //is relative to screen size
        reactLength2 = (float)player.GetComponent<GunScript>().reactTank2.capacity * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap);
        reactLength3 = (float)player.GetComponent<GunScript>().reactTank3.capacity * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap);
        prodLength1 = (float)player.GetComponent<GunScript>().prodTank1.capacity * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap);
        prodLength2 = (float)player.GetComponent<GunScript>().prodTank2.capacity * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap);
        prodLength3 = (float)player.GetComponent<GunScript>().prodTank3.capacity * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap);
        Tank reactTank1 = player.GetComponent<GunScript> ().reactTank1;
        Tank reactTank2 = player.GetComponent<GunScript> ().reactTank2;
        Tank reactTank3 = player.GetComponent<GunScript> ().reactTank3;
        Tank prodTank1 = player.GetComponent<GunScript> ().prodTank1;
        Tank prodTank2 = player.GetComponent<GunScript> ().prodTank2;
        Tank prodTank3 = player.GetComponent<GunScript> ().prodTank3;



        GUI.skin = mySkin;
        
        GUI.Label(new Rect(Screen.width / 2 - (50 * ratW), Screen.height / 2 - (50 * ratH), 100 * ratW, 100 * ratH), cursorPic);
    
        //Do need to show GUI all the time
        //if (player.GetComponent<GunScript>().eqBalanced)
            //if (player.GetComponent<GunScript>().balanced || !player.GetComponent<GunScript>().isEquation)
        //{

        // REACTANT BARS
            
            //First reactant bar
            Rect reactBar1 = new Rect(10 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH - 75);
            Rect box = new Rect(10 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+reactLength1/3));
            GUI.color = Color.blue;
            GUI.DrawTexture(box, fillTexture);
            GUI.color = Color.white;
            GUI.DrawTexture(reactBar1, texture);
            if (reactTank1.isActive)
            {    
                GUI.color = Color.yellow;
                GUI.DrawTexture(reactBar1, highlightTexture);
            }
            
            
            //Second reactant bar
            Rect reactBar2 = new Rect(40 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
            box = new Rect(40 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+reactLength2/3));
            GUI.color = Color.green;
            GUI.DrawTexture(box, fillTexture);
            GUI.color = Color.white;
            GUI.DrawTexture(reactBar2, texture);
            if (reactTank2.isActive)
            {    
                GUI.color = Color.yellow;
                GUI.DrawTexture(reactBar2, highlightTexture);
            }
        
            //Third reactant bar
            Rect reactBar3 = new Rect(70 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
            box = new Rect(70 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+reactLength3/3));
            GUI.color = Color.green;
            GUI.DrawTexture(box, fillTexture);
            GUI.color = Color.white;
            GUI.DrawTexture(reactBar3, texture);
            if (reactTank3.isActive)
            {    
                GUI.color = Color.yellow;
                GUI.DrawTexture(reactBar3, highlightTexture);
            }


        // PRODUCT BARS

            //First product bar
            Rect prodBar1 = new Rect(110 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
            box = new Rect(110 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+prodLength1/3));
            GUI.color = Color.blue;
            GUI.DrawTexture(box, fillTexture);
            GUI.color = Color.white;
            GUI.DrawTexture(prodBar1, texture);
            if (prodTank1.isActive)
            {    
                GUI.color = Color.yellow;
                GUI.DrawTexture(prodBar1, highlightTexture);
            }
            
            
            //Second product bar
            Rect prodBar2 = new Rect(140 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
            box = new Rect(140 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+prodLength2/3));
            GUI.color = Color.green;
            GUI.DrawTexture(box, fillTexture);
            GUI.color = Color.white;
            GUI.DrawTexture(prodBar2, texture);
            if (prodTank2.isActive)
            {    
                GUI.color = Color.yellow;
                GUI.DrawTexture(prodBar2, highlightTexture);
            }
        
            //Third product bar
            Rect prodBar3 = new Rect(170 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
            box = new Rect(170 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+prodLength3/3));
            GUI.color = Color.green;
            GUI.DrawTexture(box, fillTexture);
            GUI.color = Color.white;
            GUI.DrawTexture(prodBar3, texture);
            if (prodTank3.isActive)
            {    
                GUI.color = Color.yellow;
                GUI.DrawTexture(prodBar3, highlightTexture);
            }
        //Selected Reaction
            mySkin.font = font;
            GUI.Box(new Rect(10 * ratW, (Screen.height) - 160*ratH, 80 * ratW, 30*ratH), productName);
            
            
            //GUI.Box(new Rect(50 * ratW, (Screen.height - (30 * ratH * player.GetComponent<GunScript>().tank1Rate)), 30 * ratW, (30 * ratH * player.GetComponent<GunScript>().tank1Rate) - (10 * ratH)), "");

            
            //GUI.Box(new Rect(50 * ratW, (Screen.height - (30 * ratH * player.GetComponent<GunScript>().tank1Rate)), 30 * ratW, (30 * ratH * player.GetComponent<GunScript>().tank1Rate) - (10 * ratH)), "", "whitebar");
            //GUI.Box(new Rect(50 * ratW, (Screen.height - (10 * ratH)) - reactLength2, 30 * ratW, reactLength2), "", "bluebar");
            //GUI.Box(new Rect(160 * ratW, (Screen.height - (30 * ratH * player.GetComponent<GunScript>().tank2Rate)), 30 * ratW, (30 * ratH * player.GetComponent<GunScript>().tank2Rate) - (10 * ratH)), "", "whitebar");
            
            GUI.color = Color.black;
            //Element/Compound names
            //GUI.Button(new Rect(50 * ratW, (Screen.height - 30*ratH), 30 * ratW, 20*ratH), player.GetComponent<GunScript> ().tank1Name);

            // Reactant names

		string react1name = "";
		string react2name = "";
		string react3name = "";
		string prod1name = "";
		string prod2name = "";
		string prod3name = "";

		if(reactTank1.substance != null) {
			react1name = reactTank1.substance.Formula;
		}
		if(reactTank2.substance != null) {
			react2name = reactTank2.substance.Formula;
		}
		if(reactTank3.substance != null) {
			react3name = reactTank3.substance.Formula;
		}
		if(prodTank1.substance != null) {
			prod1name = prodTank1.substance.Formula;
		}
		if(prodTank2.substance != null) {
			prod2name = prodTank2.substance.Formula;
		}
		if(prodTank3.substance != null) {
			prod3name = prodTank3.substance.Formula;
		}

        GUI.Button(new Rect(10 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), react1name);
        GUI.Button(new Rect(40 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), react2name);
        GUI.Button(new Rect(70 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), react3name);

            // Product names
        GUI.Button(new Rect(110 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), prod1name);
        GUI.Button(new Rect(140 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), prod2name);
        GUI.Button(new Rect(170 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), prod3name);
			
			//GUI.Box(new Rect(10 * ratW, 10 * ratH, 50 * ratW, 50 * ratH), "   H2O", "whitebar");
			//GUI.Button(new Rect(10 * ratW, (Screen.height - (10 * ratH)) - 180, 50 * ratW, 50 * ratH), "   H2O");
		//}
		
        GUI.Label(new Rect(Screen.width / 2 - (20 * ratW), Screen.height / 2 - (60 * ratH), 50 * ratW, 50 * ratH), player.GetComponent<GunScript>().cursorName, guiStyle);



	}

    void Update()
    {



        length = 1.0f-(float)player.GetComponent<GunScript>().reactTank1.capacity/400f;
        if (length == 0) {
            length = 0.01f;
        }
        reactFillbar1.SetFloat("_Cutoff", length);
    }
}