using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public int health;
	public float moveSpeed;
	public int damage;
	public bool stunned;
	public EnemyType type;
	GameObject gamemanager;
	Game gameScript;

	//Prefabs
	public GameObject deathParticleEffect;
	public GameObject duplicatePrefab;
	public GameObject PartyEffect;
	

	//Components
	public SpriteRenderer sr;

	void Start ()
	{
		gamemanager = GameObject.Find("_GameManager");
		gameScript = gamemanager.GetComponent<Game>();
		
		//If the enemy is yellow, randomly rearrange the 2nd shield.
		if(type == EnemyType.Yellow)
		{
			GameObject shield2 = transform.Find("Shield2").gameObject;
			int ranNum = Random.Range(1, 5);

			if(ranNum == 1)
				shield2.transform.localEulerAngles = new Vector3(0, 0, 89);
			if(ranNum == 2)
				shield2.transform.localEulerAngles = new Vector3(0, 0, -180);
			if(ranNum >= 3)
				shield2.SetActive(false);
		}

		//Add a tiny bit of variation to the move speed.
		moveSpeed *= Random.Range(0.9f, 1.1f);
	}

	void Update ()
	{
		//Move to planet.
		if(!stunned && Game.g.gameActive)
			transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, moveSpeed * Time.deltaTime);

		LookAtPlanet();

		
	}

	//Makes it so that the ship is always looking at the planet.
	void LookAtPlanet ()
	{
		Vector3 dir = transform.position.normalized;
		float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
		transform.eulerAngles = new Vector3(0, 0, ang);
	}

	//Called when a bullet hits the ship.
	public void TakeDamage (int dmg)
	{
		//If the health is less than or equals to 0, then die.
		if(health - dmg <= 0)
			Die();
		else
		{
			health -= dmg;
			Game.g.Stun(this);
			AudioManager.am.PlayEnemyHit();
		}

		Game.g.SpriteFlash(sr);
	}

	//Called when health reaches less than or equal to 0.
	public void Die ()
	{
		if (gameScript.gameTime >= 30.0f)
		{
			GameObject pe = Instantiate(PartyEffect, transform.position, Quaternion.identity);
			Destroy(pe, 2.0f);

			if (type == EnemyType.Blue)
				Duplicate();

			AudioManager.am.PlayEnemyDeath();
			Destroy(gameObject);
		}
		else
		{

			GameObject pe = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
			Destroy(pe, 2.0f);

			if (type == EnemyType.Blue)
				Duplicate();

			AudioManager.am.PlayEnemyDeath();
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Planet")
		{
			if(!Game.g.planetShield.activeInHierarchy)
			{
				Planet.p.TakeDamage(damage);
				Die();
			}
		}
	}

	//Called by the blue ship. Duplicates the ship.
	void Duplicate ()
	{
		GameObject e1 = Instantiate(duplicatePrefab, transform.position + (transform.up * -2), Quaternion.identity);
		GameObject e2 = Instantiate(duplicatePrefab, transform.position + (transform.right * 2), Quaternion.identity);
		GameObject e3 = Instantiate(duplicatePrefab, transform.position + (transform.right * -2), Quaternion.identity);
	}
}

public enum EnemyType { Red, Yellow, Blue, Green }
