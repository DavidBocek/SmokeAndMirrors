using UnityEngine;
using System.Collections;

public class FlashlightManager : MonoBehaviour {

	public AudioClip flickerSound;

	private Light flashlight1;
	private Light flashlight2;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<float>("FlashlightFlicker",HandleFlashlightFlicker);
		flashlight1 = GetComponentsInChildren<Light>()[0];
		flashlight2 = GetComponentsInChildren<Light>()[1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleFlashlightFlicker(float duration){
		StartCoroutine("Flicker",duration);
	}

	IEnumerator Flicker(float duration){
		AudioSource.PlayClipAtPoint(flickerSound,transform.position);
		float originalIntensity1 = flashlight1.intensity;
		float originalIntensity2 = flashlight2.intensity;
		flashlight1.intensity = Random.Range (0.0f,originalIntensity1/10f);
		flashlight2.intensity = Random.Range (0.0f,originalIntensity2/10f);
		yield return new WaitForSeconds(.5f);
		flashlight1.intensity = originalIntensity1;
		flashlight2.intensity = originalIntensity2;
		yield return new WaitForSeconds(.75f);
		flashlight1.intensity = originalIntensity1/25f;
		flashlight2.intensity = originalIntensity2/25f;
		yield return new WaitForSeconds(.8f);
		flashlight1.intensity = originalIntensity1;
		flashlight2.intensity = originalIntensity2;
		yield return new WaitForSeconds(.3f);
		flashlight1.intensity = originalIntensity1/25f;
		flashlight2.intensity = originalIntensity2/25f;
		yield return new WaitForSeconds(.5f);
		flashlight1.intensity = originalIntensity1;
		flashlight2.intensity = originalIntensity2;
		yield return new WaitForSeconds(.6f);
		flashlight1.intensity = 0.0f;
		flashlight2.intensity = 0.0f;
		float t = 0;
		while (t<duration){
			t+=Time.smoothDeltaTime;
			yield return null;
		}
		flashlight1.intensity = originalIntensity1;
		flashlight2.intensity = originalIntensity2;
	}

}
