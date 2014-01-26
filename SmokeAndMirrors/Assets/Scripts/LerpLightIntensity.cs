using UnityEngine;
using System.Collections;

public class LerpLightIntensity : MonoBehaviour {

	public float stepPerFrame;	//controls lerp speed
	public float lowIntensity;
	public float highIntensity;

	private Light light;
	private float t = 0;
	private bool increasing = true;
	private bool decreasing = false;
	
	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = Mathf.Lerp(lowIntensity,highIntensity,t);
		if (increasing){
			if (t>=highIntensity){
				increasing = false;
				decreasing = true;
				t = highIntensity;
			}
			t += stepPerFrame;
		} else if (decreasing) {
			if (t<=lowIntensity){
				increasing = true;
				decreasing = false;
				t = lowIntensity;
			}
			t -= stepPerFrame;
		}
	}
}
