using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour 
{
	//Prefabs
	public GameObject speedFirePrefab;
	public GameObject planetShieldPrefab;
	public GameObject turretPrefab;

	public float pickupMinSpawnTime;
	public float pickupMaxSpawnTime;
	private float pickupSpawnTime;
	private float timer;

	void Start ()
	{
		pickupSpawnTime = Random.Range(pickupMinSpawnTime, pickupMaxSpawnTime);
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= pickupSpawnTime && Game.g.gameActive)
		{
			SpawnPickup();
			timer = 0.0f;
			pickupSpawnTime = Random.Range(pickupMinSpawnTime, pickupMaxSpawnTime);
		}
	}

	//Spawns a pickup in the game.
	void SpawnPickup ()
	{
		GameObject pickup = Instantiate(GetRandomPickup(), GetRandomPositon(), Quaternion.identity);
	}

	//Returns a random positon behind the player.
	Vector3 GetRandomPositon ()
	{
		Vector3 dir = Rocket.r.rocketSprite.transform.position.normalized;
		return -dir * Random.Range(8.0f, 15.0f);
	}

	//Returns a random pickup prefab.
	GameObject GetRandomPickup ()
	{
		float ranNum = Random.Range(1, 4);

		if(ranNum == 1)
			return speedFirePrefab;
		else if(ranNum == 2)
			return planetShieldPrefab;
		else
			return turretPrefab;
	}
}
