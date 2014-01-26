using UnityEngine;
using System.Collections;

public class PlayBangingNoiseLR2 : MonoBehaviour {

	public AudioSource bangingSoundSource;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<GameObject>("TriggerHit",HandleTrigger);
	}
	
	void HandleTrigger(GameObject whatWasHit){
		if (gameObject == whatWasHit){
			bangingSoundSource.Play();
		}
	}
}
