using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryIntro : MonoBehaviour {

    Text paragraph1;
    Text paragraph2;
    Text paragraph3;
    Text paragraph4;
    Text paragraph5;
    Canvas canvas;
    Button startButton;
    float waitTime;
    float startTime;

	// Use this for initialization
	void Start () {
        paragraph1 = GameObject.Find("Paragraph1").GetComponent<Text>();
        paragraph2 = GameObject.Find("Paragraph2").GetComponent<Text>();
        paragraph3 = GameObject.Find("Paragraph3").GetComponent<Text>();
        paragraph4 = GameObject.Find("Paragraph4").GetComponent<Text>();
        paragraph5 = GameObject.Find("Paragraph5").GetComponent<Text>();
        canvas = gameObject.GetComponent<Canvas>();
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(() => { OnStartButtonClick(); });
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnStartButtonClick()
    {
        Application.LoadLevel("testScene");
    }
}
