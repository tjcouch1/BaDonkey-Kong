using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
	public int hp = 100;

	public bool hasKey = false;

	public float walkSpeed = 5.0f;
	public float runSpeed = 10.0f;
	public float jumpForce = 200f;
	public float lookSensitivity = 10f;

	public GameObject laserGun;
	public bool gun = false;
	public float shootSpeedCap = 1.2f;
	private float shootSpeed;
	private float shootDelayCap = .25f;
	private float shootDelay;

	private float moveSpeed;
	private Vector3 moveDir;// the actual moving direction

	private float idleTimeCap = 7;
	private float idleTime;

	private Vector3 forwardDir;//Forward Direction of the character
	private Vector3 rightDir;//Right Direction of the character

	// follow-up camera param
	private Vector3 cameraDir;
	private float camDistance;

	private Animator anim;

	private Rigidbody rb;

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
		rb = GetComponent<Rigidbody>();

		shootSpeed = shootSpeedCap;
		shootDelay = shootDelayCap;

		idleTime = idleTimeCap;
	}

	void Update()
	{
		anim.SetBool("Idle", false);
		idleTime -= Time.deltaTime;
		if (idleTime <= 0)
		{
			idleTime = idleTimeCap;
			anim.SetBool("Idle", true);
		}

		//Get forward direction of the character
		forwardDir = Camera.main.transform.TransformDirection(Vector3.forward);
		forwardDir.y = 0;
		forwardDir.Normalize();

		// Always orthogonal to the forward direction vector
		rightDir = Vector3.Cross(Vector3.up, forwardDir);
		rightDir.Normalize();

		if (hasGun())
			laserGun.GetComponent<Gun_Control>().setVisible(true);
		else laserGun.GetComponent<Gun_Control>().setVisible(false);

		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && hasGun() && !isShooting())
		{
			shootSpeed -= .01f;
			shootDelay -= .01f;
			anim.SetBool("ShootBool", true);
			if (Input.GetMouseButtonDown(0))
				moveDir = forwardDir;
		}

		if (isShooting())
		{
			//anim.SetBool("ShootBool", true);
			moveSpeed = 0;
			//rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);

			anim.SetInteger("Speed", 0);

			shootSpeed -= Time.deltaTime;
			if (shootSpeed <= 0)
				shootSpeed = shootSpeedCap;
			if (shootDelay < shootDelayCap)
				shootDelay -= Time.deltaTime;
			if (shootDelay <= 0)
			{
				shootDelay = shootDelayCap;
				laserGun.GetComponent<Gun_Control>().Fire(transform.TransformDirection(Vector3.forward));
				anim.SetBool("ShootBool", false);
			}
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
			{
				moveSpeed = 0.0f;
				anim.SetInteger("Speed", 0);//still
			}
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				moveSpeed = walkSpeed;
				anim.SetInteger("Speed", 1);//walk
			}
			else
			{
				moveSpeed = runSpeed;
				anim.SetInteger("Speed", 2);//run
			}

			transform.position += moveDir * moveSpeed * Time.deltaTime;
			//rigidbody.velocity = new Vector3(moveDir.x * moveSpeed * Time.deltaTime, rigidbody.velocity.y, moveDir.z * moveSpeed * Time.deltaTime);
			//rigidbody.AddForce(new Vector3(moveDir.x * moveSpeed * Time.deltaTime, 0, moveDir.z * moveSpeed * Time.deltaTime) * 50);

			//jump
			if (IsGrounded())
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					rb.AddForce(Vector3.up * jumpForce);
					anim.SetBool("IsJumping", true);
				}
				else
					anim.SetBool("IsJumping", false);
			}
			else
				anim.SetBool("IsJumping", false);
		}

		// update the rotation
		if (moveDir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(moveDir);
	}

	void LateUpdate()
	{
		//left and right
		Camera.main.transform.position = gameObject.transform.position + cameraDir * camDistance;
		Camera.main.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime);

		//up and down
		//constrain to -7.5 to 50
		//TODO: fix jitters
		Camera.main.transform.RotateAround(transform.position, Camera.main.transform.TransformDirection(Vector3.right), 
			Mathf.Clamp(Input.GetAxis("Mouse Y") * -lookSensitivity * Time.deltaTime, 0f - Camera.main.transform.localEulerAngles.x, 50f - Camera.main.transform.localEulerAngles.x));

		//detect direction rotating, if messes up from it, set it back to good transform
		/*GameObject cam = new GameObject();
		cam.transform.position = Camera.main.transform.position;
		cam.transform.rotation = Camera.main.transform.rotation;
		cam.transform.RotateAround(transform.position, Camera.main.transform.TransformDirection(Vector3.right), Input.GetAxis("Mouse Y") * -lookSensitivity * Time.deltaTime);
		if (cam.transform.localEulerAngles.x > -7.5 && cam.transform.localEulerAngles.x < 50)
		{
			Camera.main.transform.position = cam.transform.position;
			Camera.main.transform.rotation = cam.transform.rotation;
		}*/

		//Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.rotation.x, -7.5f, 50f), Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);
		cameraDir = Camera.main.transform.position - transform.position;
		cameraDir.Normalize();
		//Camera.main.transform.rotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X"));
		Camera.main.transform.position = gameObject.transform.position + cameraDir * camDistance;
	}

	public void Damage(int d)
	{
		hp -= d;

		if (hp <= 0)
			LoseGame();
	}

	public void LoseGame()
	{
		//TODO: Molly
		//SceneManager.LoadScene("Ded");
	}

	public void setHasKey(bool k)
	{
		hasKey = k;
	}

	private bool IsGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, 0.4f);
	}

	public bool hasGun()
	{
		return gun;
	}

	public void setGun(bool has)
	{
		gun = has;
	}

	public bool isShooting()
	{
		return shootSpeed < shootSpeedCap;
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Door") && hasKey)
		{
			col.gameObject.GetComponent<Door_Control>().Unlock();
		}
		else if (col.gameObject.CompareTag("Key"))
		{
			setHasKey(true);
			Destroy(col.gameObject);
		}
	}
}
