using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKControllerController : MonoBehaviour {

	BadonkeyKong_Controller con_chill;
	BKController_Chase con_chase;

	void Awake() {
		con_chill = GetComponent<BadonkeyKong_Controller>();
		con_chase = GetComponent<BKController_Chase>();
	}

	// Use this for initialization
	void Start () {
		activate_chill();
	}
		
	public void activate_chase() {
		con_chill.enabled = false;
		con_chase.enabled = true;
		con_chase.Activate();
	}

	void activate_chill() {
		con_chill.enabled = true;
		con_chase.enabled = false;
	}

}
