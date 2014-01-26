using UnityEngine;
using System.Collections;

public class PlayFlicker : MonoBehaviour {
	

	public float durationOfFlicker;
	public bool useHasPlayed;

	private bool hasPlayed;
	
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
			Messenger.Broadcast<float>("FlashlightFlicker",durationOfFlicker);
		}
	}
}
