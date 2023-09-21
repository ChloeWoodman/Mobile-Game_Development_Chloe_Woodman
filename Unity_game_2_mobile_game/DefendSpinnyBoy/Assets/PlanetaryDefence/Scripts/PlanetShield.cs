using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShield : MonoBehaviour 
{
	public SpriteRenderer sr;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			col.GetComponent<Enemy>().Die();
			Game.g.SpriteFlash(sr);
		}
	}
}
