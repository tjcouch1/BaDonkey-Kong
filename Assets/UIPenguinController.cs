using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPenguinController : MonoBehaviour {

	public PauseGame pause;
	public Player_Control pc;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			pause.enablePenguinScreen();
			pause.Pause();
			pc.setGun(true);
		} 
	}
}
