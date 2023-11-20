---
layout: post
title: "Devlog Round 2"
date: 2023-11-20
tags: [devlog, gameoff2023, gameart, gamedesign]
comments: true
---

![Sample for Project Kiwi Room](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/kiwi_sample_small.png) 
Hello again! Ready for our second devlog? Here we go!
We finished the first sprint with a testing session, where we found some issues. For example, the jump physics were a little to slow, so during this week we fixed them.
We kept up the hard work to have a complete version of the first level at the end of the week.

## Code

On the programming side, we are working on these fronts:
- **Slider Puzzle:** While Kiwi is big, he can change the order of the rooms in a slider puzzle. Our main programmer implemented this mechanic by creating a test grid where cubes and spheres could be placed. So we have a prefab grid to design these "board levels". They also have scriptable objects attached to them to assign modular rooms to the grid via Unity Interface.
- **Platforming mechanics:** We are fighting with the platforming mechanics so that it gets the more polished at possible. This involves game physics as Kiwi's jump and fall velocity, gliding, wall sticking and gameplay mechanics as enemy animations and routines.
- **Menu/Game Story:** We are preparing the "game story" of our game, the flow of the player experience we designed the previous week. So we are programming this concept on Unity by creating and connecting all the scenes that our game will have: intro, main menu, game room, pase menu, game over and ending. For this matter, we are implementing an event manager, that will tell the game how to load the sucession of levels, and when there is a game over or a game ending. 

## Art

Our artist finished the job with the first (egyptian style) level: models, textures, assets and designs for the modular rooms are finished. He also created the hazards that Kiwi will face. Have a look at it. A dreadful spinning top, isn't it?

<video src="https://ies-rafael-alberti.github.io/gameoff2023/assets/img/kiwi_animations.mp4" alt="Kiwi's moving and running animations" controls="controls" style="max-width: 720px;">
</video>

## Design & Issues

As we said, we have prepared a preliminary version of the first level to test it at the end of the second sprint. We pointed out some issues so to correct them will be the main goal for the beginning of the third development week:
- Kiwi's jumping and gliding feel: we found it a bit orthopedic so we will work to refine it.
- Kiwi's tendency to stick to platform's wall when he hit thems.
- Item's detection: keys appeared at unwanted places.
- Modular rooms: We found that when the player puts a room with a door pointing to a place with no other room beside, Kiwi can enter the door and fall to the void.
- Menu and events: At this point we are wondering how to detect a next level event and how to load the next chapter board. We have to detect game over, too. 

## Next sprint

Appart from solving these problems, for the next week, we will be making art for level 2 (greek-roman style), a three lives and game over mechanics and pause menu. We also hope to finish the event system.

One more thing! We have decided a final title for our game. We will share it soon with our final game logo. We hope you like it. Stay tune!
