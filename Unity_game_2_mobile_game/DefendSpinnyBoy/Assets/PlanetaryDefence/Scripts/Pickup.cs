using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour 
{
	public int health;
	public SpriteRenderer sr;
	public PickupType type;
	public float timeAlive;

	void Update ()
	{
		timeAlive -= Time.deltaTime;

		if(timeAlive <= 0.0f)
			StartCoroutine(Shrink());
	}

	//Called when a bullet hits the pickup.
	public void TakeDamage (int dmg)
	{
		if(health - dmg <= 0)
		{
			ApplyPickup();
		}
		else
		{
			health -= dmg;
			transform.localScale += new Vector3(0.2f, 0.2f, 0.0f);
		}

		CameraController.c.Shake(0.3f, 0.5f, 50.0f);
		Game.g.SpriteFlash(sr);
	}

	//Called when the health reaches less than or equal to 0.
	public void ApplyPickup ()
	{
		switch(type)
		{
			case PickupType.Turret:
			{
				Game.g.SetTurret();
				break;
			}
			case PickupType.SpeedFire:
			{
				Rocket.r.ActivateSpeedFire();
				break;
			}
			case PickupType.PlanetShield:
			{
				Game.g.SetTempPlanetShield();
				break;
			}
		}

		AudioManager.am.PlayGetPickup();
		Destroy(gameObject);
	}

	//Slowly shrinks the pickup before it gets destroyed after it's lived out it's max time alive.
	IEnumerator Shrink ()
	{
		//Scale it down overtime, then destroy it.
		while(transform.localScale.x > 0.0f)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 0.5f);
			yield return null;
		}

		Destroy(gameObject);
	}
}

public enum PickupType { Turret, SpeedFire, PlanetShield }
