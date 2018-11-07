# Header
* Name: Oliver Barr
* Username: barroliv
* Animal Role: Owl
* Primary Project Responsibility: Adding features and fixing bugs

# Code Discussion

### Contributions
* Added PS3 controller support [Some]
* Fixed player spawning locations [All]
* Added support for tallying score [Some]
* Fixed an error where the player could dash whilst blocking [All]
* Fixed animations and trasitions between them [Some]
* Allowed the animations to load (and start) properly on startup [All]
* Fixed the dashing animation [All]
* Fixed player attack animations [Some]
* Fixed the issue where players could continuously attack [All]
* Added blocking during and cooldown [All]
* Added support for different characters into the main player script [All]
* Created the ability to multi jump [All]
* Fixed general blocking and dashing bugs [Most]
* Fixed infinite dashing + jump glitch [All]
* Fixed issue where players would not maintain velocity when switching sides [All]

### My Most Interesting Code

### My Most Proud of Code 
My most proud of code is something that took me far too long to figure out, hence giving me great satisfactioon upon completion. Upon receiving the project, there existed a bug which would cause players to spazz and break should one dash over or under the other. This was a pretty major bug, and was being caused by the PlayerDirection script. The PlayerDirection script simply rotates the characters 180 degrees upon switching sides. This is good and solves the switching sides issue, however __also__ rotates their velocities, causing the known bug. My first attempts to fix this were to find means of rotating the character without rotating the velocities, but ended up with nothing. The next (and final) solution I devised was to add a direction to each dash within the player script. Utilizing the existing onRightSide boolean value, I added a dash direction boolean (to keep it simple) to determine dash direction. A super simple fix that works as we need.

This can be found in the [Merged Behaviour](https://github.com/Gyle/parrychris_OneHit_P2/blob/master/parrychris_OneHit_P2/Assets/Resources/Scripts/MergedPlayerBehaviour.cs) script between lines 447 and 462

# Learning Reflection
### What I have Learnt
Through the course of COMP313 I learnt a great deal about what it means to collaboratively work on a project. As the Owl of the group, I made early to list down features the team would like to see in the final product. And although I was not the team leader of the project (nor responsible for organizing), I utilized Kyle's (the Bear) standups early to establish my own goals for my role.

The other biggest skill I will take away from this project is a better understanding of C# and creating things within Unity3D. While C# is not something I regularly work with, I believe having learnt a basic understanding will help me with future projects. Not only for the sake of knowing new languages, but C# is the first scripting language I have learnt. While I have by no means mastered it, I found using C# with Unity to be very simple and easy to pick up, and I hope to use it again.

The final important skill I have learnt through this project is a basic understanding of how the __Design__ works. Developing maps and character animations were not my tasks in this project, but just simply working with students who have this different field of expertise has really helped me to understand how each component of a game adds together. As a software developer, I'm glad to have learnt more about the designers process, as I could very well be working with them in the future.

### Most important thing I learnt for future projects
Technical skills aside, I believe that good management skills are crucial for team projects. While there's always room for improvement, I believe my team has done an excellent job at setting, managing, and achieving project goals. Working with my team has helped me to understand the effort it takes for a good management, and the benefits that come with it. In future projects I will no doubt be working with another team, so I will take what I have learnt to ensures its success.


