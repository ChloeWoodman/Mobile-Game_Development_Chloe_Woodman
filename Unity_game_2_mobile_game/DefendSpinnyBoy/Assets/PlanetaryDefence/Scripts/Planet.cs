using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{
	public int health;

	//Prefabs
	public GameObject deathParticleEffect;

	//Components
	public SpriteRenderer sr;

	public static Planet p;

	void Awake () { p = this; }

	//Called when a ship hits the planet.
	public void TakeDamage (int dmg)
	{
		//If the health is less than or equal to 0, then end the game.
		if(health - dmg <= 0)
		{
			Game.g.EndGame();

			//Create the explosion particle effect.
			GameObject pe = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
			Destroy(pe, 2.0f);
		}
		else
			health -= dmg;

		CameraController.c.Shake(0.3f, 0.5f, 50.0f);
		Game.g.SpriteFlash(sr);
		UI.ui.SetPlanetHealthBarValue(health);
	}
}
