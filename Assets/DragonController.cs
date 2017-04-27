using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

	public GameObject chest;
	public Transform player;
	public GameObject fireball;
	public GameObject iceball;
	public float attack_cooldown = 3.0f;
	float attackTime = 0.0f;
	public float engagement_radius = 20.0f;
	public float rotation_speed = 3.0f;

	bool attackflag;

	Animator anim;
	
	void Start() {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(player.position, transform.position) < engagement_radius){
			anim.SetBool("Walking", true);
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Armature|Fly_New")) {
				attackflag = true;
			}
			else {
				if(attackflag) {
					attackflag = false;
					attackTime = 0.0f;
				}

				attackTime += Time.deltaTime;
				if(attackTime > attack_cooldown) {
					attack();
				}
				else {
					transform.LookAt(player);
				}

			}
		}
		else {
			anim.SetBool("Walking", false);
		}
	}


	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, engagement_radius);
	}

	void attack() {
		anim.SetTrigger("Attack");
		// Attack code
	}
}
