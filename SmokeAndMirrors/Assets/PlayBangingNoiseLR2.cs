using UnityEngine;
using System.Collections;

public class PlayBangingNoiseLR2 : MonoBehaviour {

	public AudioSource bangingSoundSource;
	private bool hasPlayed;
	public bool useHasPlayed;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<GameObject>("TriggerHit",HandleTrigger);
		hasPlayed = false;
	}
	
	void HandleTrigger(GameObject whatWasHit){
		if (gameObject == whatWasHit && !hasPlayed)
		{
			if(useHasPlayed)
			{
				hasPlayed = true;
			}
			bangingSoundSource.Play();
		}
	}
}
