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
		ps.SetActive(true);
		light.SetActive(true);
	}

	void deactive() 
	{
		ps.SetActive(false);
		light.SetActive(false);
	}


	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player" && active) {
			// Win da game. 
		}
	}
}
