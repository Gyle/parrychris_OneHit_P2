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

# Learning Reflection

### What I Learned

Intially when I came into this project I expected to coding parts to be fairly easy this was because it was all done in C# which was a language familiar to me. However I found that the real learning curve came in learning about Unity's engine. All the different components you could add to an object and what each one did was what I spent most of my time learning about. I also learnt about the scripting aspects of each component. 

The UI aspects of the project also taught me a new and interesting method of positioning. Compared to classic CSS and HTML styles of positioning elements, Unity's methods seemed easier to use. Unity used a system of anchors to position elements relative to a position of the screen. Initially I was confused by all of the different options, however once I got my head around it, it was an easy and very powerful way of positioning elements which works across all different screen sizes.

Another small thing which I found quite surprising was that the design students were just as interested and just as motivated about gaming and games as us engineering and comp-sci students were. The only difference seemed to be that they were much more interested in creating cool animations and cool environments where as the comp students were more interested in cool abilities and functionality.

Overall I found this project an excellent introduciton into Game development. It was interesting to see how design art, sounds, animations and code ere all merged easily into the single project. I found that the methods of importing things like music and assets from the unity store were extremely easy to do and straightforward.

### Most Important aspect of this project (for future development)

I think the most important aspect of this project that I learned was my knowledge of the Unity system. More specifically, this was things like what components were available and what scripting could achieve. This is extremely useful as it taught me what a game engine could do and what it was capable of. Even though the knowledge of how to achieve certain aspects of the game are specific to Unity, the general concept is great to know. This knowledge is trasfererable to any game engine as they are all designed to acheive the same thing and most have similar methods to Unity.
