using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_control : MonoBehaviour {
	//public Transform sparkle;
	private ParticleSystem _psystem;
	void Awake(){
		_psystem=GetComponent<ParticleSystem>();

	}
		void OnTriggerEnter (Collider col)
	{
		{
			_psystem.Play ();
		}
		// Use this for initialization
//	void Start () {
//		sparkle.GetComponent<ParticleSystem> ().enableEmission = false;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//
//void OnTriggerEnter3D(){
//		sparkle.GetComponent<ParticleSystem> ().enableEmission = true;
//		StartCoroutine (stopSparkles ());
//}
//		
//	IEnumerator stopSparkles(){
//		yield return new WaitForSeconds (.4f);
//		sparkle.GetComponent<ParticleSystem> ().enableEmission = false;
//	}
//


	}
}
