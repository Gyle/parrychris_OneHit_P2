# Header
- Name: Thomas Edwards 
- Username: edwardthom
- Animal: Puppey
- Primary Role: Adding features (all types) and fixing bugs.

# Code discussion

### Contribution

- Main Menu [Most]
- Sounds - Adding all music and sound effects into the game and into the code [All]
- Mid air dashing [All]
- Refactoring code (merging the two player controllers, creating prefabs for reusable assets) [Half]
- Created scriptable objects to hold control schemes [All]
- Implementing PS3 controller support [Some]
- Adding Roofs to maps that players could jump on [All]
- Character and Map Select Screens [Some]
- Added Win/Lose popups and 3,2,1 Countdown to game [All]
- Fixed bugs with the game (getting stuck in walls, jumping too high, not being able to change directions while jumping etc.) [Half]
- Modified Blocking so it works with an energy bar [Touched]
- Adding correct animations to correct characters. [Half]

### Most interesting part of the code

I found the most interesting part of code was implementing modifiable controls. Initially we had our controls defined in each of the player's controller scripts. However I changed the controls to be definied in there own files. This allowed me to access the control schemes easily from other scripts. I ran into an issue because the file I used to define the control scheme was a scriptable object. Scriptable objects are really cool, they act as a set of definitions for constant variables. You can have multiple scriptable objects for the same set of variables which worked well for our game as it allowed us to define seperate files for each player's control scheme, as well as a PS3 control scheme.
My issue was that in the editor it appears that you can change these files at runtime. This is what I was initially doing when I allowed players to change the controls in the main menu. However, after building the project (and much research) I found that it was no longer possible to edit Scriptable Objects at runtime in a built project. This caused issues and I was forced to change how the the controls were changed, instead they now store any changes to the controls in a global variables script which overrides the default scriptable object.


### Most proud of code 

The thing in this project which I was most proud of the main menu as I came up with the design myself and the design students that reviewed it liked and accepted it.
In terms of code this included all of the menu items as well as the settings. I was proud of how I was able to fully link sound, design and functionality across scenes. I feel that this is particularly good code for two reasons. The first is that each segment of the code is reusable, buttons can be easily transferred to other areas of the menu. the second reason is that the code is easy to understand. Each button is linked to a single action function defined in my button controlller this makes it easy to see what each button is doing. Additionally the scene objects are labeled profusely and each object clearly shows what section of the menu it controls.

The Main Menu scripts are mostly in the Assets/Scripts/MainMenu folder, this folder also includes some of the default starter scripts which helped get me started.
