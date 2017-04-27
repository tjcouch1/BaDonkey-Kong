using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIreballController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player") {
			obj.GetComponent<Player_Control>().Damage(20);

			Vector3 dir = obj.transform.position - transform.position;
			dir.y = 2;
			dir.Normalize();
			// Debug.Log(dir);
			obj.GetComponent<Rigidbody>().AddForce(dir * 1000);
		}
	}
}
