using UnityEngine;
using System;
using System.Collections;

public class NoteBalance : MonoBehaviour {

    /* This script was made with the following assumptions:
     * 1. The player finds the note and clicks it
     * 2. An image of the note pops up that the player can read
     * 3. After reading the note, the player clicks a button to "balance equation"
     * 4. A GUI pops up, over the note, that allows you to balance the equation
     */

    public bool activateNote = false; // GUI for displaying note
    public bool activateBalancingPanel = false; // GUI for equation balancing
    public bool balanceAttempted = false;
	public bool changeColor = false;
    public bool balanceSuccessful = false; // Activates when equation is successfully balanced
    public bool noteFinished = false;
    public int h2Count;
    public int o2Count;
    public int h2oCount;
	public bool isNear = false;

    private GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
        h2Count = 1;
        o2Count = 1;
        h2oCount = 1;
        guiStyle = new GUIStyle();
        guiStyle.richText = true;
	}
	
	// Update is called once per frame
	void Update () {

        // Disable note once equation balance is finished
        if (noteFinished)
        {
            enabled = false;
            //renderer.enabled = false;
        }

		float distance = 2.0f;
		GameObject player = GameObject.Find ("Player");
		GameObject note = GameObject.Find ("Note");
		if ((player.transform.position - note.transform.position).magnitude < distance) {
			isNear = true;
		} else {
			isNear = false;
		}
        // If player is close enough to note and button is pressed, display equation balancing GUI
        if (Input.GetKeyDown(KeyCode.E))
        {        
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1.5f);
		
            if (hit.transform != null && hit.transform.tag == "Note")
            {
                activateNote = true;
            }
        }
    }

    void OnGUI()
    {
        // Trigger prompt text when close to note
		if (isNear) {
			showText();
		}

        // Display GUI elements for note
		if (activateNote)
        {
            displayNote();

            // GUI elements for equation balancing panel
            if (activateBalancingPanel)
            {
                displayBalancingPanel();
            }
        }
    }

    void displayNote()
    {
        Time.timeScale = 0.0f;
        Camera.main.GetComponent<MouseLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<MouseLook>().enabled = false;
        Screen.showCursor = true;

        // Make GUI box for the note
        GUI.Box(new Rect(20, 20, Screen.width - 40, Screen.height - 40), "Note");

        // Make button for balancing equation. If pressed, open equation panel
        if (GUI.Button(new Rect(40, 40, 80, 20), "Balance"))
        {
            activateBalancingPanel = true;
        }
    }

    void displayBalancingPanel()
    {
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balancing Panel");

        // GUI elements for H2
        Vector2 h2Pos = new Vector2(160, 160);

		//Declare coefficients 
		string h2CountString = String.Format ("<color=white><size=60>{0}</size></color>", h2Count).ToString ();
		string o2CountString = String.Format ("<color=white><size=60>{0}</size></color>", o2Count).ToString ();
		string h2oCountString = String.Format("<color=white><size=60>{0}</size></color>", h2oCount).ToString();

		//Change color of coefficients 
		if (balanceAttempted && h2Count != o2Count * 2 && changeColor) {
			h2CountString = String.Format ("<color=red><size=60>{0}</size></color>", h2Count).ToString ();
			o2CountString = String.Format ("<color=red><size=60>{0}</size></color>", o2Count).ToString ();

		} 
		if (balanceAttempted && h2Count == o2Count * 2 && changeColor) {
			h2CountString = String.Format ("<color=green><size=60>{0}</size></color>", h2Count).ToString ();
			o2CountString = String.Format ("<color=green><size=60>{0}</size></color>", o2Count).ToString ();
		}

		if (balanceAttempted && (h2oCount != o2Count * 2) && changeColor) {
			h2oCountString = String.Format ("<color=red><size=60>{0}</size></color>", h2oCount).ToString ();
		} 
		if (balanceAttempted && h2oCount == o2Count * 2 && changeColor) {
			h2oCountString = String.Format ("<color=green><size=60>{0}</size></color>", h2oCount).ToString ();
			o2CountString = String.Format ("<color=green><size=60>{0}</size></color>", o2Count).ToString ();
		}


        GUI.Label(new Rect(h2Pos.x, h2Pos.y, 100, 100), "<color=white><size=60>H<size=30>2</size> +</size></color>", guiStyle);
        GUI.Box(new Rect(h2Pos.x - 50, h2Pos.y, 40, 60), h2CountString, guiStyle);
        addButtons(h2Pos.x, h2Pos.y, ref h2Count);

        // GUI elements for O2
        Vector2 o2Pos = new Vector2(340, 160);			
				

        GUI.Label(new Rect(o2Pos.x, o2Pos.y, 100, 100), "<color=white><size=60>O<size=30>2</size></size></color>", guiStyle);
        GUI.Box(new Rect(o2Pos.x - 50, o2Pos.y, 40, 60), o2CountString, guiStyle);
        addButtons(o2Pos.x, o2Pos.y, ref o2Count);

        // GUI element for arrow
        GUI.Label(new Rect(o2Pos.x + 110, o2Pos.y-30, 200, 200), "<color=white><size=100>→</size></color>", guiStyle);

        // GUI elements for H2O
        Vector2 h2oPos = new Vector2(640, 160);        
        GUI.Label(new Rect(h2oPos.x, h2oPos.y, 100, 100), "<color=white><size=60>H<size=30>2</size>O</size></color>", guiStyle);
        GUI.Box(new Rect(h2oPos.x - 50, h2oPos.y, 40, 60), h2oCountString, guiStyle);
        addButtons(h2oPos.x, h2oPos.y, ref h2oCount);


        // Make button to attempt to balance the equation
        if (balanceSuccessful)
            GUI.enabled = false;
        if (GUI.Button(new Rect(100, Screen.height - 100, 80, 20), "Try"))
        {

			changeColor = true;
			
            balanceAttempted = true;
            Vector2 feedbackPos = new Vector2();
            // Test if equation is properly balanced
            if (h2Count == o2Count * 2 && h2oCount == o2Count * 2)
            {
                balanceSuccessful = true;
                GameObject.Find("Player").GetComponent<GunScript>().balanced = true;
            }
            else
                balanceSuccessful = false;
        }
        GUI.enabled = true;

        // Make feedback panel to communicate whether balance was successful
        if (balanceAttempted)
        {
            string labeltext = "";
            if (balanceSuccessful == true)
            {
                labeltext = "<color=white>Equation successfully balanced! You can now create water.</color>";
            }
            else
            {
                labeltext = "<color=white>Balance unsuccessful- try again.</color>";
            }
            GUI.Label(new Rect(100, Screen.height - 160, 300, 20), labeltext, guiStyle);
        }

        // Make exit button for player to click and exit
		if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 100, 80, 20), "Exit"))
        {
            // cleanup before exit
            Time.timeScale = 1.0f;
            Screen.showCursor = false;
            activateBalancingPanel = false;
            activateNote = false;
            Camera.main.GetComponent<MouseLook>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<MouseLook>().enabled = true;
            noteFinished = true;
        }

    }

    void addButtons(float xPos, float yPos, ref int count)
    {
        if (GUI.Button(new Rect(xPos - 58, yPos - 50, 40, 60), "<color=white><size=50>▲</size></color>", guiStyle) && !balanceSuccessful)
        {

            count = Math.Min(count + 1, 9);
			changeColor = false;
        }
        if (GUI.Button(new Rect(xPos - 58, yPos + 65, 40, 60), "<color=white><size=50>▼</size></color>", guiStyle) && !balanceSuccessful)
        {
            count = Math.Max(count - 1, 1);
        }
    }

	void showText(){
		GUI.Label(new Rect(Screen.width/2 - 60, 80, Screen.width, Screen.height), "Press 'E' to look at lab note.");
	}
}