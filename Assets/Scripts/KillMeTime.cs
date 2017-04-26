using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeTime : MonoBehaviour {

	public float killtime = 2.0f;
	public GameObject key;
	public float keyforce = 3.0f;
	float toKill = 0.0f;
	
	// Update is called once per frame
	void Update () {
		toKill += Time.deltaTime;
		if(toKill > killtime) {
			BonkeyHolder bh = transform.parent.gameObject.GetComponent<BonkeyHolder>();
			bh.kill_badonkey();
			if(bh.isKeySpawn()) {
				Debug.Log("SPAWNING KEY");
				Transform t = Instantiate(key).transform;
				t.position = transform.position;

				Rigidbody rb = t.gameObject.GetComponent<Rigidbody>();
				rb.velocity = new Vector3(0, keyforce, 0);
				rb.angularVelocity = new Vector3(keyforce * 33, keyforce * 33, 0);
			}
			Destroy(gameObject);
		}
	}
}
