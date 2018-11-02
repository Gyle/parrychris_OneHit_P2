# ======= NEW README.md BELOW======

## Architecture
This document will discuss the architecture of the game in terms of level structure and game loop. This topics to be 
discussed includes level structure, game loop, assets, installation and setup, how to play, and link of each developers .md file.

### Level Structure
Z-Depth organisation: When making the map levels, we considered the z-axis to add visual depth to the game to allow for 
immersion of gameplay. As opposed to a flat background that offers no immersion. Furthermore, the GUI elements have a closer Z index because characters should not walk in front of menu buttons. This elements are the following: 3, 2, 1, Fight!, Player X Wins!, Score boards on each side, and end game menu.

how prefabs are organised: We organised misc prefabs / props as decorations to add visual 
interests, but not to take away from gameplay. Furthermore, we stuck to the original theme of an Asian setting since 
there is a lot references for design inspiration. Regarding GUI, the scoreboard and menu prefabs were placed 
in the canvas object because anything inside canvas will be tracked / fixed by the gameplay camera.

3d scene 2d gameplay: We stuck with this because it adds an interesting visual contrast. Moreover, it adds a point of 
difference because there are many 2d fighting game that exists with 2d backgrounds. In terms of gameplay, we can 
easily communicate to the player map information. This is because we can add more detail in 3d since it 
allows for a better understanding of where you are positioned on the map.

Map design: We decided to use the fighting pit layout, that being elevation on the edges and a lower terrain in the middle. 
This forces players to make a strategic decision to engage or jump onto one of the roof tops. It also makes use of the 
air dash mechanic to create some Esports level highlight reels. Furthermore, it allows for utilisation of our new move, 
ground pound. This is because there is elevation to help players get above their enemy's head.

### Game Loop
#### PLAYERS
The game will be playable on PC for 2 players on one machine. The game can be played by two people sharing one keyboard. It also allows for console controller inputs for a more immersive gaming experience. The target audience for this game would be people who are interested in multiplayer, competitive old school fighting games. Controls can also be customized from the menu screen.

#### ACTIONS
Players can perform a number of actions in the game to defeat their opponent. The level design of the game forces players to navigate rooftops and try and out position their opponent, adding a strategic element to the game. Each players move-set consists of a short attack, dash-attack, jump, move and block.

Jump: A simple jump mechanic allowing players to jump onto rooftops.

Double Jump: An extension of this jump mechanic allowing players to jump higher by pressing jump twice.

Dash: A dash mechanic allowing the player to rapidly move across the map in a attack position towards their opponent. This mechanic increases the pace of the game and makes for fast strategic attacks. Other players can avoid dash by either blocking or jumping. The dash attack is placed on a 3 second cool-down, and is seen as the 'Over Powered' move.

Ground Pound: A mechanic which allows the player to attack downwards from an aerial position. This mechanic also allows for more control over jumping as players can position downward when they choose.

Jab: A simple jab mechanic used for close encounters.

Block: A blocking mechanic which players can use in defense of attacks from the other player. There is a time limit on how long you can use this mechanic so it does not take away from the fast-paced gameplay.

#### Character In-Depth
Monk: This character is based on the Street Fighter 3 characters Rolento and The King of Fighters characters Billy Kane. He is a Chinese Shaolin Monk, and has a longer-range attack than the other characters.

Samurai: This character was inspired by Japanese Samurai. This character has the ability to double jump but each jump is small, and his attack is shorter than other characters.

Ninja: This character is most balanced character in this game. He is based on the model of a ninja on Ninja, and he could be most power full characters in this game.

### Assets

#### Libraries
We do not use any libraries, except for the built in Unity API.

#### Music /Sfx
Brad got sample sounds from freesounds.org then made proper Sfx by merging them into their own sound files. 

#### Sprites / Textures
Characters: The original character was left in the game and finished by Shawn. The other two character were all designed 
by Shawn

Maps: Brad made everything on Map1 except for the trees, roof, and some miscs from the original assets. Likewise, Will 
made everything on Map2 except for the trees which he got from the Unity Store. 


### Installation and Setup
Run the mac build included in the root directory on this repo. Launcher settings do not matter.

### How to Play
In the main menu there is an instructions button showing the controls. You are able to change the controls in the 
instructions menu by selecting a control and pressing the keycode you want to use. It is a best of three wins game. 
To win a round you need to successfully hit the enemy player once. There is an option to enable PS3 controllers 
for player one and player two.

*Controls*



### Link of Each Developers .md file

# ======= NEW README.md ABOVE======

# ======= OLD README.md BELOW======

# One Hit Prototype    
"One Hit" is a fast paced 2D fighting game, with a 3D background. The twist is that it only requires one successful attack to defeat the other player, winning the round.

**Note:** Quite a few commits were from the in-lab macs and are labelled as guest commits. In these situations we have tried to make it known who made the commit (usually via comment or in the title).

