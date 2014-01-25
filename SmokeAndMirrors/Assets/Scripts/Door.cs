using UnityEngine;
using System;
using System.Collections;

public class Door : MonoBehaviour {

	public float activateRadius;
	public string actionFunctionName;
	public Transform startAnimationTransform;
	public Transform finishAnimationTransform;
	public Transform hingePoint;
	public Vector3 initialRotation;

	public Transform teleportLocationTransform;

	private Action DoOnDoorFinish = DoNothing;
	private GameObject player;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<Transform>("PlayerActionButtonPressed",HandleActionButton);
		var mtd = GetType().GetMethod(actionFunctionName,System.Reflection.BindingFlags.Instance 
		                              | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod);
		if (mtd != null){
			DoOnDoorFinish = (Action) Delegate.CreateDelegate(typeof(Action),this,mtd);
		}
		player = GameObject.FindWithTag("Player");
	}

	static void DoNothing(){}


	void HandleActionButton(Transform playerTransform){
		if (Vector3.Distance(playerTransform.position,transform.position) <= activateRadius){
			StartCoroutine("DoorAnimation",playerTransform);
		}
	}


	IEnumerator DoorAnimation(Transform trans){
		gameObject.GetComponent<BoxCollider>().enabled = false;
		Vector3 endPointPosition = finishAnimationTransform.position;

		trans.gameObject.GetComponent<PlayerMovement>().canControl = false;
		float t = 0;
		float tRot = 0;
		//lerp to start point
		while (t<=1f){
			Quaternion newRot = Quaternion.Lerp(trans.rotation,startAnimationTransform.rotation,t);
			trans.rotation = newRot;
			Vector3 newPos = Vector3.Lerp(trans.position,startAnimationTransform.position,t);
			trans.position = newPos;
			t += .01f;
			yield return null;
		}
		t = 0;
		yield return new WaitForSeconds(.5f);
		//move through door to end position, and have the door move
		while (t<=1f){
			if (tRot <=.75){
				transform.RotateAround(hingePoint.position, Vector3.up,1.2f);
			}
			if (tRot >= .5){
				Vector3 newPos = Vector3.Lerp(trans.position,endPointPosition,t);
				trans.position = newPos;
				t += .01f;
			} 
			tRot += .01f;
			yield return null;
		}

		gameObject.GetComponent<BoxCollider>().enabled = true;
		//activate ending action
		DoOnDoorFinish();
	}

	#region finishing actions

	void CloseDoor(){
		StartCoroutine("cCloseDoor",true);
	}
	IEnumerator cCloseDoor(bool getControl){
		//play close sound
		transform.RotateAround(hingePoint.position,Vector3.up,-90f);
		transform.localEulerAngles = initialRotation;
		yield return new WaitForSeconds(.5f);
		if (getControl) player.GetComponent<PlayerMovement>().canControl = true;
		yield return null;
	}

	void CloseDoorAndTeleport(){
		StartCoroutine("cCloseDoor",false);
		StartCoroutine("cTeleport",true);
	}
	IEnumerator cTeleport(bool getControl){
		player.transform.position = teleportLocationTransform.position;
		player.transform.rotation = teleportLocationTransform.rotation;
		if (getControl) player.GetComponent<PlayerMovement>().canControl = true;
		yield return null;
	}

	#endregion
}
