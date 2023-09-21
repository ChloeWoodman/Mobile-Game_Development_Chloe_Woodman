using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{
	public float moveSpeed;
	public Transform turretSprite;
	public float attackRate;
	private float attackTimer;
	public float bulletSpeed;
	public float bulletSpread = 1.0f;
	public float timeAlive;
	private float timeAliveTimer;

	//Prefabs
	public GameObject bulletPrefab;

	void OnEnable ()
	{
		timeAliveTimer = 0.0f;
		attackTimer = 0.0f;
	}

	void Update ()
	{
		//If the game is running, then rotate around the planet and shoot.
		if(Game.g.gameActive)
		{
			RotateTurret();

			if(attackTimer >= attackRate)
			{
				attackTimer = 0.0f;
				Shoot();
			}
		}

		//Once the turret has been active for time alive, disable it.
		if(timeAliveTimer >= timeAlive)
		{
			gameObject.SetActive(false);
		}

		attackTimer += Time.deltaTime;
		timeAliveTimer += Time.deltaTime;
	}

	//Rotates the turret around the planet.
	void RotateTurret ()
	{
		transform.eulerAngles += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
	}

	//Shoots a bullet forward from the turret.
	void Shoot ()
	{
		GameObject bullet = Instantiate(bulletPrefab, turretSprite.transform.position, transform.rotation);

		Vector2 dir = turretSprite.transform.position.normalized * (bulletSpeed * Random.Range(1.0f, 1.1f));
		Vector3 offset = bullet.transform.right * Random.Range(-bulletSpread, bulletSpread);
		dir.x += offset.x;
		dir.y += offset.y;

		bullet.GetComponent<Rigidbody2D>().velocity = dir;
	}
}
