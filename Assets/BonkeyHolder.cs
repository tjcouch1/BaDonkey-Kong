using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkeyHolder : MonoBehaviour {

	int num_badonkeys = 0;
	int num_deaths = 0;

	// Use this for initialization
	void Start () {
		num_badonkeys = gameObject.GetComponentsInChildren<BKControllerController>().Length;
	}
	
	public void kill_badonkey() 
	{ 
		num_deaths++; 
		Debug.Log("Killed Bonkey: " + num_deaths);
	}
	public bool isKeySpawn () 
	{ 
		return num_deaths >= num_badonkeys; 
	}
}
