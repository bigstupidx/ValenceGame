using UnityEngine;
using System.Collections;

public class MainmenuScript : MonoBehaviour
{
    public GUISkin myGUISkin;

    public MovieTexture menuBackground;
    public Texture2D valenceTitle;
    public Texture2D valenceDescription;

    public Texture2D levelSelect;
    public Texture2D levelSelectHover;
    //public Texture2D levelSelectActive;
    public Texture2D elements;
    public Texture2D elementsHover;
    //public Texture2D elementsActive;
    public Texture2D exit;
    public Texture2D exitHover;
    //public Texture2D exitActive;

    public Texture2D level1;
    public Texture2D level1Hover;
    //public Texture2D level1Active;
    public Texture2D level2;
    public Texture2D level2Hover;
    //public Texture2D level2Active;
    public Texture2D back;
    public Texture2D backHover;
    //public Texture2D backActive;

    public bool titleScreenActive;

    private float buttonWidth;
    private float buttonHeight;
    private float buttonSpacing;
    private float buttonColumnXPos;
    private float buttonColumnYStart;
    private float menuBackgroundWidth;
    private float menuBackgroundHeight;
    private float titleXPos;
    private float titleYPos;
    private float titleWidth;
    private float titleHeight;
    private float descriptionXPos;
    private float descriptionYPos;
    private float descriptionWidth;
    private float descriptionHeight;


    void OnGUI()
    {
        /*if (Screen.width / Screen.height > (16.0f / 9.0f)){
            menuBackgroundWidth = Screen.width;
            menuBackgroundHeight = menuBackgroundWidth * 0.5625f;

            buttonHeight = Screen.height * (6.0f / 27.0f);
            buttonWidth = buttonHeight * 3;
        }
        else {*/
            menuBackgroundHeight = Screen.height;
            menuBackgroundWidth = menuBackgroundHeight / 0.5625f;

            buttonWidth = Screen.width / 6;
            buttonHeight = buttonWidth / 3;
        //}

        buttonSpacing = buttonHeight / 10;
        buttonColumnXPos = Screen.width / 2 - buttonWidth / 2;
        buttonColumnYStart = Screen.height / 2;

        titleWidth = buttonWidth * 2;
        titleHeight = titleWidth / 2;
        titleYPos = buttonColumnYStart - titleHeight - buttonSpacing * 2;
        titleXPos = Screen.width / 2 - titleWidth / 2;

        descriptionWidth = buttonWidth * 1.5f;
        descriptionHeight = descriptionWidth / 5;
        descriptionXPos = Screen.width / 2 - descriptionWidth / 2;
        descriptionYPos = buttonColumnYStart + buttonHeight * 2 + buttonSpacing * 3;

        GUI.DrawTexture(new Rect(0, 0, menuBackgroundWidth, menuBackgroundHeight), menuBackground, ScaleMode.ScaleToFit);
        menuBackground.Play();
		menuBackground.loop = true;
        GUI.skin = myGUISkin;
        GUI.DrawTexture(new Rect(titleXPos, titleYPos, titleWidth, titleHeight), valenceTitle, ScaleMode.ScaleToFit);

        if (titleScreenActive)
        {
            myGUISkin.button.normal.background = levelSelect;
            myGUISkin.button.hover.background = levelSelectHover;
            
            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart, buttonWidth, buttonHeight), ""))
            {
                titleScreenActive = false;
            }
            myGUISkin.button.normal.background = exit;
            myGUISkin.button.hover.background = exitHover;
            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart + buttonHeight + buttonSpacing, buttonWidth, buttonHeight), ""))
            {
                Application.Quit();
            }
            GUI.DrawTexture(new Rect(descriptionXPos, descriptionYPos, descriptionWidth, descriptionHeight), valenceDescription, ScaleMode.ScaleToFit);
        }
        else
        {
            myGUISkin.button.normal.background = level1;
            myGUISkin.button.hover.background = level1Hover;
            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart, buttonWidth, buttonHeight), ""))
            {
                Application.LoadLevel("testScene");
            }
            myGUISkin.button.normal.background = level2;
            myGUISkin.button.hover.background = level2Hover;
            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart + buttonHeight + buttonSpacing, buttonWidth, buttonHeight), ""))
            {
                Application.LoadLevel("Level1");
            }
            myGUISkin.button.normal.background = back;
            myGUISkin.button.hover.background = backHover;
            if (GUI.Button(new Rect(buttonColumnXPos, buttonColumnYStart + buttonHeight * 2 + buttonSpacing * 2, buttonWidth, buttonHeight), ""))
            {
                titleScreenActive = true;
            }
        }
    }
}