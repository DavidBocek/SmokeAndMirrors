﻿using UnityEngine;
using System;
using System.Collections;

public class Open : MonoBehaviour {

	public float activateRadius;
	public Transform hingePoint;

	private bool opened = false;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<Transform>("PlayerActionButtonPressed",HandleActionButton);
	}

	void HandleActionButton(Transform playerTransform){
		if (Vector3.Distance(playerTransform.position,transform.position) <= activateRadius && !opened){
			StartCoroutine("DoorAnimation",playerTransform);
			opened = true;
		}
	}

	IEnumerator DoorAnimation(Transform trans){
		gameObject.GetComponent<BoxCollider>().enabled = false;

		float t = 0;

		//have the door move
		while (t<=1f){
			transform.RotateAround(hingePoint.position, Vector3.forward, 1.2f);
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
