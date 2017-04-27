using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

	bool active = false;
	ParticleSystem ps;


	void Start() {
		ps = GetComponent<ParticleSystem>();

	}

	public void activate()
	{

	}


	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player" && active) {
			// Win da game. 
		}
	}
}
