using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKController_Chase : MonoBehaviour {

	public Transform player;
	public float moveSpeed = 2.0f;
	Rigidbody rb;

	Animator anim;

	Vector3 moveDir;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		moveDir = Vector3.zero;
		toggleRun_anim();
	}

	public void Activate() {
		moveDir = Vector3.zero;
		toggleRun_anim();
	}
	
	// Update is called once per frame
	void Update () {
		moveDir = player.position - transform.position;
		moveDir.y = 0.0f;
		moveDir.Normalize();

		if(moveDir != Vector3.zero) {
			rb.velocity = moveDir * moveSpeed;
			transform.rotation = Quaternion.LookRotation(moveDir);
		}
	}

	// void OnCollisionEnter(Collision col)
	// {
	// 	if(col.gameObject.name == "Bullet") {
	// 		ParticleSystem instance = (ParticleSystem) Instantiate(ps, transform.position, transform.rotation);
	// 		Destroy(instance.gameObject, ps.startLifetime);
	// 		Destroy(instance.GetComponent<ParticleSystem>(), ps.startLifetime);
	// 		Destroy(gameObject);
	// 	}
	// }

	// void OnDrawGizmos()
	// {
	// 	Gizmos.DrawWireSphere(transform.position, awayDistance);
	// }

	void toggleWalk_anim() {
		anim.SetBool("Walking", true);
		anim.SetBool("Running", false);
	} 

	void toggleRun_anim() {
		anim.SetBool("Walking", false);
		anim.SetBool("Running", true);
	}

	void toggleNone_anim() {
		anim.SetBool("Walking", false);
		anim.SetBool("Running", false);
	}

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if(obj.tag == "Player") {
			anim.SetTrigger("Attack");
			obj.GetComponent<Player_Control>().Damage(20);

			Vector3 dir = obj.transform.position - transform.position;
			dir.y = 2;
			dir.Normalize();
			// Debug.Log(dir);
			obj.GetComponent<Rigidbody>().AddForce(dir * 1000);
		}
	}

}
