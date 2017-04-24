using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Control : MonoBehaviour {

	public bool unlocked = false;
	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		targetPos = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (unlocked)
			if ((transform.position - targetPos).magnitude > .5f)
				transform.position += (targetPos - transform.position) * .05f;
	}

	public void Unlock()
	{
		unlocked = true;
	}
}
