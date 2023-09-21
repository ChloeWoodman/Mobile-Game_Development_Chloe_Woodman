using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{
	public float moveSpeed;
	public float attackRate;
	private float attackTimer;
	public float bulletSpeed;
	public float bulletSpread = 1.0f;
	public bool canMove;
	public Transform rocketSprite;
	public bool canHoldFire; //Can the player hold down space to fire?

	//Prefabs
	public GameObject bulletPrefab;

	//Components
	public Animator anim;

	public static Rocket r;

	void Awake () { r = this; }

	void Update ()
	{
		if(canMove)
			RotateRocket();

		if(!canHoldFire && Input.GetKeyDown(KeyCode.Space) && canMove)
		{
			if(attackTimer > attackRate)
			{
				attackTimer = 0.0f;
				Shoot();
			}
		}
		else if(canHoldFire && Input.GetKey(KeyCode.Space))
		{
			if(attackTimer > attackRate)
			{
				attackTimer = 0.0f;
				Shoot();
			}
		}

		attackTimer += Time.deltaTime;


		

		
		
	}

	//Rotates the rocket around the planet.
	void RotateRocket ()
	{
		transform.eulerAngles += new Vector3(0, 0, (-moveSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);
		rocketSprite.localEulerAngles = new Vector3(0, 0, Input.GetAxis("Horizontal") * -30);
	}






	//Shoots a bullet forward from the player.
	public void Shoot ()
	{
		GameObject bullet = Instantiate(bulletPrefab, rocketSprite.transform.position, transform.rotation);

		Vector2 dir = rocketSprite.transform.position.normalized * (bulletSpeed * Random.Range(1.0f, 1.1f));
		Vector3 offset = bullet.transform.right * Random.Range(-bulletSpread, bulletSpread);
		dir.x += offset.x;
		dir.y += offset.y;

		bullet.GetComponent<Rigidbody2D>().velocity = dir;

		if(canHoldFire)
		{
			for(int x = -1; x < 2; x++)
			{
				if(x != 0)
				{
					bullet = Instantiate(bulletPrefab, rocketSprite.transform.position, rocketSprite.rotation);

					dir = rocketSprite.transform.position.normalized * (bulletSpeed * Random.Range(1.0f, 1.1f));
					offset = bullet.transform.right * (x * 5 + Random.Range(-bulletSpread, bulletSpread));
					dir.x += offset.x;
					dir.y += offset.y;

					bullet.GetComponent<Rigidbody2D>().velocity = dir;
				}
			}
		}

		AudioManager.am.PlayShoot();
	}

	//EFFECTS

	//Allows the player to hold down the fire button.
	public void ActivateSpeedFire () { if(!canHoldFire) StartCoroutine(SpeedFireTimer()); }

	IEnumerator SpeedFireTimer ()
	{
		canHoldFire = true;
		yield return new WaitForSeconds(5.0f);
		canHoldFire = false;
	}
}
