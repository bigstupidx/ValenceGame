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
//    public int h2Count;
//    public int o2Count;
//    public int h2oCount;
	public bool isNear = false;

    public Chemical.Reaction chemReaction;

	public int react1Count;
	public int react2Count;
	public int react3Count;
	public int prod1Count;
	public int prod2Count;
	public int prod3Count;
	public int react1Truth;
	public int react2Truth;
	public int react3Truth;
	public int prod1Truth;
	public int prod2Truth;
	public int prod3Truth;
	public bool react2Exist;
	public bool react3Exist;
	public bool prod2Exist;
	public bool prod3Exist;

	public bool coeffsTrue;

	public bool initializer = false;
	public bool error = false;

    private GUIStyle guiStyle;

	// Use this for initialization
	void Start () {


//        h2Count = 1;
//        o2Count = 1;
//        h2oCount = 1;
        guiStyle = new GUIStyle();
        guiStyle.richText = true;
	}

	void init() {

        if (chemReaction == null) {
			error = true;
		}
		
		react1Count = 1;
		react1Truth = chemReaction.ReactCoeff1;
		
		if (chemReaction.Reactant2 == null) {
			react2Exist = false;
			react2Count = 0;
			react2Truth = 0;
		}
		else {
			react2Exist = true;
			react2Truth = chemReaction.ReactCoeff2;
			react2Count = 1;
		}
		if (chemReaction.Reactant3 == null) {
			react3Exist = false;
			react3Count = 0;
			react3Truth = 0;
		}
		else {
			react3Exist = true;
			react3Truth = chemReaction.ReactCoeff3;
			react3Count = 1;
		}
		
		prod1Truth = chemReaction.ProdCoeff1;
		prod1Count = 1;
		if (chemReaction.Product2 == null) {
			prod2Exist = false;
			prod2Count = 0;
			prod2Truth = 0;
		}
		else {
			prod2Exist = true;
			prod2Truth = chemReaction.ProdCoeff2;
			prod2Count = 1;
		}
		if (chemReaction.Product3 == null) {
			prod3Exist = false;
			prod3Count = 0;
			prod3Truth = 0;
		}
		else {
			prod3Exist = true;
			prod3Truth = chemReaction.ProdCoeff3;
			prod3Count = 1;
		}

		initializer = true;
	}

	// Update is called once per frame
	void Update () {

		if (!initializer) {
			init();	
		}

        // Disable note once equation balance is finished
        if (noteFinished && balanceSuccessful)
        {
            enabled = false;
            //renderer.enabled = false;
        }
		else {
			noteFinished = false;
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


	bool coeffsCorrect() {
		int factor = 0;
		if (react1Count % react1Truth != 0) {
//			error = true;
			return false;	
		}

		factor = react1Count / react1Truth;

		if(react2Exist && react2Count != factor * react2Truth) {
			return false;
		}
		if(react3Exist && react3Count != factor * react3Truth) {
			return false;
		}
	
		if(prod1Count != factor * prod1Truth) {
			return false;
		}
		if(prod2Exist && prod2Count != factor * prod2Truth) {
			return false;
		}
		if(prod3Exist && prod3Count != factor * prod3Truth) {
			return false;
		}

		return true;
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

	string parseFormula(string formula) {
		System.IO.StringWriter scribe = new System.IO.StringWriter ();

		scribe.Write ("<color=white><size=60>");

		int index = 0;
		foreach (char c in formula) {
			if(Char.IsLetter(c)) {
				scribe.Write(c);
			}
			else {
				scribe.Write("<size=30>" + c + "</size>");
			}
		}

		scribe.Write ("</size></color>");

//		string jack = "CH4";
//		string bob = "<color=white><size=60>H<size=30>2</size></color>";

		return scribe.ToString();
	}



    void displayBalancingPanel()
    {
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balancing Panel");

        

		//Declare coefficients 
//		string h2CountString = String.Format ("<color=white><size=60>{0}</size></color>", h2Count).ToString ();
//		string o2CountString = String.Format ("<color=white><size=60>{0}</size></color>", o2Count).ToString ();
//		string h2oCountString = String.Format("<color=white><size=60>{0}</size></color>", h2oCount).ToString();

//		string h2CountString = String.Format ("<color=white><size=60>{0}</size></color>", react1Count).ToString ();
//		string o2CountString = String.Format ("<color=white><size=60>{0}</size></color>", react2Count).ToString ();
//		string h2oCountString = String.Format("<color=white><size=60>{0}</size></color>", prod1Count).ToString();


		//Declare generic coeffs
		string react1Coeff = String.Format ("<color=white><size=60>{0}</size></color>", react1Count).ToString ();
		string react2Coeff = String.Format ("<color=white><size=60>{0}</size></color>", react2Count).ToString ();
		string react3Coeff = String.Format ("<color=white><size=60>{0}</size></color>", react3Count).ToString ();
		string prod1Coeff = String.Format("<color=white><size=60>{0}</size></color>", prod1Count).ToString();
		string prod2Coeff = String.Format("<color=white><size=60>{0}</size></color>", prod2Count).ToString();
		string prod3Coeff = String.Format("<color=white><size=60>{0}</size></color>", prod3Count).ToString();


		//Generic change color of coeffs
		if (balanceAttempted && !coeffsTrue && changeColor) {
			react1Coeff = String.Format ("<color=red><size=60>{0}</size></color>", react1Count).ToString ();
			react2Coeff = String.Format ("<color=red><size=60>{0}</size></color>", react2Count).ToString ();
			react3Coeff = String.Format ("<color=red><size=60>{0}</size></color>", react3Count).ToString ();
			prod1Coeff = String.Format ("<color=red><size=60>{0}</size></color>", prod1Count).ToString ();
			prod2Coeff = String.Format ("<color=red><size=60>{0}</size></color>", prod2Count).ToString ();
			prod3Coeff = String.Format ("<color=red><size=60>{0}</size></color>", prod3Count).ToString ();
		} 
		if (balanceAttempted && coeffsTrue && changeColor) {
			react1Coeff = String.Format ("<color=green><size=60>{0}</size></color>", react1Count).ToString ();
			react2Coeff = String.Format ("<color=green><size=60>{0}</size></color>", react2Count).ToString ();
			react3Coeff = String.Format ("<color=green><size=60>{0}</size></color>", react3Count).ToString ();
			prod1Coeff = String.Format ("<color=green><size=60>{0}</size></color>", prod1Count).ToString ();
			prod2Coeff = String.Format ("<color=green><size=60>{0}</size></color>", prod2Count).ToString ();
			prod3Coeff = String.Format ("<color=green><size=60>{0}</size></color>", prod3Count).ToString ();
		}


//		System.IO.StringWriter scribe = new System.IO.StringWriter ();

		// GUI element for arrow
		Vector2 arrowPos = new Vector2(Screen.width/2, Screen.height/2);
		//Vector2 o2Pos = new Vector2(340, 160);
		GUI.Label(new Rect(arrowPos.x - 50, arrowPos.y - 100, 200, 200), "<color=white><size=100>→</size></color>", guiStyle);
		//340+110, 160-30, 200, 200
		//450
		
		
		// GUI elements for H2
//		Vector2 h2Pos = new Vector2(160, 160);
		Vector2 react1Pos = new Vector2(arrowPos.x - 130, arrowPos.y - 65);
		
		string dispFormula = parseFormula (chemReaction.Reactant1.Formula);
		GUI.Label(new Rect(react1Pos.x, react1Pos.y, 100, 100), dispFormula, guiStyle);
		GUI.Box(new Rect(react1Pos.x - 50, react1Pos.y, 40, 60), react1Coeff, guiStyle);
//        addButtons(h2Pos.x, h2Pos.y, ref h2Count);
		addButtons(react1Pos.x, react1Pos.y, ref react1Count);



        // GUI elements for O2
        if(react2Exist) {
//			Vector2 o2Pos = new Vector2(340, 160);			
		Vector2 react2Pos = new Vector2(arrowPos.x - 310, arrowPos.y - 65);			
					
			dispFormula = parseFormula (chemReaction.Reactant2.Formula);
		GUI.Label(new Rect(react2Pos.x, react2Pos.y, 100, 100), dispFormula + "<color=white><size=60> +</size></color>", guiStyle);
		GUI.Box(new Rect(react2Pos.x - 50, react2Pos.y, 40, 60), react2Coeff, guiStyle);
	//        addButtons(o2Pos.x, o2Pos.y, ref o2Count);
		addButtons(react2Pos.x, react2Pos.y, ref react2Count);
		}

		if(react3Exist && !react2Exist) {
			//			Vector2 o2Pos = new Vector2(340, 160);			
			Vector2 react3Pos = new Vector2(arrowPos.x - 310, arrowPos.y - 65);			
			
			dispFormula = parseFormula (chemReaction.Reactant3.Formula);
			GUI.Label(new Rect(react3Pos.x, react3Pos.y, 100, 100), dispFormula + "<color=white><size=60> +</size></color>", guiStyle);
			GUI.Box(new Rect(react3Pos.x - 50, react3Pos.y, 40, 60), react3Coeff, guiStyle);
			//        addButtons(o2Pos.x, o2Pos.y, ref o2Count);
			addButtons(react3Pos.x, react3Pos.y, ref react3Count);
		}

		if(react3Exist && react2Exist) {
			//			Vector2 o2Pos = new Vector2(340, 160);			
			Vector2 react3Pos = new Vector2(arrowPos.x - 490, arrowPos.y - 65);			
			
			dispFormula = parseFormula (chemReaction.Reactant3.Formula);
			GUI.Label(new Rect(react3Pos.x, react3Pos.y, 100, 100), dispFormula + "<color=white><size=60> +</size></color>", guiStyle);
			GUI.Box(new Rect(react3Pos.x - 50, react3Pos.y, 40, 60), react3Coeff, guiStyle);
			//        addButtons(o2Pos.x, o2Pos.y, ref o2Count);
			addButtons(react3Pos.x, react3Pos.y, ref react3Count);
		}
        

        // GUI elements for H2O
 //       Vector2 h2oPos = new Vector2(640, 160);      
		Vector2 h2oPos = new Vector2(arrowPos.x + 130, arrowPos.y - 65);      

		dispFormula = parseFormula (chemReaction.Product1.Formula);
        GUI.Label(new Rect(h2oPos.x, h2oPos.y, 100, 100), dispFormula, guiStyle);
		GUI.Box(new Rect(h2oPos.x - 50, h2oPos.y, 40, 60), prod1Coeff, guiStyle);
//        addButtons(h2oPos.x, h2oPos.y, ref h2oCount);
        addButtons(h2oPos.x, h2oPos.y, ref prod1Count);


		// GUI elements for O2
		if(prod2Exist) {
			//			Vector2 o2Pos = new Vector2(340, 160);			
			Vector2 prod2Pos = new Vector2(arrowPos.x + 260, arrowPos.y - 65);			
			
			dispFormula = parseFormula (chemReaction.Product2.Formula);
			GUI.Label(new Rect(prod2Pos.x, prod2Pos.y, 100, 100), "<color=white><size=60>+    </size></color>" + dispFormula, guiStyle);
			GUI.Box(new Rect(prod2Pos.x + 56, prod2Pos.y, 40, 60), prod2Coeff, guiStyle);
			//        addButtons(o2Pos.x, o2Pos.y, ref o2Count);
			addButtons(prod2Pos.x + 106, prod2Pos.y, ref prod2Count);
		}
		
		if(prod3Exist && !prod2Exist) {
			//			Vector2 o2Pos = new Vector2(340, 160);			
			Vector2 prod3Pos = new Vector2(arrowPos.x + 310, arrowPos.y - 65);			
			
			dispFormula = parseFormula (chemReaction.Product3.Formula);
			GUI.Label(new Rect(prod3Pos.x, prod3Pos.y, 100, 100), "<color=white><size=60>+    </size></color>" + dispFormula, guiStyle);
			GUI.Box(new Rect(prod3Pos.x + 56, prod3Pos.y, 40, 60), react3Coeff, guiStyle);
			//        addButtons(o2Pos.x, o2Pos.y, ref o2Count);
			addButtons(prod3Pos.x + 106, prod3Pos.y, ref react3Count);
		}
		
		if(prod3Exist && prod2Exist) {
			//			Vector2 o2Pos = new Vector2(340, 160);			
			Vector2 prod3Pos = new Vector2(arrowPos.x + 490, arrowPos.y - 65);			
			
			dispFormula = parseFormula (chemReaction.Product3.Formula);
			GUI.Label(new Rect(prod3Pos.x, prod3Pos.y, 100, 100), "<color=white><size=60>+    </size></color>" + dispFormula, guiStyle);
			GUI.Box(new Rect(prod3Pos.x + 56, prod3Pos.y, 40, 60), prod3Coeff, guiStyle);
			//        addButtons(o2Pos.x, o2Pos.y, ref o2Count);
			addButtons(prod3Pos.x + 106, prod3Pos.y, ref prod3Count);
		}




        // Make button to attempt to balance the equation
        if (balanceSuccessful) {
            GUI.enabled = false;
		}
        if (GUI.Button(new Rect(100, Screen.height - 100, 80, 20), "Try"))
        {

			changeColor = true;
			coeffsTrue = coeffsCorrect ();	//NOT WROKING REIGHT
            balanceAttempted = true;
        //  Vector2 feedbackPos = new Vector2();		//NEVER USED
            // Test if equation is properly balanced
  //          if (h2Count == o2Count * 2 && h2oCount == o2Count * 2)
            if (coeffsTrue)
            {
                balanceSuccessful = true;
				chemReaction.unlocked = true;
				//GameObject.Find("Player").GetComponent<GunScript>().eqBalanced = true;
                //GameObject.Find("Player").GetComponent<GunScript>().reactSelected = true;
                //GameObject.Find("Player").GetComponent<GunScript>().activeReact = chemReaction;
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
                labeltext = "<color=blue>Equation successfully balanced!</color>";
            }
            else
            {
                labeltext = "<color=purple>Balance unsuccessful- try again.</color>";
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
		GUI.Label(new Rect(Screen.width/2 - 60, 80, Screen.width, Screen.height), "<size=24>Press 'E' to look at lab note.</size>");
	}
}