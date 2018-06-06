using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreBoardScript : MonoBehaviour {
	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateScore(float score) {
		text.text = "Arrows Left: "+rightControllerScript.arrowsLeft+"\n\nScore: " + score.ToString("n2");

	}
}
