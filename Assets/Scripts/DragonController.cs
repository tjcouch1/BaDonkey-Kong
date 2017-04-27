using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

	public ChestController chest;
	public Transform player;
	public GameObject fireball;
	public GameObject iceball;
	public float attack_cooldown = 2.0f;
	float attackTime = 0.0f;
	public float engagement_radius = 20.0f;
	public float rotation_speed = 100.0f;
	public float ball_speed = 40.0f;

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
					attackTime = 0.0f;
				}

				else {
					float step = rotation_speed * Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards(transform.forward, 
									 player.position - transform.position, step, 0.0F);
					transform.rotation = Quaternion.LookRotation(newDir);
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

		GameObject fab;
		if(Random.Range(0, 2) == 1) fab = fireball;
		else fab = iceball;

		for(int i = 0; i < 3; i++) {
			GameObject newball = Instantiate(fab);
			newball.transform.position = transform.position + new Vector3(0, 3, 0);
			Rigidbody rb = newball.GetComponent<Rigidbody>();
			rb.angularVelocity = new Vector3(0, rotation_speed, 0);

			Vector3 projec = Quaternion.AngleAxis((i - 1) * 40, Vector3.up) * transform.forward;
			// Vector3 projec = transform.forward.Rotate(new Vector3(0, (i - 1) * 30, 0));
			rb.velocity = projec * ball_speed;
			Destroy(newball, 3.0f);
		}
	}

	public void spawn_chest() {
		Debug.Log("SPAWNING KEY");
		Transform t = Instantiate(chest).transform;
		t.position = transform.position;

		Rigidbody rb = t.gameObject.GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, 15, 0);
		rb.angularVelocity = new Vector3(15 * 33, 15 * 33, 0);
	}
}



