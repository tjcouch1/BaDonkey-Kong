using UnityEngine;
using System.Collections;

public class Gun_Ctrl : MonoBehaviour {

	public GameObject rocket;
	public float fireInterval = 0.1f;
	public float fireForce  = 1000.0f;
	private float elapsedTime;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(Input.GetButton("Fire1")) {
			if(elapsedTime >= fireInterval) {
				Fire();
				elapsedTime = 0.0f;
			}
		}
	}


	void Fire() 
	{
		Vector3 dir= (transform.rotation*Vector3.forward).normalized; 
		GameObject instance = (GameObject) Instantiate(rocket, transform.position, transform.rotation);
		instance.GetComponent<Rigidbody> ().AddForce (fireForce*dir);
		instance.name = "rocket";
		Destroy(instance, 6.0f);

	}
}
