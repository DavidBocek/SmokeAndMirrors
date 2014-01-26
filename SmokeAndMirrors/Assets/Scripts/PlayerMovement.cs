using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float walkSpeed;
	public float runSpeed;
	public float strafeSpeed;
	public float gravityAcceleration;
	public float mouseXSensitivity;
	public float maxFallSpeed;
	public bool sprinting {get; set;}
	public bool moving {get; set;}
	public bool onGrass {get; set;}
	public bool canControl {get; set;}

	private Vector3 velocity;
	private CharacterController controller;

	private float dt;
	// Use this for initialization
	void Start () {
		onGrass = true;
		canControl = true;
		controller = GetComponent<CharacterController>();
		velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
		if (canControl){
			UpdateInput();
			UpdateGravity();
			UpdatePositionAndRotation();
		} else {
			moving = false;
			sprinting = false;
		}
	}

	void UpdateInput(){
		if (Input.GetButtonDown("Action")){
			Messenger.Broadcast<Transform>("PlayerActionButtonPressed",transform);
		}
	}

	void UpdateGravity(){
		if (!controller.isGrounded){
			velocity.y -= gravityAcceleration * dt;
			velocity.y = Mathf.Max (velocity.y,maxFallSpeed);
		} /*if (Input.GetButtonDown("Jump") && controller.isGrounded){
			velocity.y = jumpImpulse;
		}*/
	}

	void UpdatePositionAndRotation(){
		sprinting = Input.GetButton ("Run");
		velocity.z = Input.GetButton("Run") ? Input.GetAxis ("Vertical") * runSpeed : Input.GetAxis ("Vertical") * walkSpeed;
		velocity.x = Input.GetAxis ("Horizontal") * strafeSpeed;

		Vector3 rotation = transform.localEulerAngles;
		rotation.y += Input.GetAxis("Mouse X")*mouseXSensitivity;
		velocity = transform.TransformDirection(velocity);

		transform.localEulerAngles = rotation;

		Vector2 xzVel = new Vector2(velocity.x,velocity.z);
		moving = xzVel.sqrMagnitude >= 1f;

		controller.Move(velocity*dt);
	}

	void OnTriggerEnter(Collider other){
		Messenger.Broadcast("TriggerHit",other.gameObject);
	}
}
