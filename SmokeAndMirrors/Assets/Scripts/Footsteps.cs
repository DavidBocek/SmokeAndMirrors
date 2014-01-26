using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	public AudioSource source;
	public AudioClip[] footstepSounds;
	public AudioClip[] grassFootstepSounds;
	public float walkFootstepTime;
	public float sprintFootstepTime;

	private float walkTimer;
	private float sprintTimer;
	private PlayerMovement playerMovement;
	private bool movedLastFrame;

	// Use this for initialization
	void Start () {
		playerMovement = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement>();
		walkTimer = walkFootstepTime;
		sprintTimer = sprintFootstepTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerMovement.sprinting){
			if (sprintTimer <= 0){
				PlaySound();
				sprintTimer = sprintFootstepTime;
			} else {
				sprintTimer -= Time.smoothDeltaTime;
			}
		} else if (playerMovement.moving) {
			if (walkTimer <= 0){
				PlaySound();
				walkTimer = walkFootstepTime;
			} else {
				walkTimer -= Time.smoothDeltaTime;
			}
		} else {
			if (walkTimer != walkFootstepTime) walkTimer = walkFootstepTime;
			if (sprintTimer != sprintFootstepTime) sprintTimer = sprintFootstepTime;
			//play a footstep if we just stopped moving
			if (movedLastFrame){
				PlaySound();
			}
		}
		movedLastFrame = playerMovement.sprinting || playerMovement.moving;
	}

	/// <summary>
	/// Plays a random footstep sound out of the array
	/// </summary>
	void PlaySound(){
		if (playerMovement.onGrass){
			source.PlayOneShot(grassFootstepSounds[Random.Range(0,grassFootstepSounds.Length-1)]);
		} else {
			source.PlayOneShot(footstepSounds[Random.Range(0,footstepSounds.Length-1)]);
		}
	}
	
}
