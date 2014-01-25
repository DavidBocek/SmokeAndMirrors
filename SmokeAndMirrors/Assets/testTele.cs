using UnityEngine;
using System.Collections;

public class testTele : MonoBehaviour {

	public Transform TelePoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Vector3 newRot = transform.localEulerAngles;
		newRot.y += 180f;
		transform.position = TelePoint.position;
		transform.localEulerAngles = newRot;
	}
}
