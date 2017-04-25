using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadonkeyKong_Controller : MonoBehaviour {

	public GameObject waypoint_nav;
	GameObject current_nav;
	BoxCollider box_col;
	CapsuleCollider cap_col;

	// Use this for initialization
	void Awake () {
		box_col = GetComponent<BoxCollider>();
		cap_col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
