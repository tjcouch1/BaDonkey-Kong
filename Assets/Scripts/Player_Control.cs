using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	public float walkSpeed = 2.0f;
	public float runSpeed = 4.0f;

	private float moveSpeed;
	private Vector3 v3_moveDirection; // the actual moving direction

	private Vector3 v3_forward; //Forward Direction of the character
	private Vector3 v3_right; //Right Direction of the character

	// follow-up camera param
	private Vector3 v3_cam_dir;
	private float cam_distance;

	private Animator anim;

	// Use this for initialization
	void Start () {
		moveSpeed = 0;
		v3_moveDirection = Vector3.zero;
		v3_forward = Vector3.zero;
		v3_right = Vector3.zero;

		v3_cam_dir = Camera.main.transform.position - transform.position;
		cam_distance = v3_cam_dir.magnitude;
		v3_cam_dir.Normalize ();

		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetMouseButton(0)) { // mouse left button
			moveSpeed = 0.0f;
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
				transform.rotation *= Quaternion.Euler(0, -5, 0);
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
				transform.rotation *= Quaternion.Euler(0, 5, 0);	
			anim.SetBool ("ShootBool", true);
		}

		else {
			//Get forward direction of the character
			v3_forward = Camera.main.transform.TransformDirection (Vector3.forward);

			//Make sure that vertical direction equals zero 
			v3_forward.y = 0;  

			v3_forward.Normalize ();

			// Always orthogonal to the forward direction vector
			v3_right = Vector3.Cross (Vector3.up, v3_forward);
			v3_right.Normalize ();

			float f_hor = Input.GetAxis ("Horizontal");
			float f_ver = Input.GetAxis ("Vertical");

			//Get the move direction
			v3_moveDirection = (f_hor * v3_right) + (f_ver * v3_forward);
			v3_moveDirection = v3_moveDirection.normalized;

			// Calculate actual movement
			if (v3_moveDirection.magnitude < 0.01 && v3_moveDirection.magnitude > -0.01) {
				moveSpeed = 0.0f;
			} else if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
				moveSpeed = runSpeed;
			} else {
				moveSpeed = walkSpeed;
			}

			anim.SetFloat ("Speed", Mathf.Abs (moveSpeed));

			//update the position
			transform.position += v3_moveDirection * moveSpeed * Time.deltaTime;

			// update the rotation
			if (v3_moveDirection != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation (v3_moveDirection);
			} 
		}
		if (Input.GetMouseButtonUp (0)) {
			anim.SetBool ("ShootBool", false);
		}
	}

	void LateUpdate()
	{
		// create follow-up camera
		Camera.main.transform.position = gameObject.transform.position + v3_cam_dir*cam_distance;
	}
}
