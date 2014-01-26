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
	public AudioClip openClip;
	public AudioClip closeClip;
	public AudioClip lerpInSound;

	public Transform teleportLocationTransform;
	public Material lerpMaterial;

	private float origR;
	private float origG;
	private float origB;

	private Action DoOnDoorFinish = DoNothing;
	private GameObject player;
	private Vector3 originalEulerAngles;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<Transform>("PlayerActionButtonPressed",HandleActionButton);
		var mtd = GetType().GetMethod(actionFunctionName,System.Reflection.BindingFlags.Instance 
		                              | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod);
		if (mtd != null){
			DoOnDoorFinish = (Action) Delegate.CreateDelegate(typeof(Action),this,mtd);
		}
		player = GameObject.FindWithTag("Player");
		if (lerpMaterial != null){
			origR = lerpMaterial.color.r; origG = lerpMaterial.color.g; origB = lerpMaterial.color.b;
			lerpMaterial.color = new Color(origR,origG,origB,0.0f);
		}
	}

	static void DoNothing(){}


	void HandleActionButton(Transform playerTransform){
		if (Vector3.Distance(playerTransform.position,transform.position) <= activateRadius && 
		    Vector3.Distance(playerTransform.position,startAnimationTransform.position) < 
		    Vector3.Distance(playerTransform.position,finishAnimationTransform.position)){

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
		AudioSource.PlayClipAtPoint (openClip, transform.position);
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
		AudioSource.PlayClipAtPoint (closeClip, transform.position);
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

	void CloseDoorAndTeleportAndTurnOffGrass(){
		StartCoroutine("cCloseDoor",false);
		StartCoroutine("cTeleport",true);
		player.GetComponent<PlayerMovement>().onGrass = false;
	}

	void CloseDoorAndTeleportAndSpawnTexture(){
		StartCoroutine("cCloseDoor",false);
		StartCoroutine("cTeleport",true);
		StartCoroutine("cLerpInTexture");
		AudioSource.PlayClipAtPoint(lerpInSound,player.transform.position);
	}

	IEnumerator cLerpInTexture(){
		float t = 0;
		float newAlpha = 0;
		while (t<=1){
			newAlpha += .025f;
			lerpMaterial.color = new Color(origR,origG,origB,newAlpha);
			t += .025f;
			yield return null;
		}
	}

	void CloseDoorAndEnd(){
		StartCoroutine("cCloseDoor",false);
		StartCoroutine("cEndGame");
	}

	IEnumerator cEndGame(){
		float t = 0;
		drawText = true;
		while (t<=1f){
			Camera.main.fieldOfView += t*.17f;
			t += .005f;
			yield return null;
		}
		player.gameObject.GetComponent<PlayerMovement>().canControl = false;
		yield return new WaitForSeconds(15f);
		Application.Quit();
	}

	#endregion
	private bool drawText = false;
	void OnGUI(){
		if (drawText){
			GUI.TextField(new Rect(Screen.width/2,Screen.height/2,80f,20f),"KTHNXBAI");
		}
	}
}
