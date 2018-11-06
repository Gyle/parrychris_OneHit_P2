# Header
* Name: Kyle Claudio
* Username: claudikyle
* Animal Role: Bear (Leader)
* Primary Project Responsibility: Adding Features and Leading Standup 

# Code Discussion
### Note that Thomas moved all the scripts into the resources folder which led to all history with scripts not directly displaying correctly. This is why I'm referencing the commits showing that I made/contributed toward specific files.

### Contributions
* Ground Pound Attack [All]
* Loading Character and Map Sprites [All]
* Modified Blocking so it Works With an Energy Bar [All]
* Game Over Pop-up Menu [All]
* Display Player Score [All]
* BUG: Player can attack while blocking [All]
* Character Select Screen [Most]
* Map Select Screen [Most]
* Carrying information between different scenes (Global Variable Script) [Most]
* Adding correct animations to correct characters [Half]
* Refactoring Code [Some]
* Implementing PS3 controller support [Some]
* Main Menu [Some]

### My Most Interesting Code 
#### Modified Blocking so it Works With an Energy Bar
Commit: https://github.com/Gyle/parrychris_OneHit_P2/commit/9e701ae6cef11618758cf157edc114226d822af6

The first piece of most interesting code I wrote was for the functionality of block using an energy bar system. 
I find this code interesting because this was a result that was derived from play testing week. The awkward 
feeling for block was a common trend for feedback which is why this issue was addressed. The main complaint was that 
blocking for even a micro second will initiate cooldown for block. As a result, players were unaware that 
they used their block and were not happy that they could not use it as they thought they could use it. 
Therefore, I was assigned to the issue of improving the block system. 

This code makes use of the Time object and an integer variable representing the energy bar. Since the cooldown code 
was already implemented by Oliver, I just had to work out how to rearrange it to achieve energy bar style blocking.
Essentially, `handleBlockMeter()` is called every frame to handle the behaviour of blocking. Once it finds that 
the energy bar is depleted, it will update the next time it may use block via Time object and reset the energy bar 
to full. You can see that it will do nothing if `blockIsOnCooldown()` or the energy bar is fully depleted, followed 
by linearly incrementing or decrementing the energy bar accordingly. I made it so that you cannot keep spamming 
block as continuous use only depletes the bar by 1 unit while initial use depletes the bar by 20 units. To change 
the size of the energy bar and how much energy initial blocking uses, I added two private variables named 
`MAX_BLOCK_VALUE` as the energy bar and `INITIAL_BLOCK_VALUE_DECREASE` and the initial cost to block.

#### Display Player Score
Commit: https://github.com/Gyle/parrychris_OneHit_P2/commit/5a4c57c24dbbf376e7fe4f8900cb9b93229b861a

The second most interesting code was to enable a live scoreboard to display on screen. This code is interesting 
to me because this was one of the most challenging issues for me in the project. In addition, it was really 
interesting working closely with my design partner, Shawn Lu. I asked him to make eight sprite images representing 
the four different states the scoreboard will be for both players. The whole process of working with another 
person from a different background was really cool. Another reason why I found this code interesting was because I was 
also learning how to work with UI elements in Unity 3D and my experience of making the `character select screen` and 
`map select screen` helped with my understanding of how to solve this issue.

To complete this task, I utilised my SpriteManager class to load in these assets and handle 
changing the sprite image of the UI element for the scoreboard. Essentially once a player wins a round, the player 
will call SpriteManager's `IncrementScoreboard()` and `UpdateScoreboardPlayerOne()` or `UpdateScoreboardPlayerTwo()`, 
depending on which player won the round. These functions will set the new sprite to the next one in the array which 
it has previously loaded, next being empty for reset or another bar full for increment. Once the game is won, being 
a player wins three rounds, the player will call reset the global variable script player wins values to zero.

#### Carrying information between different scenes (Global Variable Script)
Commit: https://github.com/Gyle/parrychris_OneHit_P2/commit/8676a76f52dd2089481c493bb962e51df2def4bb

