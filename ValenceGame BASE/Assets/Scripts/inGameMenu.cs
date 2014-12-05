using UnityEngine;
using System.Collections;

public class inGameMenu : MonoBehaviour {

    public GUISkin myGUISkin;

    public Texture2D resume;
    public Texture2D resumeHover;
    //public Texture2D restartActive;

    public Texture2D restart;
    public Texture2D restartHover;
    //public Texture2D restartActive;

    public Texture2D exit;
    public Texture2D exitHover;
    //public Texture2D exitActive;

    private float buttonWidth;
    private float buttonHeight;
    private float buttonSpacing;
    private float buttonColumnXPos;
    private float buttonColumnYStart;

    public bool menuActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape")) {
            if (!menuActive)
            {
                menuActive = true;
            }
        }
	}

    void OnGUI() {
        if (menuActive) {
            Time.timeScale = 0.0f;
            Camera.main.GetComponent<MouseLook>().enabled = false;
            GameObject.FindWithTag("Player").GetComponent<MouseLook>().enabled = false;
            Screen.showCursor = true;

            buttonWidth = Screen.width / 6;
            buttonHeight = buttonWidth / 3;

            buttonSpacing = buttonHeight / 10;
            buttonColumnXPos = Screen.width / 2 - buttonWidth / 2;
            buttonColumnYStart = Screen.height / 2;

            GUI.skin = myGUISkin;

            myGUISkin.button.normal.background = resume;
            myGUISkin.button.hover.background = resumeHover;

            if (Input.GetKeyDown("escape"))
            {
                Time.timeScale = 1.0f;
                Screen.showCursor = false;
                menuActive = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MouseLook>().enabled = true;
            }

            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart, buttonWidth, buttonHeight), "")) {
                Time.timeScale = 1.0f;
                Screen.showCursor = false;
                menuActive = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MouseLook>().enabled = true;
            }

            myGUISkin.button.normal.background = restart;
            myGUISkin.button.hover.background = restartHover;

            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart + buttonHeight + buttonSpacing, buttonWidth, buttonHeight), "")) {
                Application.LoadLevel(Application.loadedLevelName);
                Time.timeScale = 1.0f;
                Screen.showCursor = false;
                menuActive = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MouseLook>().enabled = true;
            }

            myGUISkin.button.normal.background = exit;
            myGUISkin.button.hover.background = exitHover;

            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart + buttonHeight * 2 + buttonSpacing * 2, buttonWidth, buttonHeight), "")) {
                Application.LoadLevel("MainMenu");
            }
        }
    }
}
