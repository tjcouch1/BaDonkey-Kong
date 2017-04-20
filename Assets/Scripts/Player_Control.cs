using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
	public float walkSpeed = 5.0f;
	public float runSpeed = 10.0f;
	public float jumpForce = 200f;
	public float lookSensitivity = 10f;

	private float moveSpeed;
	private Vector3 moveDir;// the actual moving direction

	private Vector3 forwardDir;//Forward Direction of the character
	private Vector3 rightDir;//Right Direction of the character

	// follow-up camera param
	private Vector3 cameraDir;
	private float camDistance;

	private Animator anim;

	private Rigidbody rigidbody;

	void Start()
	{
		moveSpeed = 0;
		moveDir = Vector3.zero;
		forwardDir = Vector3.zero;
		rightDir = Vector3.zero;

		cameraDir = Camera.main.transform.position - transform.position;
		camDistance = cameraDir.magnitude;
		cameraDir.Normalize();

		anim = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		//Get forward direction of the character
		forwardDir = Camera.main.transform.TransformDirection(Vector3.forward);
		forwardDir.y = 0;
		forwardDir.Normalize();

		// Always orthogonal to the forward direction vector
		rightDir = Vector3.Cross(Vector3.up, forwardDir);
		rightDir.Normalize();

		if (Input.GetMouseButton(0))
		{
			anim.SetBool("ShootBool", true);
			moveDir = forwardDir;
			moveSpeed = 0;
			//rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}
		else//update the position if not shooting
		{
			anim.SetBool("ShootBool", false);

			float moveHorizontal = 0;
			if (Input.GetKey(KeyCode.A))
				moveHorizontal -= 1;
			if (Input.GetKey(KeyCode.D))
				moveHorizontal += 1;
			float moveVertical = 0;
			if (Input.GetKey(KeyCode.S))
				moveVertical -= 1;
			if (Input.GetKey(KeyCode.W))
				moveVertical += 1;

			//Get the move direction
			moveDir = (moveHorizontal * rightDir) + (moveVertical * forwardDir);
			moveDir.Normalize();

			// Calculate actual movement
			if (moveDir.magnitude < 0.01 && moveDir.magnitude > -0.01)
				moveSpeed = 0.0f;
			else if (Input.GetKey(KeyCode.LeftShift))
				moveSpeed = walkSpeed;
			else moveSpeed = runSpeed;

			transform.position += moveDir * moveSpeed * Time.deltaTime;
			//rigidbody.velocity = new Vector3(moveDir.x * moveSpeed * Time.deltaTime, rigidbody.velocity.y, moveDir.z * moveSpeed * Time.deltaTime);
			//rigidbody.AddForce(new Vector3(moveDir.x * moveSpeed * Time.deltaTime, 0, moveDir.z * moveSpeed * Time.deltaTime) * 50);

			//jump
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rigidbody.AddForce(Vector3.up * jumpForce);
			}
		}

		// update the rotation
		if (moveDir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(moveDir);

		anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
	}

	void LateUpdate()
	{
		//left and right
		Camera.main.transform.position = gameObject.transform.position + cameraDir * camDistance;
		Camera.main.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime);

		//up and down
		//constrain to -7.5 to 50
		//detect direction rotating, if messes up from it, set it back to good transform
		GameObject cam = new GameObject();
		cam.transform.position = Camera.main.transform.position;
		cam.transform.rotation = Camera.main.transform.rotation;
		cam.transform.RotateAround(transform.position, Camera.main.transform.TransformDirection(Vector3.right), Input.GetAxis("Mouse Y") * -lookSensitivity * Time.deltaTime);
		//Camera.main.transform.RotateAround(transform.position, Camera.main.transform.TransformDirection(Vector3.right), Input.GetAxis("Mouse Y") * -lookSensitivity * Time.deltaTime);
		if (cam.transform.localEulerAngles.x > -7.5 && cam.transform.localEulerAngles.x < 50)
		{
			Camera.main.transform.position = cam.transform.position;
			Camera.main.transform.rotation = cam.transform.rotation;
		}

		//Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.rotation.x, -7.5f, 50f), Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);
		cameraDir = Camera.main.transform.position - transform.position;
		cameraDir.Normalize();
		//Camera.main.transform.rotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X"));
		Camera.main.transform.position = gameObject.transform.position + cameraDir * camDistance;
	}
}
