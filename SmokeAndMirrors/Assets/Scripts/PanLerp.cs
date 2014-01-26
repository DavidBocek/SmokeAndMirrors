using UnityEngine;
using System.Collections;

public class PanLerp : MonoBehaviour {


	public float stepPerFrame;	//controls lerp speed

	private AudioSource audioSource;
	private float t = 0;
	private bool increasing = true;
	private bool decreasing = false;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		audioSource.panLevel = Mathf.Lerp(0.0f,1.0f,t);
		if (increasing){
			if (t>=1){
				increasing = false;
				decreasing = true;
				t = 1;
			}
			t += stepPerFrame;
		} else if (decreasing) {
			if (t<=0){
				increasing = true;
				decreasing = false;
				t = 0;
			}
			t -= stepPerFrame;
		}
	}
}
