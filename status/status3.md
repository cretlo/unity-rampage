# 1. Recap

The plan was for Ian to get throwable objects, the climbing refactored, and the leaping functionality. Jacob was to figure out a workflow to produce destructible buildings faster along with LOD, and start working on the enemy AI. Edward to finish the design and function of the main menu.

# 2. Tasks Completed

## Description of Tasks Completed

Edward: Finished a basic main menu.

Jacob: Finalized our asset pipeline, and created a way to have native LOD when importing the assets into blender. Created the basic functionality of our enemy AI.

Ian: Created the leaping functionality, throwable object with appropriate physics, a character manager to keep track of the characters state, a backend that communicates to Firebase, a hand menu and leaderboard where the player is able to stop their run, restart the level, see the current stats, and upload their run to display on the in-game leaderboard. Additionally resolved all issue that have been found so far, and fixed
the punching mechanism to register on the walls perfectly. For cosmetics, I created a way for us to handle particle system spawning.

## Quantifiable Metrics

Each person met once each week for a few hours while maintaining continuous communication through discord group.

Edward made multiple script files and spent roughly 20+ hours total.

Jacob finalized our asset workflow with integrated LOD for Unity to recognize. Created scripts for enemy AI.

Ian wrote over 500 lines of code, creating a wide range of scripts that implement some functionalty we needed, and getting them all working together cohesively with zero bugs found so far. He got the game hooked up to firebase to keep track of all players time and score, and created the in game UI to display it all.

# 3. Successes

## Accomplishments

We have reached our minimal viable product along with implementing features that weren't orginally in the game plan, such as a real time
leaderboard. We also have the game working completely bug free (at least the ones we've found so far).

## Successful Solution

Creating a character manager for managing the character state which allowed us to resolve all bugs with the character controller and getting each ability to work together seamlessly. Using Newtonsoft JSON package resolved all issue with us serializing our data structures. Our idea of leaping surprisingly worked with no issues the first time we implemented it. Lastly were getting all the menus implemented.

## Things Tried But Didn't Work

We tried to use Firebases functions for ordering and sorting the data returned when fetched, however, due to how we structure our data
we had to sort and organize our data ourselves. We also tried to use Unity's JSON handling module but it did not work with how we were structuring our data.

# 4. Roadblocks/Challenges

## Challenges

Edward - Figuring out how to make a canvas with buttons and input text. Figuring out how to transition and quit scenes. I also needed to figure out how to pass a variable between multiple scenes. Overcame them by looking up and following multiple tutorials that explained how to do them. Tried multiple different ways to do them and figured out what works. The menu will need to be spruced up and connected to the main rampage scene.

Jacob - Understanding the mechanics of Unity's NavMesh, as well as automating LOD objects within our asset pipeline.

Ian - One of the big challenges was getting firebase working appropriately with Unity in C# due to the modules complexity. Additionally, as we added more features, getting them to work right with each other was a big challenege that required multiple iterations of refactoring until we came up with a good design for handling all the state. Also, for our character controller, when disabling certain components needed for a specific state, and turning the XR components back on, all references were disappearing, giving us a null value reference. Hunting down this issue, and supplying the component with all it references again, when enabled, resolved the issue.

# 5. Changes/Deviation From Plan

Jeanne ended up not taking the 2nd semester of the course so we were down a member along with our last semester planning documents. We originally weren't going to include persisten data but changed our decision with implenting it to give the game more purpose.

# 6. Details Description of Goals/ Plan for Next 3 Weeks

Jacob - Level designing and creating assets for our presentation. Work on making the AI more robust.

Edward - Add the main menu to the rest of the project and get the project ready for presentation.

Ian - Get the peices of the breakable wall throwable when they are detached from the building, as well as getting about 50% of the debris to disappear after some time to cut down on computing cost. The main plan is to get our project presentable with a solid rough draft presentation. We also need to find a way to transfer data over from scene to scene.

# 7. Confidence

Edward - I am 4 on how confident I am on getting the project ready for presentation.

Jacob - I am a 5 on completing everything listed.

Ian - I am a 5 on completing everything listed.
