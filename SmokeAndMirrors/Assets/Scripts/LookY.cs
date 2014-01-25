using UnityEngine;
using System.Collections;

public class LookY : MonoBehaviour {
	public float sensitivityY;
	public bool inverted;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	void LateUpdate () {
		if (!GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().canControl) return;
		//adjust vertical rotation
		float curRotX = transform.eulerAngles.x;
		float xAngleAdjust = inverted ? Input.GetAxis("Mouse Y") : -Input.GetAxis ("Mouse Y");
		curRotX += xAngleAdjust * sensitivityY;
		if (curRotX < 275f && curRotX > 180f){curRotX = 275.0f;}
		else if (curRotX > 85f && curRotX < 180f){curRotX = 85.0f;}
		//input new rotation
		transform.localEulerAngles = new Vector3(curRotX,0,0);
	}
}
