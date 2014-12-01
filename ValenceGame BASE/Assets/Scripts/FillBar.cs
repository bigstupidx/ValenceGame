using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillBar : MonoBehaviour {
    public Scrollbar fillBar1;
    public Scrollbar fillBar2;
    public Scrollbar fillBar3;
    public GameObject player;
    public float react1;
    public float react2;
    public float react3;

    private Chemical.Reaction guiReaction;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        SetGUIReaction(null);
        guiReaction = null;
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
        react2 = (float)player.GetComponent<GunScript>().reactTank2.capacity;
        //fillBar2.size = react2 / 400f;
        react3 = (float)player.GetComponent<GunScript>().reactTank3.capacity;
        //fillBar3.size = react3 / 400f;
	}
}
