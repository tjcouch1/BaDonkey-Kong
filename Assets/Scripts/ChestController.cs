using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

	bool active = false;
	ParticleSystem ps;
	Light light;


	void Start() {
		ps = GetComponent<ParticleSystem>();
		light = GetComponent<Light>();
	}	

	public void activate()
	{
		light.enabled = true;
		pc.enabled = true;
	}

	void deactivate() {
		light.enabled = false;
		pc.enabled = false;
	}


	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player" && active) {
			// Win da game. 
		}
	}
}
