# 1. Recap

The plan was for Ian to get throwable and disappearing wall chunks. Jacob was to level design and create assets for our presentation, and additionally, work on making the AI more robust. Edward was to add the main menu to the rest of the project and get the project ready for presentation.

# 2. Tasks Completed

## Description of Tasks Completed

Edward: Helped finish a draft of our slide presentation.

Jacob: Worked on AI and building design.

Ian: I implemented sorting on the Firebase user data to sort by highest score and then by lowest time. Additionally, I added a word filter to discard bad usernames. Lastly, I made videos and slides for our presentation, and I edited the demonstration.

## Quantifiable Metrics

Each person met once each week for a few hours while maintaining continuous communication through discord group.

Jacob worked 30+ hours prototyping buildings in blender and learning how to use the navmesh in unity.  
Jacob finalized our asset workflow with integrated LOD for Unity to recognize. Created scripts for enemy AI.

Ian fully completed the leaderboard feature. Recorded footage of gameplay for demonstration and slides.

# 3. Successes

## Accomplishments

We are presentation ready, and beyond minimum viable product.

## Successful Solution

Jacob: Made a template for building design so it is easily configurable. Learned how to bake nav meshes. Also how to make skyboxes. I used array and mirror modifiers in blender to generate a building from two blocks in order to make them easily configurable.

Ian: Was able to sort the data using a C# IComparer.

## Things Tried But Didn't Work

Jacob: Tried to use the normal navmesh system in unity but it would not work in the scene that I set up so I had to use a different navmesh library.

Ian: At one point, Firebase wasn't storing runs since it wasn't returning from it's asyncronous read call, so the way we implented reading the data was to first check if the user wasn't stored in the database first, which strangely wasn't an issue before.

# 4. Roadblocks/Challenges

## Challenges

Edward: Couldn’t connect the main menu scene to the main rampage scene yet because it still needs to be worked on. The Menu still needs to be spruced up and connected to the main rampage scene.

Jacob: How to texture objects in blender, how to use cell fracture, how to use UV projection so the textures were not distorted once I used cell fracture to break up the object. How to turn a high polygon object into a low polygon object with convex mesh. How to implement an LOD system to save save system resources. How to make an object into a flat plane to use for the LOD when the player is far away, how to use LOD on each individual object to show the player the cell fractured version when they are close enough to hit it, how to add all objects into their own collection in order to export them to automatically import as an LOD object in Unity, how to batch export objects so I didn’t have to export them all individually, how to make all the objects import into unity in the correct location in relation to the house. How to find and fix overlapping objects in order to use the wall breaking script. I still need to improve the workflow on assets creation in blender. I was previously using 3rd party programs that didn’t work well with our script. I still need to make the nav mesh more complex. I still need to make enemy AI. I still need to compile the 3D printed parts for the force feedback gloves.

Ian: How to use the Sort() function in C# with IComparers, but with working with it for awhile, I was able to get it implemented successfully. Also, locating the issue with Firebase not reading data and returning from its async call which was found by checking the database, using a new async call, with a user who's already stored, which would always return, so we checked if the user is stored first before reading data that isn't there.

# 5. Changes/Deviation From Plan

Ian: Worked on making the leaderboard more robust and to get it sorted appropriately since that still needed to be done.

Edward: No deviation from the plan.

Jacob: No deviation from the plan.

# 6. Details Description of Goals/ Plan for Next 3 Weeks

Ian - Assist with adding the main menu to the project, assist with getting the assets ported over, create new in-game footage with new assets and level design, get project 100% ready for presentation.

Edward: Add the main menu to the rest of the project and finish getting the project ready for presentation.

Jacob: Need prepare the scene for presentation and add on extras if time allows.

# 7. Confidence

Edward - I am 4 on getting the project ready for presentation.

Jacob - I am a 5 getting the project ready for presentation.

Ian - I am a 5 on getting the project fready for presentation.
