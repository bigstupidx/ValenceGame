using UnityEngine;
using System.Collections;

public class myGUI : MonoBehaviour
{
	
	public Texture2D cursorPic;
	
	public float length1 = 0;
	public float length2 = 0;
	
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
	public Material mat; 
	public GameObject player;
	
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

        length1 = (float)player.GetComponent<GunScript>().tank1Cap * (((float)Screen.height - 100) / (float)fullCap); //is relative to screen size
        length2 = (float)player.GetComponent<GunScript>().tank2Cap * (((float)Screen.height - 100) / (float)fullCap);

        GUI.skin = mySkin;

        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), cursorPic);
        if (player.GetComponent<GunScript>().eqBalanced)
        //if (player.GetComponent<GunScript>().balanced || !player.GetComponent<GunScript>().isEquation)
        {
            GUI.Box(new Rect(10, (Screen.height - 10) - length1, 30, length1), "", "redbar");
            GUI.Box(new Rect(50, (Screen.height - (30 * player.GetComponent<GunScript>().tank1Rate)), 30, (30 * player.GetComponent<GunScript>().tank1Rate) - 10), "", "whitebar");
            GUI.Box(new Rect(110, (Screen.height - 10) - length2, 30, length2), "", "bluebar");
            GUI.Box(new Rect(160, (Screen.height - (30 * player.GetComponent<GunScript>().tank2Rate)), 30, (30 * player.GetComponent<GunScript>().tank2Rate) - 10), "", "whitebar");

            GUI.Box(new Rect(10, 10, 50, 50), "   H2O", "whitebar");
        }

        GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 - 60, 50, 50), player.GetComponent<GunScript>().cursorName, guiStyle);

        GUI.matrix = m;
        */
		ratH = (float)Screen.height / (float)relScreenH;
		ratW = (float)Screen.width / (float)relScreenW;
		
		length1 = (float)player.GetComponent<GunScript>().tank1Cap * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap); //is relative to screen size
		length2 = (float)player.GetComponent<GunScript>().tank2Cap * (((float)Screen.height - (100 * (float)ratH)) / (float)fullCap);
		string tank1Name = player.GetComponent<GunScript> ().tank1Name;
		GUI.skin = mySkin;
		
		GUI.Label(new Rect(Screen.width / 2 - (50 * ratW), Screen.height / 2 - (50 * ratH), 100 * ratW, 100 * ratH), cursorPic);
	
        //Do need to show GUI all the time
		//if (player.GetComponent<GunScript>().eqBalanced)
			//if (player.GetComponent<GunScript>().balanced || !player.GetComponent<GunScript>().isEquation)
		//{
			
			//First bar
			Rect bar1 = new Rect(10 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
			Rect box = new Rect(10 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+length1/3));
			GUI.color = Color.blue;
			GUI.DrawTexture(box, fillTexture);
			GUI.color = Color.white;
			GUI.DrawTexture(bar1, texture);
			
			//Second bar
			Rect bar2 = new Rect(40 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
			box = new Rect(40 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+length2/3));
			GUI.color = Color.green;
			GUI.DrawTexture(box, fillTexture);
			GUI.color = Color.white;
			GUI.DrawTexture(bar2, texture);
			
			//Third bar
			Rect bar3 = new Rect(70 * ratW, (Screen.height) - 130*ratH, 20 * ratW, 100*ratH);
			//box = new Rect(70 * ratW, (Screen.height) - 30*ratH, 20 * ratW, 100*ratH-(100*ratH+length2/3));
			GUI.color = Color.green;
			//GUI.DrawTexture(box, fillTexture);
			GUI.color = Color.white;
			GUI.DrawTexture(bar3, texture);

			//Selected Reaction
			mySkin.font = font;
			GUI.Box(new Rect(10 * ratW, (Screen.height) - 160*ratH, 80 * ratW, 30*ratH), productName);
			
			
			//GUI.Box(new Rect(50 * ratW, (Screen.height - (30 * ratH * player.GetComponent<GunScript>().tank1Rate)), 30 * ratW, (30 * ratH * player.GetComponent<GunScript>().tank1Rate) - (10 * ratH)), "");

			
			//GUI.Box(new Rect(50 * ratW, (Screen.height - (30 * ratH * player.GetComponent<GunScript>().tank1Rate)), 30 * ratW, (30 * ratH * player.GetComponent<GunScript>().tank1Rate) - (10 * ratH)), "", "whitebar");
			//GUI.Box(new Rect(50 * ratW, (Screen.height - (10 * ratH)) - length2, 30 * ratW, length2), "", "bluebar");
			//GUI.Box(new Rect(160 * ratW, (Screen.height - (30 * ratH * player.GetComponent<GunScript>().tank2Rate)), 30 * ratW, (30 * ratH * player.GetComponent<GunScript>().tank2Rate) - (10 * ratH)), "", "whitebar");
			
			GUI.color = Color.black;
			//Element/Compound names
			GUI.Button(new Rect(10 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), player.GetComponent<GunScript> ().tank1Name);
			//GUI.Button(new Rect(50 * ratW, (Screen.height - 30*ratH), 30 * ratW, 20*ratH), player.GetComponent<GunScript> ().tank1Name);
			GUI.Button(new Rect(40 * ratW, (Screen.height - 30*ratH), 20 * ratW, 20*ratH), player.GetComponent<GunScript> ().tank2Name);
			
			//GUI.Box(new Rect(10 * ratW, 10 * ratH, 50 * ratW, 50 * ratH), "   H2O", "whitebar");
			//GUI.Button(new Rect(10 * ratW, (Screen.height - (10 * ratH)) - 180, 50 * ratW, 50 * ratH), "   H2O");
		//}
		
		GUI.Label(new Rect(Screen.width / 2 - (20 * ratW), Screen.height / 2 - (60 * ratH), 50 * ratW, 50 * ratH), player.GetComponent<GunScript>().cursorName, guiStyle);		
		
	}
	
}
