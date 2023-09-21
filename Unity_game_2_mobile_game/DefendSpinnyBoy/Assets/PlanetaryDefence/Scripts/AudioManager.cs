using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	//Audio Clips
	public AudioClip shoot;
	public AudioClip getPickup;
	public AudioClip enemyDeath;
	public AudioClip enemyHit;

	public AudioClip buttonHover;
	public AudioClip buttonClick;

	//Audio Source
	public AudioSource audioSource;

	public static AudioManager am;

	void Awake () { am = this; }

	//Plays the "shoot" sound effect, from the audio source.
	public void PlayShoot ()
	{
		audioSource.PlayOneShot(shoot);
	}

	//Plays the "getPickup" sound effect, from the audio source.
	public void PlayGetPickup ()
	{
		audioSource.PlayOneShot(getPickup);
	}

	//Plays the "enemyDeath" sound effect, from the audio source.
	public void PlayEnemyDeath ()
	{
		audioSource.PlayOneShot(enemyDeath);
	}

	//Plays the "enemyHit" sound effect, from the audio source.
	public void PlayEnemyHit ()
	{
		audioSource.PlayOneShot(enemyHit);
	}

	//Plays the "buttonHover" sound effect, from the audio source.
	public void PlayButtonHover ()
	{
		audioSource.PlayOneShot(buttonHover);
	}

	//Plays the "buttonClick" sound effect, from the audio source.
	public void PlayButtonClick ()
	{
		audioSource.PlayOneShot(buttonClick);
	}
}
