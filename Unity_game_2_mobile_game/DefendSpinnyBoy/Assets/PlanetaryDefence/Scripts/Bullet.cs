using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public int damage; //Most of the time 1.
	public GameObject hitParticleEffect;

	void Start ()
	{
		Destroy(gameObject, 2.0f);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		//If the bullet hits an enemy.
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
			SpawnParticleEffect();
			Destroy(gameObject);
		}

		//If the bullet hits a shield.
		else if(col.gameObject.tag == "Shield")
		{
			col.gameObject.GetComponent<ShipShield>().ShieldHit();
			SpawnParticleEffect();
			Destroy(gameObject);
		}

		//If the bullet hits a pickup.
		else if(col.gameObject.tag == "Pickup")
		{
			col.gameObject.GetComponent<Pickup>().TakeDamage(damage);
			SpawnParticleEffect();
			Destroy(gameObject);
		}

		CameraController.c.Shake(0.1f, 0.25f, 30.0f);
	}

	//Called when the bullet hits an object. Spawns the particle effect.
	void SpawnParticleEffect ()
	{
		GameObject pe = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
		pe.transform.LookAt(Planet.p.transform);
		Destroy(pe, 2.0f);
	}
}
