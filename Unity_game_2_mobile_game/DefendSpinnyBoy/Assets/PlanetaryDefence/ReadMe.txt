PLANETARY DEFENCE - COMPLETE UNITY PROJECT
-------------------------------------------
***Open in a text editor like MonoDevelop or VB. Not notepad.***

>>> About <<<

	Planetary Defence is a project that you can do to it, whatever you want. The basis
	of the game, is that you are a rocket defending a planet. There are 3 types of
	enemies and you need to shoot them down. You cannot win the game, as the aim is to
	have the highest time alive. Highscores get saved to player prefs.



>>> Project Features <<<

	- A complete Unity game ready for release.
	- Was made for desktop play, but can easily be transfered to mobile.
	- Easy to navigate project folders.
	- Documented code.
	- White sprites, so that they can easily be coloured to your choosing.



>>> Game Features <<<

	- 3 enemy types (basic enemy, enemy with shield, duplicating enemy).
	- 3 pickup types (speed fire, planet shield, turret).
	- Menu and game over screens.
	- Saved highscore (to the player prefs as "Highscore").
	- Great looking and color customizable sprites.
	- Audio for every action.
	- "game feel" or "juice".



>>> Tags & Sorting Layers <<

	>> TAGS <<

		- Bullet
			The player and turret's projectiles have this tag. It enters the colliders of
			enemies and pickups.
		- Enemy
			All 3 types of enemies have this tag. It collides with the planet.
		- Shield
			1 of the enemy ships has a shield, this collides with bullets.
		- Pickup
			All pickups have this tag. It collides with player bullets.

	>> Sorting Layers <<

		1. BG
			This is unnused currently in the game. Its purpose is to be for background 
			elements such as stars, asteroids, or other space detail.
		2. Bullet
			This is the player and turret's bullet projectiles.
		3. Planet
			This is the planet in the center of the screen.
		4. Enemy
			This is for the 3 different enemy types.
		5. Player
			This is for the rocket that the player controls.
		6. UI
			This is for all UI elements.



>>> Scripts <<<

	>> AudioManager.cs <<

		Manages all the audio in the game. Has functions that can be called, when an audio clip
		needs to be played.

	>> Bullet.cs <<

		Manages collisions with enemies and pickups. Destroys the bullet after a certain period
		of time and spawns a particle effect when the bullet collides with an object.

	>> CameraController.cs <<

		Moves the camera in the general direction that the rocket is facing. Also can be called
		upon to shake the screen.

	>> Enemy.cs <<

		Moves the enemy towards the planet. Manages damage taken, death and particle effect that
		spawns on death. Also manages collision with the planet, telling it to take damage.

	>> EnemySpawner.cs <<

		When the game is running, spawns enemies over time and at certain different rates, which
		are increased and determined in script.

	>> Game.cs <<

		Manages the overall game time, states, and general functions such as SpriteFlash, 
		enabling the turret and more.

	>> Pickup.cs <<

		Waits to get shot a few times before activating the pickup. Dies after some time, reducing
		in size until invisible.

	>> PickupSpawner.cs <<

		Spawns pickups over time, behind the player at random.

	>> Planet.cs <<

		Manages the planet's health and if it reaches 0 or below, ends the game.

	>> PlanetShield.cs <<

		Checks for collisions of enemies, killing them instantly.

	>> Rocket.cs <<

		The player's controller. It can rotate around the planet and shoot.

	>> ShipShield.cs <<

		Like the shield for the planet, but instantly destroys bullets.

	>> Turret.cs <<

		Rotates around the planet automatically, shooting in set intervals. Gets disabled after
		a certain amount of time.

	>> UI.cs <<

		Manages all the UI in the game. Menu, game and game over screens as well as button functions.




>>> Contact <<<

	Thank you very much for purchasing this asset, I hope it's useful to you in commercial use,
	learning use, or if you just want to check out something interesting. If you have any questions
	about the game, project or suggestions, you can contact me by...

	- Email: buckleydaniel101@gmail.com



	Thanks again and have fun.