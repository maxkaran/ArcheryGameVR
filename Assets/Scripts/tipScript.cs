using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) { //once tip hits a target
		if (collider.tag != "projectile") { 
			Rigidbody r = GetComponentInParent<Rigidbody>(); //tell arrow to stop moving
			r.isKinematic = true;
			r.useGravity = false;
		}
	}

}
