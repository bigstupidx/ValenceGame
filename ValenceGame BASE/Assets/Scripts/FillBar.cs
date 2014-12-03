using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillBar : MonoBehaviour {
    public Scrollbar fillBar1;
    public Scrollbar fillBar2;
    public Scrollbar fillBar3;
    public Scrollbar fillBar4;
    public Scrollbar fillBar5;
    public Scrollbar fillBar6;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public GameObject player;
    public float react1;
    public float react2;
    public float react3;
    public float prod1;
    public float prod2;
    public float prod3;

    private Chemical.Reaction guiReaction;

	// Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetGUIReaction(null);
        guiReaction = null;

        fillBar1.size = 0;
        fillBar2.size = 0;
        //fillBar3.size = 0;
        fillBar4.size = 0;
        fillBar5.size = 0;
        // fillBar6.size = 0;
        fillBar5.image.color = Color.cyan;
    }

    // Changes GUI to display new reaction
    // Triggered when active reaction changes
    void SetGUIReaction(Chemical.Reaction reaction)
    {
        // Change Informal Reaction Text
        Text informalReactText = GameObject.Find("InformalReactionText").GetComponent<Text>();
        if (reaction != null)
        {
            informalReactText.text = reaction.ReactName;
        }
        else
        {
            informalReactText.text = "";            
        }


        // Change Formal Reaction Text
        Text formalReactText = GameObject.Find("FormalReactionText").GetComponent<Text>();
        if(reaction != null)
        {
            formalReactText.text = reaction.EquationString;
        }
        else
        {
            formalReactText.text = "";
        }
    }
    
	
	// Update is called once per frame
	void Update () {
        
        // Detect change in active reaction and change GUI accordingly
        Chemical.Reaction activeReaction = player.GetComponent<GunScript>().activeReact;
        if(activeReaction != guiReaction)
        {
            guiReaction = activeReaction;
            SetGUIReaction(activeReaction);
        }

        react1 = (float)player.GetComponent<GunScript>().reactTank1.capacity;
        fillBar1.size = react1 / 400f;
        text1.text = player.GetComponent<GunScript>().reactTank1.substance.getFormula();
        fillBar1.image.color = player.GetComponent<GunScript>().reactTank1.substance.color;

        react2 = (float)player.GetComponent<GunScript>().reactTank2.capacity;
        fillBar2.size = react2 / 400f;
        text2.text = player.GetComponent<GunScript>().reactTank2.substance.getFormula();
        fillBar2.image.color = player.GetComponent<GunScript>().reactTank2.substance.color;

        //react3 = (float)player.GetComponent<GunScript>().reactTank3.capacity;
        //fillBar3.size = react3 / 400f;
        //text3.text = player.GetComponent<GunScript>().reactTank3.substance.getFormula();

        prod1 = (float)player.GetComponent<GunScript>().prodTank1.capacity;
        fillBar4.size = prod1 / 400f;
        text4.text = player.GetComponent<GunScript>().prodTank1.substance.getFormula();
        fillBar4.image.color = player.GetComponent<GunScript>().prodTank1.substance.color;

        prod2 = (float)player.GetComponent<GunScript>().prodTank2.capacity;
        fillBar5.size = prod2 / 400f;
        text5.text = "";
        text5.text = player.GetComponent<GunScript>().prodTank2.substance.getFormula();
        fillBar5.image.color = player.GetComponent<GunScript>().prodTank2.substance.color;

        //prod3 = (float)player.GetComponent<GunScript>().prodTank3.capacity;
       // fillBar6.size = prod3 / 400f;
        //text6.text = player.GetComponent<GunScript>().prodTank3.substance.getFormula();
	}
}
