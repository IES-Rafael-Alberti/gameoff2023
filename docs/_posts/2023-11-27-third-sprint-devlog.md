---
layout: post
title: "Third Sprint Devlog!"
date: 2023-11-27
tags: [devlog, gameoff2023, gameart, gamedesign]
comments: true
---

![Treasure Room Teaser](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/Teaser3Bright.jpg) 
Hello there! How are you? We are tired, very tired, but still alive xD The final countdown for Github's Game Off draws near, but Project Kiwi is growing well and healthy.
Oh, wait, I said Project Kiwi, but as you may have seen in our landing page, we have a final title and a logo. A beautiful one indeed! Say hello to Kiwi's Adventure!
We have to thank our artist Rubus for such and amazing illustration!

And now it is time to share our progressions with you:

## Design

<video src="https://ies-rafael-alberti.github.io/gameoff2023/assets/img/KiwiTeaser2.mp4" alt="Kiwi VS Chainsaw Spinning Top" controls="controls" style="max-width: 720px;">
</video>

First things first, we decide to baptize our game with a simple, direct and clear title that focus in the value of our MC. We considered a subtitle: "Dungeon Slider" pointing directly to the gameplay. 
We could say that this is the genre of our game. Other options that were discarded were: "Treasure Measure" or "Kiwi Scales".

Another thing we tried but didn't implement was to develop the game story. A teacher from our Game Development Degree suggested us to use an AI tool to take some ideas, so we did it a try. 
Some of the results were very interesting, for example we asked the AI for a motto, aphorism or moral inspired by the levels' cultures. In was surprising how the AI gave them a poetic touch. It said some sentences that could be adapted in our game as teachings that Kiwi obtains after beating a level.
We prefer not to use any AI tool, because we prefer to do all our work by ourselves, but it can be of some help to fight the blank page syndrome.
It's true that you have to refine the AI conclusions by giving it information, otherwise the first results tend to be stereotypical (we found some anime and movie clichés asking the AI for Kiwi's backstory).
Although you have those eternal themes (way of the hero, chosen one, nemesis, hero's lineage) that can be remixed with fresh ideas to create something unique. At this point, the AI can be useful to connect some apparently distant ideas. 

We finished the design for level two and its modular rooms and the sheet design of level three. We decided that the game will have three levels for the jam, though for a later versions we would like to add a fourth level.

## Code

![Unity's Animator and Input System](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/tr3.PNG) 

Our main programmer BoofWoof is refining the input system. We will have two controls types: keyboard and gamepad. Keyboard one will use WASD, space, Q, E and R.
During our tests, we checked that gamepad controls were nicer due to the 3D nature of our game. The work of our programmer is to connect correctly the input system with the animator and the physics so the game feel be the most satisfactory version as possible. 
This issue involves velocity, gravity, collisions, etcetera. He has fixed all bugs found during the previous week: wall sticking, air walking, void falling (see our last post for more info).

Our programmers are also finishing the mechanics for hazards and interactible objects for level two and three. While I'm writing, the core gameplay is virtually finished. We have to join all pieces left. 
Our game occurs in one Unity Scene and the progress is hold by several scriptable objects that saves the level information (grid composition, modular rooms, current level, prefabs, etcetera).

![Unity's Scriptable Objects](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/tr1.PNG) 

Speaking of the game flow, we have finished the complete structure: logo sequence, main menu scene (with new game, continue and exit options), main game scene and pause menu (with resume, restart and exit options).
We can say that the "skeleton" of the game story is prepared for being decorated by our artist. 

## Art

Our artist is on a tour de force to finish assets for level 3 and game hub (treasure room), adjust illumination, draw UI elements, everything to get to the finish line in time.
Here you have a sneak peek of his work.

![Greek-Roman Level Teaser](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/image.png) 

![Platforming Mode Teaser](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/c2.PNG) 

## It's the final countdown nanananaaa...

And what about music? We have to say hello to audio designer Aradian that incorporated our team last week. He has made the music and sound effects for the three levels. We have to say that Kiwi's sound are adorable!

Okey, so with all pieces joined, for this last week we have to finish level 3 in Unity, plus UI elements and lasts sound and graphic adjustments. We will take some tests, because there are some mechanics that we want to fix. So we are crossing fixes to see the game finished on time.
See you at the Game Off!

![Kiwi Logo](https://ies-rafael-alberti.github.io/gameoff2023/assets/img/KiwiLogo.jpg) 
