using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public float moveDist;
	public float lerpSpeed;

	private bool shakingCam;

	//Camera
	public Vector3 menuCameraPos;
	public float menuOrthoSize;
	public float gameOrthoSize;

	public static CameraController c;

	void Awake () { c = this; }

	void LateUpdate ()
	{
		//If the game is running move the camera a bit in the player's direction.
		if(Game.g.gameActive)
		{
			transform.position = Vector3.Lerp(transform.position, Rocket.r.rocketSprite.transform.position * moveDist, lerpSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		}
	}

	//Creates a camera shake from 3 different variables.
	public void Shake (float duration, float amount, float intensity)
	{
		if(!shakingCam)
			StartCoroutine(ShakeCam(duration, amount, intensity));
	}

	IEnumerator ShakeCam (float dur, float amount, float intensity)
	{
		float t = dur;
		Vector3 originalPos = Camera.main.transform.localPosition;
		Vector3 targetPos = Vector3.zero;
		shakingCam = true;

		while(t > 0.0f)
		{
			if(targetPos == Vector3.zero)
			{
				targetPos = originalPos + (Random.insideUnitSphere * amount);
			}

			Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPos, intensity * Time.deltaTime);

			if(Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
			{
				targetPos = Vector3.zero;
			}

			t -= Time.deltaTime;
			yield return null;
		}

		Camera.main.transform.localPosition = originalPos;
		shakingCam = false;
	}

	//Called at the start of the game, to have the camera zoom in for the menu.
	public void SetMenuView ()
	{
		Camera.main.transform.position = menuCameraPos;
		Camera.main.orthographicSize = menuOrthoSize;
	}

	//Transitions the camera out to see the whole game view.
	public void TransitionToGameView ()
	{
		StartCoroutine(gv());
	}

	IEnumerator gv ()
	{
		//Move the camera to the center of the planet.
		while(Camera.main.transform.position.x < 0.0f)
		{
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(0, 0, -10), 8 * Time.deltaTime);
			yield return null;
		}

		yield return new WaitForSeconds(0.3f);

		//Zoom out to see the game space.
		while(Camera.main.orthographicSize < gameOrthoSize)
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, gameOrthoSize, 20 * Time.deltaTime);
			yield return null;
		}

		Game.g.StartGame();
	}
}