Lastly, another interesting code I wrote was the global variable script, DataStore. We needed a way to carry over 
data from different scenes because of how we have a map select, character select, changing key bindings for players, 
how many rounds a player has won, if they're using PS3 controller, and the SpriteManager (which will be described in 
my most proud code section). This highlights why this code was interesting as this code is crucial for the game 
to function correctly. Without DataStore, then we could not make this game work as a proper game. 

Basically DataStore is a static class which holds static global variables which may be get and set anywhere in the 
code. Since it was a static class, it was very easy to work with it to get and set variables. As a note, the reason 
I say I did most of the work for this script despite making this script is because my other team mates built on top of 
this script to store information they required as global variables. Therefore, I setup an important foundation for 
finishing the game.


### My Most Proud of Code 
Script File: https://github.com/Gyle/parrychris_OneHit_P2/blob/master/parrychris_OneHit_P2/Assets/Resources/Scripts/SpriteManager.cs

Commits: 
* https://github.com/Gyle/parrychris_OneHit_P2/commit/fb5e79259663e852ac98bb28248915dd8e109d1d
* https://github.com/Gyle/parrychris_OneHit_P2/commit/3843a97a0ca852a295353ddf08b4d81ae1e2a7ee
* https://github.com/Gyle/parrychris_OneHit_P2/commit/61985fe49aa94c82eae4ce5cfddc5fe01a034dc7

My most proud code of this project is definitely the SpriteManager. This is because it has two important 
functionalities of loading in sprite assets and updating game objects to render the correct sprite. Moreover, 
I feel that I have implemented these functions well. The reason for this claim is firstly the way I load in 
the sprite assets. I have added documentation in the script file explaining how loading works and how to 
extended. But as a summary, if you follow the naming conventions for naming the character sprites and map 
preview sprite, the SpriteManager will automatic accept your new sprites without having to modify the script. 
Therefore, I have implemented an extensible solution for loading sprite assets. As an example, if you add 
another map, you have to put the sprite in the `Resources/Backgrounds` folder and name the image `map3.png or .jpg`.

Regarding the second function of updating game object sprites, it was nice to have everything related to sprites 
in on script. This meant that it was easy to find code related to scripts and helps with navigating through the 
logic of this projects code. Furthermore, the fact that there is documentation everything in this class, and in 
most places where I coded, is another strong reason why I am proud of this code. Personally I find no documentation 
for code is one of the worst things to find when working on a new project. 

Overall, I implemented an extensible solution for loading assets and kept the logic for managing sprite assets 
inside of one script.


# Learning Reflection
### What I have Learnt
Regarding human resources, I learned how to work in a team of different backgrounds. Since I was given the bear role, 
it enabled me to have more interaction with the management of the project. Therefore, I have learnt how to manage 
different members in a team. Moreover, myself leading daily standups really helped with progression because the 
outcome was that everyone in the team knew what was happening and were comfortable with speaking amongst each other. 
Standup also helped us gauge what each individual can do as I learned that one designer preferred character design 
and animation, two designers preferred level design, one comp student could do anything while another could do 
features but was not comfortable with UI. Thus, I have also learned that keeping up with communication is an 
essential strategy for maintaining morale, motivation and awareness. 

Regarding Unity3D knowledge, I have learned how to utilise UI aspects and how to carry over information to 
different scenes. As I mentioned earlier, UI was a challenge for me since I have not needed to utilise the UI 
for the past projects. However, it was a great experience since I got learn more aspects of the Unity3D system. 
I did not know that functions could only take one parameter max to be called through user events such as 
`mouse exit`.

Overall, I learned how to work in a team of different backgrounds and about Unity3D's UI utilisation.

### Most Important Thing I Will Use From This Project in Future Projects
Definitely the biggest takeaway from this project for me is how to work in a team of different backgrounds. This is 
such an important skill to have in team projects, especially for game development teams. I plan to exercise the 
soft skills I developed from this course in my upcoming project as an intern at Fonterra. I will be working with 
designers and other backgrounds to make a product. Therefore, this experience will greatly aid in my ability 
to adapt to a different team dynamic. Thank you so much for this opportunity, this was a well 
structured course that was enjoyable and it provided invaluable teamwork experience.
