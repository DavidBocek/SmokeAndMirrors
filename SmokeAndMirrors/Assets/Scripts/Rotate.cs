using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
	public Transform pivotPoint;
	public float degPerFrame;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround (pivotPoint.position, Vector3.up, degPerFrame);
	}
}
