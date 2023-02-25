# 1. Recap

The plan was for Ian to get climbing working, Jacob to figure out the asset situation that works with our destructible building algorithm, and Edward to make models for the game in blender.

# 2. Tasks Completed

## Description of Tasks Completed

Edward: Created protoype project for understanding Unity's raycasting and learning menu concepts.

Jacob: Created blender to Unity pipline for assets and created Navmesh for AI.

Ian: Implemented the climbing functionality on the destructible buildings, and assisted with asset creation related to Jacobs pipeline.

## Quantifiable Metrics

Each person met once each week for a few hours while maintaining continuous communication through discord group.

Ian wrote roughly 150 lines of additional code and spent roughly 35+ hours total.

Jacob created the pipeline for blender to unity, gathered all assets, and spent roughly 50+ hours total.

# 3. Successes

## Accomplishments

We now have climbing implemented with the VR rig working with our chosen destructible walls method, along with a asset pipeline, and basic AI movement. 

## Successful Solution

Creating models that have the appropriate topology for working with Unit's convex colliders, making fully destructible buildings, not just basic objects, getting Blender's cell fracture
working on assets, and modularizing building walls for creating the destructible buildings.

## Things Tried But Didn't Work

We tried to incorporate free assets from online, however, the topology did not allow us to use our pipeline workflow, and did not like Unit's convex colliders.

# 4. Roadblocks/Challenges

## Challenges

Edward - Finding the additional assets that were required in the tutorials. Also finding more in-dept tutorials to get a broader view of how to make them. I still need to find the assets
and complete the basic menu.

Jacob - How to texture objects in blender, how to use cell fracture, how to use UV projection so the textures were not distorted once I used cell fracture to break up the object. 
How to turn a high polygon object into a low polygon object with convex mesh. How to implement an LOD system to save system resources. How to make an object into a flat plane to use 
for the LOD when the player is far away, how to use LOD on each individual object to show the player the cell fractured version when they are close enough to hit it, how to add all objects 
into their own collection in order to export them to automatically import as an LOD object in Unity, how to batch export objects so I didn’t have to export them all individually, 
how to make all the objects import into unity in the correct location in relation to the house. How to find overlapping objects in order to use the wall breaking script appropriately. 
I still need to improve the workflow on assets creation in blender. I was previously using 3rd party programs that didn’t work well with our script. I still need to make the nav mesh more 
complex. I still need to make enemy AI. I still need to compile the 3D printed parts for the force feedback gloves.

Ian - When trying to implement climbing using Unity's configurable joint, when we would grab a wall, it would shift the player rig over a few feet, and rotate them away from the wall.
I was able to overcome this by setting the default anchor positino to 0, and rotating the configurable joints rotation based off the player rigs local rotation. Additinally, when the 
character would move the following hand models would stutter due to it being in the physics loop and the headset being in the update loop. Changing the mesh to the controller objects
resolved this issue. Lastly, the biggest challenge was assisting with the asset pipeline for our wall algorithm. Creating the low poly assets resolved all issue since we were able to 
control the topology how we needed it and test with the convex colliders. The challenges still left are to get leaping, throwing, and climbing working together and fully completed. Additionally,
to create more modular peices for buildings and houses along with typical scenery, and to optimize the destructible walls so that they aren't taking up as much computing.

# 5. Changes/Deviation From Plan

Jeanne ended up not taking the 2nd semester of the course so we were down a member along with our last semester planning documents. Edward is now working on creating the VR menu that will
play music, change settings, and choose the level.

# 6. Details Description of Goals/ Plan for Next 3 Weeks

Jacob - Figure out a workflow to produce destructible buildings faster along with LOD, and start working on the enemy AI by making it more robust.

Edward - Finish the design and functions of the menu.

Ian - Work on refactoring the climbing script to better integrate with the walls/destroyed wall peices, and to get throwable object, along with the leaping ability completed.

# 7. Confidence

Edward - I am 4 on how confident I am on completing the menus for the game.

Jacob - I am a 5 on completing everything listed.

Ian - I am a 5 on completing everything listed.