## Demos:
#### Moveset Demo:  
https://drive.google.com/open?id=1zP4KMrXxN-oN4KXTCvPpkNofGJBc3F1h  
#### Real Fight:  
https://drive.google.com/open?id=1bkC0A2XEVCCGC8xvF60LeN1gcxQfQl1j
#### Bug or Feature?
https://drive.google.com/open?id=1T-E13J8vxmQBjk-bb9ggsEmScpfDtOh2  
## Architecture  
This game, developed in Unity, is played by two people sharing one keyboard. The blue player is controlled with the arrow keys and the green player is controlled with WASD. Each players moveset consists of a short attack, dash-attack, jump, move and block.  The dash attack is on a 3 second cooldown, and is seen as the 'Over Powered' move.  

#### Level structure  
The players have rigidbodys, box colliders and trigger box colliders. There are head and body trigger colliders. The head colliders are so player can jump off other players head. This works by the trigger setting the top players 'grounded' boolean to true, allowing them to use jump.  

The game has one camera which updates and adjusts its position in response to both players positions. From a viewers perspective, the camera follows the players horizontally. It also zooms in and out as the players get closer or further awayfrom each other. This camera movement allows the surrounding enviroment to appear 3D.   

The sky is displayed with clouds passing though. This was done by writing a dedicated 'CloudScroll' script. It simply incremements the clouds position every frame, resetting it to the begining position once a pre-set value is reached.  

There is a separate script for each player, which are very similar. Although there is a lot of duplicated code, I kept it like this as there are plans to add a range of characters, with different stats and moves.   

Most variables which may need tuning in the future are public. This is so the designers can help fine tune the game without having to change code.

I had a lot of trouble implementing a canvas with UI components, such as text. In the end I disabled the canvas and used a sprite on top of each player as an indicator. The players indicator dissapears when they lose. This should be improved in the next iteration.   

#### Game Loop
The game begins with players on opposite sides. The round is over when one player scores successful damage on the other player. This makes the scene restart, and essentially begins the next round.   

Each round take from less than 1 second, to several seconds long. It is a faced paced game, hence the short round times. The game is in the same state at the beginning of every round.   

Although there is no total wins so far count, this is to be implemented at a later date.  

### Most Technically Interesting/Challenging Parts of Prototype  
- Merging designers 3D background and other designers sprites/animations into project.
- Getting UI to work - only works in Unity. Settled for a coloured sprite indicator to show game state.
- Getting the camera working with 3D scene.
- Implementing the first attack move (close-range melee) was difficult, and the learning curve was steep. Once the first was done, however, the rest of the moves were done much faster.
- Team-members using different versions of Unity caused a range of issues, such as missing assets. Most of the issues were never solved.

### Controls && MoveSet:  
#### Blue Player:  
Movement: Left/Right Arrow Keys  
Jump: Up Arrow Key  
Attack: Right Shift  
Block: Right Alt  
Dash: Down Arrow  
Restart: Delete  

#### Green Player:  
Movement: A / D  
Jump: W  
Attack: Left Shift  
Block: E  
Dash: S  
Restart: Delete  

### Instructions to copy repository locally:  
* Download Git   
* In command line / terminal:  
* navigate to directory  


```bash
$ git clone https://github.com/CJParry/parrychris_OneHit_P2.git
$ cd parrychris_OneHit_P2
```
### Ideas
Powerups
  - take an extra hit
  - double jump
  - movement speed buff
  - longer hitbox
  - ~~frame recovery~~
  - weapon
  
UI
  - welcome screen
  - new round. best of 3
  - ~~show end game~~

- map interactions
  - ~~walk on roof~~
  - worm holes
  - map hazards that kill player / stun / slowdown / push player
  - wall breaks (cinematic of moving to the next level)
  
add new map after adding map interactions

- ~~expand level size~~

extra moves
  - ~~double jump~~
  - Feint attack (looks like a dash to try trick the opponent into blocking)
  - ~~slam attack~~
  
sound
- death sound
- background music
- getting powerup
- hit sound'

player interaction
 - slow motion on near hit
 - consider time it takes to fist to reach player
 - blocking too much means u can get stunned.
 - ~~blocking means you can only move backwards. No jumping or forward movement.~~
 - combo system ( player has strings )
 - maybe change one hit death system?
 - gun, strong but backfires player
 - duck high jabs
 - ~~timed block~~
 - small knockback on successful block
 - controller support
 
 
animations
  - animate powerups
  - animate moves
  - recycle animations if possible
  
 roster
  - different characters with their own moveset
  - different attributes eg big person thats strong but slow, agile ninja?
  
### Character Two Traits
* Shorter dash
* Shorter dash cooldown

* Lower jump
* 1 Jump

* Longer jab range
* Longer dash range

* Longer shield block
* Longer shield block cooldown

* Slower run speed


