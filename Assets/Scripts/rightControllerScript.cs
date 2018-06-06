using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightControllerScript : MonoBehaviour {
	public static int arrowsLeft = 15;

	public GameObject arrow; //the arrow prefab must be attached to this in Unity editor
	private GameObject attachedArrow; //this is the arrow once it is "held" in the players hand

	static scoreBoardScript scoreBoard;

	public GameObject stringAttachPoint;
	public GameObject arrowAttachPoint;

	public SteamVR_TrackedObject rightController; //the right controller must be attached to this in Unity editor

	bool isAttached = false;

	float dist;

	/////////////////////////// Singleton Programming, have public instance to access public methods
	public static rightControllerScript Instance;

	void Awake() {
		if (Instance == null)
			Instance = this;
	}

	void OnDestroy() {
		if (Instance == this)
			Instance = null;
	}



	// Use this for initialization
	void Start () {
		
		AttachArrow();
		if (scoreBoard == null) {
			scoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<scoreBoardScript>(); //get script component from scoreboard
		}
		scoreBoard.UpdateScore(targetScript.score);
	}
	
	// Update is called once per frame
	void Update () {
		AttachArrow();
		if (isAttached) {
			PullString();
			var device = SteamVR_Controller.Input((int)rightControllerScript.Instance.rightController.index);
			if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
				Fire();
			}
		}
	}

	private void AttachArrow() {
		if(attachedArrow == null && arrowsLeft > 0) { //if an arrow is not already attached
			attachedArrow = Instantiate(arrow);
			attachedArrow.transform.parent = rightController.transform; //make arrow child of controller
			attachedArrow.transform.localPosition = new Vector3(0, 0, 0);
			attachedArrow.transform.localRotation = Quaternion.identity;
		}

	}

	public void AttachBowToArrow() {
		attachedArrow.transform.parent = arrowAttachPoint.transform;
		attachedArrow.transform.localPosition = stringAttachPoint.transform.localPosition + new Vector3(0f, 0f, 0f);
		attachedArrow.transform.rotation = stringAttachPoint.transform.rotation;

		isAttached = true;
		
	}

	private void PullString() {
		dist = (rightController.transform.position - stringAttachPoint.transform.position).magnitude; //distance of controller from hand
		arrowAttachPoint.transform.localPosition = stringAttachPoint.transform.localPosition + new Vector3(0f, 0f, -5*dist-(1.3f));
	}

	void Fire() {
		attachedArrow.transform.parent = null;
		Rigidbody arrowRigid = attachedArrow.GetComponent<Rigidbody>();
		//Rigidbody arrowTipRigid = attachedArrow.GetComponentsInChildren<Rigidbody>()[1];

		//enable forces to act on arrow/tip
		arrowRigid.isKinematic = false;
		//arrowTipRigid.isKinematic = false;

		//enable gravity on arrow/tip
		arrowRigid.useGravity = true;
		//arrowTipRigid.useGravity = true;

		isAttached = false;
		arrowRigid.AddForce(attachedArrow.transform.forward * dist * 250);
		//arrowTipRigid.AddForce(attachedArrow.transform.forward * dist * 250);

		//arrowRigid.velocity = attachedArrow.transform.forward * dist * 10f;
		attachedArrow = null;

		arrowAttachPoint.transform.localPosition = new Vector3(0, 0, -1.416228f); //reset the bowstring

		arrowsLeft--; //one less arrow

		scoreBoard.UpdateScore(targetScript.score);
	}
}
