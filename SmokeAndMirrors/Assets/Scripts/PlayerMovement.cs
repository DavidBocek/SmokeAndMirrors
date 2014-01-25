using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float walkSpeed;
	public float runSpeed;
	public float strafeSpeed;
	public float gravityAcceleration;
	public float mouseXSensitivity;
	public float maxFallSpeed;
	
	private Vector3 velocity;
	private CharacterController controller;

	private float dt;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
		UpdateGravity();
		UpdatePositionAndRotation();
	}

	void UpdateGravity(){
		if (!controller.isGrounded){
			velocity.y -= gravityAcceleration * dt;
			velocity.y = Mathf.Max (velocity.y,maxFallSpeed);
		}
	}

	void UpdatePositionAndRotation(){
		velocity.z = Input.GetButton("Run") ? Input.GetAxis ("Vertical") * runSpeed : Input.GetAxis ("Vertical") * walkSpeed;
		velocity.x = Input.GetAxis ("Horizontal") * strafeSpeed;

		Vector3 rotation = transform.localEulerAngles;
		rotation.y += Input.GetAxis("Mouse X")*mouseXSensitivity;
		velocity = transform.TransformDirection(velocity);

		transform.localEulerAngles = rotation;
		controller.Move(velocity*dt);
	}
}
