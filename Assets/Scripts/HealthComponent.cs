using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

	public int hp = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage(int damage)
	{
		hp -= damage;
		/**TODO: Clark
		 * Call controller's die method?
		 */
	}
}
