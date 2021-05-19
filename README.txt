Playable executable available at https://jessrangel.itch.io/

Software Engineering Project 
Spring 2021
Dr. Joanne Skiles

Using Agile methodology and Unity Engine.

Development of the projectiles, player shooting behavior, enemy behavior, enemy interactions, user interface and other implementations were done by Jessica Rangel.
Development of the player movement, player data, camera movement, hazards, player interactions, and other various implementations were done by Alexander Hecht.
Level design was done by Joshua Kim.
Music and sound design was done by Julian Maniquis.

The source code is organized in Unity project files.
Inside the assets folder there is a folder named "Scripts".
This is where our scripts for the code are located.

The file titled "SE Project Final Project Files.zip" is the main source code of our project.
It contains files that can be loaded into Unity, and scripts that can be loaded with an IDE (in our case, visual studio).

The CameraBehavior.cs script controls the behavior of the camera and causes it to move with the player.
The Checkpoint.cs script simply holds the value of what checkpoint it is for the player to interact with.
The EnemyBehavior.cs script controls everything related to how the enemy behaves and its interaction with the player.
The EnemyProjectile.cs script is the projectile that is fired from the enemy towards the player.
The Killzone.cs script is for killzones that kill the player on contact.
The PauseMenu.cs script controls the behavior of pausing the game and what the buttons do.
The PlayerData.cs script is where most of the code related to the player is located.  This controls the player's health, taking damage, dying behavior, spawning including at checkpoints, pausing the game using the PauseMenu script, updating the UI and various collisions the player has.
The PlayerMovement.cs script contains the main movement of the player as well as how firing projectiles work from the player's side.
The PlayerProjectile.cs script is the projectile that is fired from the enemy towards the player.
The Projectile.cs script is the base class that the various projectiles inherit from.  This is the base functionality that every projectile uses.

The Menu folder contains scripts related to menu behaviors.  Among these are:
The EndScreen.cs script which controls the behavior of everything on the end screen.
The StartMenu.cs script which controls the behavior of the various buttons of starting the game, showing controls, and quitting the game.

In order to build the project, you need to use Unity and under File->Build settings, build the project from there.
The file titled "SE Project Final Build.zip" contains the built project in case you do not have unity to compile and build the project in unity.
These files would ideally be sent to users of the program.

The only package that we used outside of creating everything ourselves was the TextMeshPro package so we could make text look better.
