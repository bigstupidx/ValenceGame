using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class reactionMasterList : MonoBehaviour {
	public Chemical.Reaction[] reactionList; 
	// Use this for initialization
	void Start () {
		reactionList = GameObject.Find ("Reactions").GetComponents<Chemical.Reaction> ();
	}

    public Chemical.Reaction[] initReactionList()
    {
		reactionList = GameObject.Find ("Reactions").GetComponents<Chemical.Reaction> ();

        return reactionList;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
