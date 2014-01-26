using UnityEngine;
using System.Collections;

public class PlayBangingNoiseLR2 : MonoBehaviour {

	public AudioSource bangingSoundSource;
	private bool hasPlayed;
	public bool useHasPlayed;
	public float delay = 0;

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
			if (delay == 0)
				bangingSoundSource.Play();
			else
				bangingSoundSource.PlayDelayed(delay);
		}
	}
}
