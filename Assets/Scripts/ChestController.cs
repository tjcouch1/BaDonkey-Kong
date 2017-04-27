using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {


	void OnCollisionEnter(Collision other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player") {
			// win da game
			Debug.Log("WIN THA GAME");
		}
	}
}
