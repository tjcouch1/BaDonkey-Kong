using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadonkeyKong_Controller : MonoBehaviour {

	public Waypoints_s waypoint;
	public Transform player;
	public ParticleSystem ps;


	public float moveSpeed = 4.0f;
	public float chillTime = 2.0f;
	public float distanceToPlayer = 10.0f;
	public float awayDistance = 10.0f;

	Animator anim;

	float moveTime = 0.0f;
	float spinTime  = 0.0f;
	bool spinFlag  = false;
	float time2idle = 10.0f;

	Vector3 moveDir;

	bool chillFlag;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		moveDir = Vector3.zero;
		transform.position = waypoint.StartPosition();
		time2idle = Random.Range(5.0f, 15.0f);
		toggleWalk_anim();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(waypoint.AwayFromWaypoint(transform, awayDistance)) {
			moveDir = waypoint.GetDirection(transform);
		}
		
		if(Vector3.Distance(player.position, transform.position) <= distanceToPlayer ||
		   anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
			moveTime = 0.0f;	
			moveDir = Vector3.zero;
			transform.LookAt(player, Vector3.up);
			toggleNone_anim();
		}	
		else {
			toggleWalk_anim();
			moveDir = waypoint.GetDirection(transform);
		}

		// update motion mode: spin/move
		if(!spinFlag) {
			moveTime += Time.deltaTime;
			if(moveTime > time2idle) {
				spinFlag = true;
				moveTime = 0.0f;
				time2idle = Random.Range(3.0f, 15.0f);
				toggleNone_anim();
				anim.SetTrigger("TriggleIdle");
			}
		}
		else {
			spinTime += Time.deltaTime;
			toggleNone_anim();	
			if(spinTime > 4.0f) {
				spinFlag = false;
				spinTime = 0.0f;
				toggleWalk_anim();
			}
		}
		
		if(!spinFlag) {
			if(moveDir != Vector3.zero) {
				transform.position += moveDir*moveSpeed*Time.deltaTime;
				transform.rotation = Quaternion.LookRotation(moveDir);
			}
		}
		else {
			// transform.rotation *= Quaternion.Euler(0, 10, 0);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Bullet") {
			ParticleSystem instance = (ParticleSystem) Instantiate(ps, transform.position, transform.rotation);
			Destroy(instance.gameObject, ps.startLifetime);
			Destroy(instance.GetComponent<ParticleSystem>(), ps.startLifetime);
			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, awayDistance);
	}

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
