using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : MonoBehaviour 
{
	public float rotateSpeed;
	public SpriteRenderer sr;

	void Update ()
	{
		transform.eulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
	}

	//Called when the shield gets hit with a bullet.
	public void ShieldHit ()
	{
		Game.g.SpriteFlash(sr);
	}
}
