using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour {

	// public GameObject waypoint_handle;
	public Transform player;
	public Waypoints_s waypoint;
	public float moveSpeed = 2.0f;
	public float distanceToPlayer = 5.0f;
	public float awayDistance = 10.0f;
	public float chillTime = 2.0f;

	Animator anim;

	float moveTime = 0.0f;
	// float idle_time = 0.0f;
	// float time_til_next_idle = 0.0f;
	// bool talkingFlag = false;

	Vector3 moveDir;
	Vector3 storeDir;

	bool chillFlag;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		moveDir = Vector3.zero;
		storeDir = moveDir;
		transform.position = waypoint.StartPosition();
		// time_til_next_idle = Random.Range(10.0f, 15.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.position, transform.position) <= distanceToPlayer) {
			moveTime = 0.0f;	
			transform.LookAt(player, Vector3.up);
		}	
		else {
			moveDir = waypoint.GetDirection(transform);
		}

		if(waypoint.AwayFromWaypoint(transform, awayDistance)) {
			moveDir = waypoint.GetDirection(transform);
		}

		if(moveDir != storeDir) {
			chillFlag = true;
			anim.SetBool("Walking", false);
		}

		storeDir = moveDir;


		if(chillFlag) {
			moveTime += Time.deltaTime;
			moveDir = Vector3.zero;
			if(moveTime > chillTime) {
				moveTime = 0.0f;
				chillFlag = false;
			}
		}
		else{
			if(moveDir != Vector3.zero) {
				anim.SetBool("Walking", true);
				transform.position += moveDir * moveSpeed * Time.deltaTime;
				transform.rotation = Quaternion.LookRotation(moveDir);
			}
			else {
				anim.SetBool("Walking", false);
			}
		}	
	
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, awayDistance);
	}
}
