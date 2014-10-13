// JavaScript

public var count1;
public var count2;
public var count3;
public var count4;
public var one;
public var two;
public var three;
public var four;

var mySkin : GUISkin;

//var down_button : Texture;

//down_button = Resources.Load("down_button");

var check : Texture;
check = Resources.Load("check");

mySkin = Resources.Load("mySkin");

function Start(){
	count1=0;
	count2=0;
	count3=0;
	count4=0;
	one="1";
	two="2";
	three="3";
	four="4";
}

function OnGUI () {
	
	GUI.skin=mySkin;
	
	
	var cString1=count1.ToString();
	
	GUI.Box (Rect (Screen.width - 80,Screen.height - 80,40,40), cString1);
	
	GUI.Box (Rect (Screen.width - 40,Screen.height - 80,40,40), "H"+two+"O");
	
	if(GUI.Button (Rect (Screen.width - 80,Screen.height - 120,40,40), "", "up_button")){
		count1=count1+1;
	}
	
	if(GUI.Button (Rect (Screen.width - 80,Screen.height - 40,40,40), "", "down_button")){
		count1=count1-1;
	}
	
	
	GUI.Box (Rect (Screen.width - 120,Screen.height - 80,40,40), "+");
	
	
	var cString2=count2.ToString();
	
	GUI.Box (Rect (Screen.width - 200,Screen.height - 80,40,40), cString2);
	
	GUI.Box (Rect (Screen.width - 160,Screen.height - 80,40,40), "CO"+two);
	
	if(GUI.Button (Rect (Screen.width - 200,Screen.height - 120,40,40), "", "up_button")){
		count2=count2+1;
	}
	
	if(GUI.Button (Rect (Screen.width - 200,Screen.height - 40,40,40), "", "down_button")){
		count2=count2-1;
	}
	
	
	GUI.Box (Rect (Screen.width - 240,Screen.height - 80,40,40), "=");
	
	
	var cString3=count3.ToString();
	
	GUI.Box (Rect (Screen.width - 320,Screen.height - 80,40,40), cString3);
	
	GUI.Box (Rect (Screen.width - 280,Screen.height - 80,40,40), "O"+two);
	
	if(GUI.Button (Rect (Screen.width - 320,Screen.height - 120,40,40), "", "up_button")){
		count3=count3+1;
	}
	
	if(GUI.Button (Rect (Screen.width - 320,Screen.height - 40,40,40), "", "down_button")){
		count3=count3-1;
	}
	
	
	GUI.Box (Rect (Screen.width - 360,Screen.height - 80,40,40), "+");
	
	
	var cString4=count4.ToString();
	
	GUI.Box (Rect (Screen.width - 440,Screen.height - 80,40,40), cString4);
	
	GUI.Box (Rect (Screen.width - 400,Screen.height - 80,40,40), "CH"+four);
	
	if(GUI.Button (Rect (Screen.width - 440,Screen.height - 120,40,40), "", "up_button")){
		count4=count4+1;
	}
	
	if(GUI.Button (Rect (Screen.width - 440,Screen.height - 40,40,40), "", "down_button")){
		count4=count4-1;
	}
	
	// Correct Equation
	
	if(count1==2 && count2==1 && count3==2 && count4==1){
		//print("Correct!");
		if(GUI.Button (Rect (Screen.width - 520,Screen.height - 100,80,80), check)){
			// do nothing
		}
	}
	
}