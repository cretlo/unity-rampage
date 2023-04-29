# Project Synopsis

For our project we set out to build a time-based virtual reality destruction sandbox, taking motivation from the 1990s 2D game, Rampage. Completely physics based, the buildings are entirely destructible, allowing the player to cause destruction anyway they see fit. The player comes equipped with various abilities and movement capabilities such as walking, turning, leaping, climbing, punching, grabbing, and throwing, allowing for a dynamic motion experience. Additionally, for the time-based aspect of the experience, the player is timed with a tracked score that equates to the destruction they’ve caused, saved to a Firebase database, and displayed on an in-game world leaderboard. All of this resides in an environment completely constructed from scratch. This includes a dynamic skybox, navmesh agents and all assets visible, heavily optimized using LOD (Level of Detail).

# Status Updates

- [Status 1](https://github.com/cretlo/unity-vr-rampage/blob/main/status/status1.md)
- [Status 2](https://github.com/cretlo/unity-vr-rampage/blob/main/status/status2.md)
- [Status 3](https://github.com/cretlo/unity-vr-rampage/blob/main/status/status3.md)
- [Status 4](https://github.com/cretlo/unity-vr-rampage/blob/main/status/status4.md)

# Videos

- [Working Demo](https://www.youtube.com/watch?v=SysGxwCsr8I)
- [In-Development Demo](https://www.youtube.com/watch?v=DtVMk030N-U)

# Presentation

- [PowerPoint Presentation](https://uwy-my.sharepoint.com/:p:/g/personal/ireimers_uwyo_edu/EYY5WqaJJGpLjfqGb6QT-HQBsVIcz2s93dKQ1j1kw9bwBQ?e=ddQHDo)

# Project Planning and Execution

- [Design Requirements & Specification](https://uwy-my.sharepoint.com/:w:/g/personal/ireimers_uwyo_edu/EUpWzcEJwFNKkzetrwYUOowBspXquCv7HifL29Dr8rxh2Q?e=6Re07Thttps://google.com)

- Expected Finalized Plan of Work

  - Ian – Programmer/Designer/Modeler/Sound Design

  - Edward – Modeler/Programmer

  - Jacob - Programmer/Artist/VR Expert

  - Jeanne - Project Coordinator/Schedule Coordinator/Modeler

- Actual Finalized Plan of Work

  - Ian – Destructible Buildings/Character Controller/Leaderboard

  - Edward – Menus/UI

  - Jacob – Asset Management/Programming

  - Unfortunately, we lost Jeanne the following semester, forcing us to quickly recreate a plan, assigning more specific roles to each person.

# Final Implementation Summary

- Design,

  - For the final design, we were able to include everything listed in our synopsis. The game is built for a general experience with causing destruction; however, we included a leader board that keeps track of the time and score of each destruction run for an alternative in-game purpose. The game is fully cross-platform with the successful implementation of OpenXR. All assets were made from scratch utilizing Blender.

- Limitations,

  - Not having an in-game keyboard to type your username.

  - Due to each building being computationally heavy, there are only so many buildings present in the level for the current implementation.

  - The button mapping is unchangeable and might not fit the player’s wants or needs.

- Future Direction,

  - Add Story/Quests, Sound, and Powerups.

  - Implement haptic feedback

  - Optimize scripts

  - Implement multithreading and use unity DOTS for object management

- Work Statement

  - Whole Team:

    - As a team we communicated our roles with each other through weekly Discord and in-person meetings. Each person not only equally contributed to the planning, project direction, documents, media, and presentation work, but also successfully filled their role within the project.

  - Individual:

    - Jacob Bahr: Designed and textured the buildings used in the final implementation, modeled the spears, Modeled the skybox w/ procedural clouds and mountains, implemented the AI Nav Mesh system and designed car movement behaviors, Optimized the psychics for the buildings in order for us to insert entire cities into our game and designed the LOD system. Build the Roads and landscape using Blender OSM, Made the cars with a procedural car generator. Spend 150-200+ hours on the project (Most of that learning blender)

    - Ian Reimers: For the work, I created and managed the repository, character controller, destructible buildings, leaderboard functionality, and various other scripts necessary for the project. The total work was roughly 150+ hours.

    - Edward Gilbert: Worked on learning blender and Unity. Made the menus and UI. Created transition fades between scenes. The total work is about 40+ hours.

# Reflection

- Lessons Learned
  - Unity Game Engine and Unity Components
    - C#
    - OOP
    - Delegates and Events
    - Decoupling
    - AI Navigation
  - Basic UI design
  - Firebase
  - OpenXR
  - Virtual Reality concepts
- If we had to do it all over again, we would integrate faster, and just overall more, prototyping into the early phase of the development process. Additionally, we would choose planning software, such as Trello, that everyone is familiar with so if a person leaves, other members can take over managing the planning and scheduling. We originally thought having 3rd party assets would make our development easier, but they ended up causing issues for us, so, starting over, we would build all assets from scratch, right from the start, instead. Lastly, we would focus on a lot more planning before beginning development of the project. Maybe use Maya instead of Blender.
- Advice for Future Teams

  - GitHub comes with a premade .ignore template for unity projects.

  - Find each group member's strengths and weaknesses early on to get a better understanding of what tasks they should be doing.

  - Blender is not fun to learn, maybe use a different 3D modeling software if you are just learning.

  - Design patterns would of drastically helped us implement some of our systems
