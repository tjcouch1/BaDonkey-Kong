using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Control : MonoBehaviour {

	public int damage = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter(Collision other)
	{
		GameObject obj = other.gameObject;
		if (!obj.CompareTag("Player"))
		{
			if (obj.CompareTag("Enemy"))
				obj.GetComponent<HealthComponent>().Damage(damage);
		}
		Destroy(gameObject, .1f);
	}
}
