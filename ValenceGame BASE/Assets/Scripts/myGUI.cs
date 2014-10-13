using UnityEngine;
using System.Collections;

public class myGUI : MonoBehaviour {

	public Texture2D cursorPic;
	int length1 = 0;
    int length2 = 0;
	public GUISkin mySkin;

    public GameObject player;

    private GUIStyle guiStyle;

	void Start()
	{
		cursorPic = (Texture2D)Resources.Load("cursor");
        guiStyle = new GUIStyle();
        guiStyle.richText = true;
		guiStyle.fontSize = 24;
		guiStyle.normal.textColor = Color.white;
	}

	void OnGUI () {
		length1 = player.GetComponent<GunScript>().tank1Cap;
		length2 = player.GetComponent<GunScript>().tank2Cap;

		GUI.skin = mySkin;

		GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-50,100,100), cursorPic);
        if (player.GetComponent<GunScript>().eqBalanced)
        {
            GUI.Box(new Rect(10, (Screen.height - 10) - length1, 30, length1), "", "redbar");
			GUI.Box(new Rect(50, (Screen.height - (30 * player.GetComponent<GunScript>().tank1Rate)), 30, (30 * player.GetComponent<GunScript>().tank1Rate) - 10), "", "whitebar");
            GUI.Box(new Rect(110, (Screen.height - 10) - length2, 30, length2), "", "bluebar");
			GUI.Box(new Rect(160, (Screen.height - (30 * player.GetComponent<GunScript>().tank2Rate)), 30, (30 * player.GetComponent<GunScript>().tank2Rate) - 10), "", "whitebar");

            GUI.Box(new Rect(10, 10, 50, 50), "   H2O", "whitebar");
        }

        GUI.Label(new Rect(Screen.width/2 - 20, Screen.height/2-60, 50, 50), player.GetComponent<GunScript>().cursorName, guiStyle);

	}
}
