using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour {
	public static float score = 0;
	static GameObject player;
	static scoreBoardScript scoreBoard;
	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("MainCamera");
		}
		if(scoreBoard == null) {
			scoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<scoreBoardScript>(); //get script component from scoreboard
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.tag == "tip" || collision.tag == "projectile") {
			float accuracy = (collision.transform.position - transform.position).magnitude;//how close was shot to center of the target
			float distance = (player.transform.position - transform.position).magnitude; //how far away is the target
			score += distance/(5*accuracy);
			//Debug.Log("acc: "+accuracy+"	dist: "+distance+"	score: "+score);
			scoreBoard.UpdateScore(score);
		}
	}
}
