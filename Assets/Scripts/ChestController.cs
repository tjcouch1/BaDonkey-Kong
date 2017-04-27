using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour {


	void OnCollisionEnter(Collision other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player") {
			// win da game
			Debug.Log("WIN THA GAME");
			SceneManager.LoadScene("Win");
		}
	}
}
