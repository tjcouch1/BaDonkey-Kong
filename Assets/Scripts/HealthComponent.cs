using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

	public int hp = 50;
	public GameObject ragdoll = null;

	public void Damage(int damage)
	{
		hp -= damage;
		if(hp <= 0){
			Debug.Log("Dead");
			if(ragdoll != null) {
				Transform t = Instantiate(ragdoll, transform.position, transform.rotation).transform;
				t.parent = transform.parent;
			}
			Destroy(gameObject);
		}
	}
}
