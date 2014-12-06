using UnityEngine;
using UnityEngine.UI;
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
    private Chemical.Reaction guiReaction;
    
    public string productName;

    void Start()
    {
        cursorPic = (Texture2D)Resources.Load("cursor");
        guiStyle = new GUIStyle();
        guiStyle.richText = true;
        guiStyle.fontSize = 24;     //how to change wrt width/height?
        guiStyle.normal.textColor = Color.white;
        guiReaction = null;
        
        fullCap = player.GetComponent<GunScript>().getFullCap();
    
        productName = "";
    }

    /*
    // Changes GUI to display new reaction
    // Triggered when active reaction changes
    void SetGUIReaction(Chemical.Reaction reaction)
    {
        // Change Informal Reaction Text
        Text informalReactText = GameObject.Find("InformalReactionText").GetComponent<Text>();
        informalReactText.text = "Found it!";
        if (reaction != null)
        {
        }
        else
        {
        }

        // Formal Reaction Text
        Text formalReactText = GameObject.Find("FormalReactionText").GetComponent<Text>();
        formalReactText.text = "Found it!";
    }
    */
    
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

        Chemical.Reaction activeReaction = player.GetComponent<GunScript>().activeReact;
        /*
        if(activeReaction != guiReaction)
        {
            guiReaction = activeReaction;
            SetGUIReaction(activeReaction);
        }
        */
        
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
    
		
        GUI.Label(new Rect(Screen.width / 2 - (20 * ratW), Screen.height / 2 - (60 * ratH), 50 * ratW, 50 * ratH), player.GetComponent<GunScript>().cursorName, guiStyle);



	}

}