using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPenguinController : MonoBehaviour {

	public PauseGame pause;
	public Player_Control pc;
	public GameObject BKHolder;
	bool give_gun = false;

	void OnTriggerEnter(Collider other) {
		if(!give_gun){
			if(other.gameObject.tag == "Player") {
				give_gun = true;

				BKControllerController[] badonks = 
					BKHolder.GetComponentsInChildren<BKControllerController>();
				foreach(BKControllerController bkc in badonks)
					bkc.activate_chase();

				pause.enablePenguinScreen();
				pause.Pause();
				pc.setGun(true);
			} 
		}
	}
}
