using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour {
	Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody>(); 
	}

	void FixedUpdate()
    {
    	rb.AddForce(Physics.gravity * rb.mass);
    }
}
