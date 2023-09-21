using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour 
{

	
	public float gameTime;
	public float gameTimeHighscore;
	public bool gameActive;

	public GameObject planetShield;
	public GameObject turret;

	public static Game g;

	void Awake () { g = this; }

	void Start ()
	{
		//Setting stuff up so the camera is set to menu view.
		CameraController.c.SetMenuView();
		UI.ui.SetMenuUI();
		UI.ui.gameUI.SetActive(false);

		//Getting the highscore.
		gameTimeHighscore = PlayerPrefs.GetFloat("Highscore");
	}

	void Update ()
	{
		if(gameActive)
			gameTime += Time.deltaTime;
	}

	//Called when the game begins.
	public void StartGame ()
	{
		gameActive = true;
		gameTime = 0.0f;
		Rocket.r.canMove = true;
		UI.ui.SetGameUI();
	}

	//Called when the planet's health reaches 0. Ends the game.
	public void EndGame ()
	{
		if(gameActive)
		{
			gameActive = false;
			Rocket.r.canMove = false;
			UI.ui.gameUI.SetActive(false);
			UI.ui.SetGameOverUI();

			//Disabling the planet and rocket sprites.
			Rocket.r.rocketSprite.gameObject.SetActive(false);
			Planet.p.gameObject.SetActive(false);
		}
	}

	//Sets the gameTime as the highscore.
	public void SetTimeAsHighscore ()
	{
		PlayerPrefs.SetFloat("Highscore", gameTime);
	}

	//Calls a coroutine to flash the sent sprite renderer white.
	public void SpriteFlash (SpriteRenderer sr)
	{
		StartCoroutine(sf(sr));
	}

	//Flashes the sent sprite renderer white for 0.05 seconds.
	IEnumerator sf (SpriteRenderer sr)
	{
		if(sr.color != Color.white)
		{
			Color defaultColour = sr.color;
			sr.color = Color.white;

			yield return new WaitForSeconds(0.05f);

			if(sr != null)
				sr.color = defaultColour;
		}
	}

	//Calls a coroutine to stun the sent enemy.
	public void Stun (Enemy enemy)
	{
		StartCoroutine(st(enemy));
	}

	//Stuns the sent enemy for 0.02 seconds.
	IEnumerator st (Enemy enemy)
	{
		enemy.stunned = true;

		yield return new WaitForSeconds(0.02f);

		if(enemy != null)
			enemy.stunned = false;
	}

	//Enables the planet shield for a few seconds.
	public void SetTempPlanetShield ()
	{
		planetShield.SetActive(true);
		StartCoroutine(ps());
	}

	IEnumerator ps ()
	{
		yield return new WaitForSeconds(8.0f);
		planetShield.SetActive(false);
	}

	//Enables the turret object.
	public void SetTurret ()
	{
		if(!turret.activeInHierarchy)
		{
			turret.SetActive(true);
			turret.transform.eulerAngles = Vector3.zero;
		}
	}
}
