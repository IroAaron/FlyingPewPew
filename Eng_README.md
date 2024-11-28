A simple prototype of a 2D bullet hell genre game. This project is just a prototype without complex services.
No entry point, no complex DI solutions, no Dataset, no async operations, no projectile pull container on stage.
Made 2 opponents - boss and kamikaze. One player unit is made. All of them are heirs of Ship.cs class

The logic of the boss is as follows: 

Large sized circle, has 16 cannons all around. Fires all of them simultaneously in all directions. 
Constantly rotates clockwise. The less amount of hp left, the faster the rotation speed and the faster the guns are firing. 
When there is less than 50% remaining each shot has a chance to spawn a kamikaze - an enemy flying at the player, when shot at which he dies, if not destroyed explodes when reaching a certain distance to the player and damages the area... 
When less than 25% xp, the chance of spawning is doubled. When it reaches 5% xp it becomes invulnerable for 30 seconds, spins and fires at maximum speed.
