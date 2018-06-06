using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody r = GetComponent<Rigidbody>();
		if (r.useGravity) {
			transform.LookAt(transform.position + r.velocity);
		}
	}

	void OnTriggerEnter(Collider collide) {
		if(collide.tag == "bow") //if making contact with bow
			AttachArrow();
	}

	void OnTriggerStay(Collider collide) {
		if (collide.tag == "bow") //if making contact with bow
			AttachArrow();
	}

	private void AttachArrow() {
		var device = SteamVR_Controller.Input((int)rightControllerScript.Instance.rightController.index);
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			rightControllerScript.Instance.AttachBowToArrow();
		}
	}
}
