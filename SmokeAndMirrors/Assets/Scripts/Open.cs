using UnityEngine;
using System;
using System.Collections;

public class Open : MonoBehaviour {

	public float activateRadius;
	public Transform hingePoint;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<Transform>("PlayerActionButtonPressed",HandleActionButton);
		player = GameObject.FindWithTag("Player");
	}

	void HandleActionButton(Transform playerTransform){
		if (Vector3.Distance(playerTransform.position,transform.position) <= activateRadius){
			StartCoroutine("DoorAnimation",playerTransform);
		}
	}

	IEnumerator DoorAnimation(Transform trans){
		gameObject.GetComponent<BoxCollider>().enabled = false;

		float t = 0;

		//have the door move
		while (t<=1f){
			transform.RotateAround(hingePoint.position, Vector3.left, .9f);
			t += .01f;
			yield return null;
		}
		
		gameObject.GetComponent<BoxCollider>().enabled = true;
		//activate ending action
		//DoOnDoorFinish();
	}
	
	#region finishing actions

	
	#endregion
}
