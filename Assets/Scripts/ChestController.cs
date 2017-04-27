using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour {

	bool active = false;
	ParticleSystem ps;
	Light light;


	void Start() {
		ps = GetComponent<ParticleSystem>();
		light = GetComponent<Light>();
		deactivate();
	}	

	public void activate()
	{
		light.enabled = true;
		ps.Play();
	}

	void deactivate() {
		light.enabled = false;
		ps.Stop();
	}


	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player" && active) {
			// Win da game. 
			SceneManager.LoadScene("Win");
		}
	}
}
