using UnityEngine;
using System;
using System.Collections;

public class NoteBalance : MonoBehaviour
{

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
    public int reactionNum;

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

    public Font noteFont;
    public int noteTextSize;

    private GUIStyle noteStyle;
    private GUIStyle noteTextStyle;
    private GUIStyle noteButtonStyle;
    private GUIStyle balanceTextStyle;
    private GUIStyle balanceBoxStyle;

    private Rect noteRect;
    private Rect balanceButtonRect;

    // Use this for initialization
    void Start()
    {
        GameObject temp = GameObject.Find("Reactions");
        Chemical.Reaction[] chems = temp.GetComponents<Chemical.Reaction>();
        chemReaction = chems[reactionNum];

        if(noteTextSize == 0)
        {
            noteTextSize = 30;
        }
    }

    void init()
    {

        if (chemReaction == null)
        {
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
        // Set note styles
        noteStyle = new GUIStyle();
        Texture2D white = new Texture2D(1, 1);
        white.SetPixel(0, 0, Color.white);
        noteStyle.normal.background = white;

        noteTextStyle = new GUIStyle();
        noteTextStyle.wordWrap = true;
        noteTextStyle.fontSize = noteTextSize;

        noteButtonStyle = new GUIStyle(GUI.skin.button);
        noteButtonStyle.fontSize = noteTextSize;

        balanceBoxStyle = new GUIStyle(GUI.skin.box);
        balanceBoxStyle.font = noteFont;
        balanceBoxStyle.fontSize = noteTextSize;

        balanceTextStyle = new GUIStyle();
        balanceTextStyle.richText = true;
        balanceTextStyle.fontSize = noteTextSize;

        if (noteFont != null)
        {
            noteTextStyle.font = noteFont;
            balanceTextStyle.font = noteFont;
            noteButtonStyle.font = noteFont;
        }

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


    // Returns true if coefficients in reaction match up in proper proportions
    bool coeffsCorrect() {
        int factor = 0;
        if (react1Count % react1Truth != 0)
        {
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
        noteRect = new Rect(20, 20, Screen.width - 40, Screen.height - 40);
        GUI.Box(noteRect, "", noteStyle);
        GUI.Box(noteRect, "", noteStyle);

        // Make button for balancing equation. If pressed, open equation panel
        balanceButtonRect = new Rect(40, Screen.height - 80, 400, 50);
        if (GUI.Button(balanceButtonRect, "Balance Equation", noteButtonStyle))
        {
            activateBalancingPanel = true;
        }

        // Make exit button for player to click and exit
        if (GUI.Button(new Rect(Screen.width - 440, Screen.height - 80, 400, 50), "Exit", noteButtonStyle))
        {
            exitNote();
        }


        // Display note text
        string noteText1 = @"<color=black>This is the hydrogen and oxygen lab. You’ll need to utilize all your resources to make it out of here in one piece. 
I know it’s still a prototype, but maybe the Catalyst can help you. Its database is still incomplete, so you’ll have to program it yourself. I don’t have much time, but I’ll leave you with this unbalanced equation to get you started...

☐ H2 + ☐ O2 -> ☐ H2O

Just in case you forgot, chemistry is not magic. Elements must be combined in certain proportions to react - they cannot violate the laws of conservation of mass. 
In each reaction, there must be the same amount of each element on both sides of the equation. 

Large numbers multiply across the entire compound, while subscripts only apply to the element they are attached to. 
For example: A2 + 2B2 -> 2A2B.</color>";

        //string noteText1 = string.Format("({0},{1})", Screen.width, Screen.height);

        GUI.Label(new Rect(40, 50, Screen.width - 80f, (balanceButtonRect.y - 20) - 50), noteText1, noteTextStyle);

        //GUI.Label(new Rect(40, 300, Screen.width - 80f, 150), noteText2, noteTextStyle);
    }

    string parseFormula(string formula) {
        System.IO.StringWriter scribe = new System.IO.StringWriter ();

        scribe.Write ("<color=white><size=100>");

        foreach (char c in formula) {
            if(Char.IsLetter(c)) {
                scribe.Write(c);
            }
            else {
                scribe.Write("<size=50>" + c + "</size>");
            }
        }

        scribe.Write ("</size></color>");

//        string jack = "CH4";
//        string bob = "<color=white><size=60>H<size=30>2</size></color>";

        return scribe.ToString();
    }



    void displayBalancingPanel()
    {
        // Display multiple times to increase opacity
        // Hacky? Yes. Effective? Heck yes.
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balance the Equation", balanceBoxStyle);
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balance the Equation", balanceBoxStyle);
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balance the Equation", balanceBoxStyle);
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balance the Equation", balanceBoxStyle);
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balance the Equation", balanceBoxStyle);
        GUI.Box(new Rect(40, 40, Screen.width - 80, Screen.height - 80), "Balance the Equation", balanceBoxStyle);



        //Declare coefficients 
        //		string h2CountString = String.Format ("<color=white><size=60>{0}</size></color>", h2Count).ToString ();
        //		string o2CountString = String.Format ("<color=white><size=60>{0}</size></color>", o2Count).ToString ();
        //		string h2oCountString = String.Format("<color=white><size=60>{0}</size></color>", h2oCount).ToString();

        //		string h2CountString = String.Format ("<color=white><size=60>{0}</size></color>", react1Count).ToString ();
        //		string o2CountString = String.Format ("<color=white><size=60>{0}</size></color>", react2Count).ToString ();
        //		string h2oCountString = String.Format("<color=white><size=60>{0}</size></color>", prod1Count).ToString();


        //Declare generic coeffs
        string react1Coeff = String.Format("<color=white><size=100>{0}</size></color>", react1Count).ToString();
        string react2Coeff = String.Format("<color=white><size=100>{0}</size></color>", react2Count).ToString();
        string react3Coeff = String.Format("<color=white><size=100>{0}</size></color>", react3Count).ToString();
        string prod1Coeff = String.Format("<color=white><size=100>{0}</size></color>", prod1Count).ToString();
        string prod2Coeff = String.Format("<color=white><size=100>{0}</size></color>", prod2Count).ToString();
        string prod3Coeff = String.Format("<color=white><size=100>{0}</size></color>", prod3Count).ToString();


        //Generic change color of coeffs
        if (balanceAttempted && !coeffsTrue && changeColor) {
            react1Coeff = String.Format("<color=red><size=100>{0}</size></color>", react1Count).ToString();
            react2Coeff = String.Format("<color=red><size=100>{0}</size></color>", react2Count).ToString();
            react3Coeff = String.Format("<color=red><size=100>{0}</size></color>", react3Count).ToString();
            prod1Coeff = String.Format("<color=red><size=100>{0}</size></color>", prod1Count).ToString();
            prod2Coeff = String.Format("<color=red><size=100>{0}</size></color>", prod2Count).ToString();
            prod3Coeff = String.Format("<color=red><size=100>{0}</size></color>", prod3Count).ToString();
        }
        if (balanceAttempted && coeffsTrue && changeColor) {
            react1Coeff = String.Format("<color=green><size=100>{0}</size></color>", react1Count).ToString();
            react2Coeff = String.Format("<color=green><size=100>{0}</size></color>", react2Count).ToString();
            react3Coeff = String.Format("<color=green><size=100>{0}</size></color>", react3Count).ToString();
            prod1Coeff = String.Format("<color=green><size=100>{0}</size></color>", prod1Count).ToString();
            prod2Coeff = String.Format("<color=green><size=100>{0}</size></color>", prod2Count).ToString();
            prod3Coeff = String.Format("<color=green><size=100>{0}</size></color>", prod3Count).ToString();
        }


        //		System.IO.StringWriter scribe = new System.IO.StringWriter ();

        // GUI element for arrow
        Vector2 arrowPos = new Vector2(Screen.width / 2, Screen.height / 2);
        //Vector2 o2Pos = new Vector2(340, 160);
        GUI.Label(new Rect(arrowPos.x - 50, arrowPos.y - 100, 200, 200), "<color=white><size=80>→</size></color>", balanceTextStyle);


        // GUI elements for Reactant 1
        Vector2 react1Pos = new Vector2(arrowPos.x - 200, arrowPos.y - 100);

        string dispFormula = parseFormula(chemReaction.Reactant1.Formula);
        Rect rCoeff1Rect = new Rect(react1Pos.x - 75, react1Pos.y, 80, 100);
        GUI.Label(new Rect(react1Pos.x, react1Pos.y, 100, 100), dispFormula, balanceTextStyle);
        GUI.Box(rCoeff1Rect, react1Coeff, balanceTextStyle);
        addButtons(rCoeff1Rect, ref react1Count);

        // GUI elements for Reactant 2
        if (react2Exist) {
            Vector2 react2Pos = new Vector2(arrowPos.x - 510, arrowPos.y - 100);

            dispFormula = parseFormula(chemReaction.Reactant2.Formula);
            Rect rCoeff2Rect = new Rect(react2Pos.x - 75, react2Pos.y, 80, 100);
            GUI.Label(new Rect(react2Pos.x, react2Pos.y, 100, 100), dispFormula + "<color=white><size=100>   +</size></color>", balanceTextStyle);
            GUI.Box(rCoeff2Rect, react2Coeff, balanceTextStyle);
            addButtons(rCoeff2Rect, ref react2Count);
        }

        // GUI elements for Reactant 3
        if (react3Exist && !react2Exist) {
            Vector2 react3Pos = new Vector2(arrowPos.x - 820, arrowPos.y - 100);

            dispFormula = parseFormula(chemReaction.Reactant3.Formula);
            Rect rCoeff3Rect = new Rect(react3Pos.x - 75, react3Pos.y, 80, 100);
            GUI.Label(new Rect(react3Pos.x, react3Pos.y, 100, 100), dispFormula + "<color=white><size=100>   +</size></color>", balanceTextStyle);
            GUI.Box(rCoeff3Rect, react3Coeff, balanceTextStyle);
            addButtons(rCoeff3Rect, ref react3Count);
        }

        if (react3Exist && react2Exist) {
            Vector2 react3Pos = new Vector2(arrowPos.x - 760, arrowPos.y - 100);

            dispFormula = parseFormula(chemReaction.Reactant3.Formula);
            Rect rCoeff3Rect = new Rect(react3Pos.x - 75, react3Pos.y, 80, 100);
            GUI.Label(new Rect(react3Pos.x, react3Pos.y, 100, 100), dispFormula + "<color=white><size=100>   +</size></color>", balanceTextStyle);
            GUI.Box(rCoeff3Rect, react3Coeff, balanceTextStyle);
            addButtons(rCoeff3Rect, ref react3Count);
        }


        // GUI elements for Product 1
        Vector2 prod1Pos = new Vector2(arrowPos.x + 140, arrowPos.y - 100);

        dispFormula = parseFormula(chemReaction.Product1.Formula);
        Rect pCoeff1Rect = new Rect(prod1Pos.x - 75, prod1Pos.y, 80, 100);
        GUI.Label(new Rect(prod1Pos.x, prod1Pos.y, 100, 100), dispFormula, balanceTextStyle);
        GUI.Box(pCoeff1Rect, prod1Coeff, balanceTextStyle);
        addButtons(pCoeff1Rect, ref prod1Count);


        // GUI elements for Product 2
        if (prod2Exist) {
            Vector2 prod2Pos = new Vector2(arrowPos.x + 330, arrowPos.y - 100);

            dispFormula = parseFormula(chemReaction.Product2.Formula);
            Rect pCoeff2Rect = new Rect(prod2Pos.x + 75, prod2Pos.y, 80, 100);
            GUI.Label(new Rect(prod2Pos.x, prod2Pos.y, 100, 100), "<color=white><size=100>+    </size></color>" + dispFormula, balanceTextStyle);
            GUI.Box(pCoeff2Rect, prod2Coeff, balanceTextStyle);
            addButtons(pCoeff2Rect, ref prod2Count);
        }

        // GUI elements for Product 3
        if (prod3Exist && !prod2Exist) {
            Vector2 prod3Pos = new Vector2(arrowPos.x + 640, arrowPos.y - 100);

            dispFormula = parseFormula(chemReaction.Product3.Formula);
            Rect pCoeff3Rect = new Rect(prod3Pos.x + 75, prod3Pos.y, 80, 100);
            GUI.Label(new Rect(prod3Pos.x, prod3Pos.y, 100, 100), "<color=white><size=100>+    </size></color>" + dispFormula, balanceTextStyle);
            GUI.Box(pCoeff3Rect, react3Coeff, balanceTextStyle);
            addButtons(pCoeff3Rect, ref react3Count);
        }

        if (prod3Exist && prod2Exist) {
            Vector2 prod3Pos = new Vector2(arrowPos.x + 640, arrowPos.y - 100);

            dispFormula = parseFormula(chemReaction.Product3.Formula);
            Rect pCoeff3Rect = new Rect(prod3Pos.x + 75, prod3Pos.y, 80, 100);
            GUI.Label(new Rect(prod3Pos.x, prod3Pos.y, 100, 100), "<color=white><size=100>+    </size></color>" + dispFormula, balanceTextStyle);
            GUI.Box(pCoeff3Rect, prod3Coeff, balanceTextStyle);
            addButtons(pCoeff3Rect, ref prod3Count);
        }

        // Make button to attempt to balance the equation
        if (balanceSuccessful) {
            GUI.enabled = false;
        }
        if (GUI.Button(new Rect(100, Screen.height - 120, 300, 50), "Try", noteButtonStyle)) {
            changeColor = true;
            coeffsTrue = coeffsCorrect();
            balanceAttempted = true;
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
                labeltext = "<color=green>Equation successfully balanced!</color>";
            }
            else
            {
                labeltext = "<color=red>Balance unsuccessful- try again.</color>";
            }
            GUI.Label(new Rect(100, Screen.height - 200, 300, 20), labeltext, balanceTextStyle);
        }

        // Make exit button for player to click and exit
        if (GUI.Button(new Rect(Screen.width - 400, Screen.height - 120, 300, 50), "Exit", noteButtonStyle))
        {
            exitNote();
        }

    }

    void exitNote()
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

    // Add up/down buttons over a coefficient
    // numberRect = rect that arrows should be placed around
    void addButtons(Rect aroundRect, ref int count)
    {
        Rect topButtonRect = new Rect(aroundRect.x - aroundRect.width / 6, aroundRect.y - aroundRect.height / 1.2f, aroundRect.width, aroundRect.height);
        Rect bottomButtonRect = new Rect(aroundRect.x - aroundRect.width / 6, aroundRect.y + aroundRect.height / 1f, aroundRect.width, aroundRect.height);

        if (GUI.Button(topButtonRect, "<color=white><size=80>▲</size></color>", balanceTextStyle) && !balanceSuccessful)
        {
            count = Math.Min(count + 1, 9);
            changeColor = false;
        }
        if (GUI.Button(bottomButtonRect, "<color=white><size=80>▼</size></color>", balanceTextStyle) && !balanceSuccessful)
        {
            count = Math.Max(count - 1, 1);
        }
    }

    void showText(){
        GUI.Label(new Rect(Screen.width/2 - 60, 80, Screen.width, Screen.height), "<size=24>Press 'E' to look at lab note.</size>");
    }
}