# Mobile-Game_Development_Chloe_Woodman
 Version control for the development of my mobile game, **Mouse Meadow Mischief**.

 # Designs and asssets

 Concept design for game BETA
 
![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/810d98f3-f472-4ef5-8452-e2d608ba1f95)

 
 Key ideas:
Use a microphone for picking up audio to enable the dandelion to blow seeds to enable glide mechanics.
2 inputs, a touch screen, and on-screen joysticks
The game will be developed for Android due to the overall world having more Androids than iPhones; Android is universally accessible across multiple phone brands; and despite the USA and UK having more iPhone users, most of Europe consists of Android users, thus ensuring the game will be available to a large audience.
Framework "Snapshots" API will be used to save data and progress externally to a server - details provided here https://developer.android.com/games/pgs/android/saved-games; this API provides a convenient method to save players' game progress to the Google Server, which can retrieve saved game data to allow players to return to continue playing their game at their last save point from any device - which means that they can have their progress even if their phone is dead or broken, they could even play the game on a tablet without losing nay progress and starting anew! 
Monetization can be given by completing a level and watching an ad to unlock another.

# White box
Start Menu, with actions button, play game button, and quit button; options will open the options menu (which contains a back button to return to the start menu); play button moves the player onto the main (game) scene; and quit button will close the program.

![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/920383e1-4d3c-43b5-8edd-187dbbda0fcd) ![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/c2ee8fa7-060e-4d28-97a3-caac4d00f336)


The white box design of a simple level of the game with a yellow flower as the goal, UI (hearts to represent lives, joystick for movement, jump button, options button, attack button, and score number that goes down -10 every second), flying enemies (bees), and ground enemies (snakes) can lose if all 3 lives represented as hearts are gone or if the score =< 0:

![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/424506da-cd8d-43c1-8bea-8296ad2b4047)

If the player presses the top left corner yellow button, this will open the pause menu that pauses the game and gives you a resume game button that unpauses and resumes the game, an actions menu, and an exit button that returns to the start menu.

![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/ee4dc74e-91b2-42f6-810d-d315043b6dcc)


The ad will pop up after the main menu moves to a win-or-lose scene.

![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/c65a3892-5444-417d-a22c-e74af96e4806)

The Win screen will load when the player reaches the goal, with a button to restart the game by reloading the main (game) scene, output the player score, and exit the game, which returns to the start menu:

![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/0674f3ae-02cd-4866-891e-c9983dd9d829)


Game over screen will be shown when the player *either* has no lives left or the score =< 0, this screen can reload the main (game) scene to restart the game, or an exit button to return to the start menu.

![image](https://github.com/ChloeWoodman/Mobile-Game_Development_Chloe_Woodman/assets/113985493/08b8d0d1-c6a9-4443-93b6-4283b1e6623b)

# Frameworks to implement
I have already implemented Unity Ads (as seen in the white box above), and this framework (although created by Unity) will be utilized for online features elsewhere. I plan to use Unity Ads to make it modular (e.g., have time in between ads so they do not show after every completed level because this could frustrate the player and make them stop playing). I can also utilize the Unity Ads framework by adding extra lives for the player and extra skins for the player to unlock if they watch an ad.

I am also considering implementing Cinemachine because of its dynamic camera control and responsive camera systems, smooth transitions between camera states, and cinematic effects that boost the user's experience playing the game, making it more interactive. I will be making the camera slightly shake when a life is lost, using its smooth-follow camera features (virtual camera) to move dynamically, and using procedural cinematics by making the wind affect the camera to make the outdoor environment more realistic.

Unity's Universal Render Pipeline (URP) is another framework I am implementing due to its design making the game easily scalable depending on what performance device it is running - whilst maintaining quality visuals - this is important due to Androids vast variety of mobile phones that each have different specs and abilities to run mobile games, URP will make this rendering a lot easier on the phones. I will be implementing this for 3 core reasons; physically based rendering (PBR) that will simulate the interactions of light with materials so materials, textures, and lights are more realistic - volumetric lighting and fog to improve the game atmosphere and seem more realistic - and, finally, the fact it will allow me to use the Unity's Shader Graph to create and customize shaders without needing to code, thus giving custom materials and shaders specific visual effects that improve the game's stylization/

## Mobile features
As I will have a dandelion mechanic that can blow its seeds and the player can attach to until they reach the ground, I have decided to implement my mobile features inside of its gliding mechanic and camera to ensure it is still relevant to the game; I have used acceleormeter to tilt while gliding to ensure the player has control over movement in air to reach the goal. Also, my camera is designed to only be viewable in the third person, but the player never turns around, so I use a gyroscope sensor while moving to see the level while moving to understand platforms and how to reach the goal, once the player collides with a platform the camera will return to its original state to avoid confusion. 

I attempted to have microphone input to begin the gliding state, and then use the mobile accelerometer to tilt the mouse, so the player can glide around more naturally, however despite the microphone being initialized, Unity does not identify the microphone.

## Implemented framework with features
URPRender assets for optimized mobile use, low, medium, and high. Occlusion culling for higher quality. physically based rendering (PBR) that will simulate the interactions of light with materials so materials, textures, and lights are more realistic, URP also has new materials for the models that ensure it is better quality models, and also 
I have also used Cinemachine and Unity Ads but very basic implementations with only one feature for both (Unity Ads ad implementation and Cinemachine wind implementation).
I have also used 2 features from LootLocker, one for sign-up account data (e.g. login, registration, and forgotten password), and another regarding score saving for members, and their score will be uploaded to a leaderboard that will be displayed to all members. Note, if you wish to progress past level 1 and have scores uploaded and viewable for the leaderboard, you must make an account.

## Actual game
