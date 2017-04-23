using UnityEngine;
using System.Collections;

public class Gun_Control : MonoBehaviour {

	public GameObject bullet;
	public float fireForce  = 1000.0f;
	private float elapsedTime;

	public GameObject shootPos;

	private bool visible = false;
	private Vector3 origScale;

	// Use this for initialization
	void Start () {
		origScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		/*elapsedTime += Time.deltaTime;
		if(Input.GetButton("Fire1")) {
			if(elapsedTime >= fireInterval) {
				Fire();
				elapsedTime = 0.0f;
			}
		}*/
	}


	public void Fire(Vector3 dir)
	{
		//Vector3 dir = (transform.rotation*Vector3.forward).normalized; 
		GameObject instance = (GameObject) Instantiate(bullet, transform.position, Quaternion.LookRotation(dir, Vector3.up));
		instance.GetComponent<Rigidbody> ().AddForce (fireForce*dir);
		Destroy(instance, 6.0f);

	}

	public void setVisible(bool v)
	{
		if (v)
			transform.localScale = origScale;
		else
			transform.localScale = Vector3.zero;
	}
}
