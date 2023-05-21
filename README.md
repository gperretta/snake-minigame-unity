<h1>DEBUG - A Snake Minigame</h1>

"Debug" is a 2D game for macOS, developed in Unity as a brief revival of the 70's arcade game, Snake - previously _Blockade_.

<h3>Table of contents</h3>
<a href="https://github.com/gperretta/snake-minigame-unity/tree/main#-gameplay-">Gameplay</a></br>
<a href="https://github.com/gperretta/snake-minigame-unity/tree/main#-game-mechanics-">Game mechanics</a></br>
<a href="https://github.com/gperretta/snake-minigame-unity/tree/main#-setting-">Setting</a></br>
<a href="https://github.com/gperretta/snake-minigame-unity/tree/main#-visual-">Visual</a></br>
<a href="https://github.com/gperretta/snake-minigame-unity/tree/main#-software-systems-and-interactions-">Software, systems and interactions</a></br>
<a href="https://github.com/gperretta/snake-minigame-unity/tree/main#-final-note-">Final note</a></br>
<img width="1920" alt="menu-bg" src="https://user-images.githubusercontent.com/113616815/228385932-3e5bb1ef-00d5-4dec-af81-49692b19a0aa.png">
</br>
<h2> Gameplay </h2>
As in the classic snake-genre games, the player can control the snake to allow it to find and eat its food - a _bug_, in this version, hence the choosen game title.
<br></br>
The player gets 100 points for each bug the snake eats, while the snake gets longer and longer. 
The speed of the game increases the more bugs are eaten to make the game more entertaining and harder.
There's only one limitation: if the snake bites itself, it will die!
<br></br>
<h2> Game mechanics </h2>
The game mechanics include collisions between objects, such as the snake and the bugs, and the movement of the snake.
When the snake exceeds the borders of the screen, it comes back from the opposite side, providing a seamless and continuous experience.
The snake's body grows incrementally as it eats more bugs and the movement of the body aligns and rotates to follow the snake head for smooth movements. 
<br></br>
<h2> Setting </h2>
The game area is a rectangular space where the snake moves around, controlled by the player using the arrow keys on the keyboard, while the bugs keep spawning in random positions of the screen.
<br></br>
<h2> Visual </h2>
The game features a cartoon-style graphics designed using Figma. 
<br></br>
<h2> Software, systems and interactions </h2>
The whole game was developed in Unity and it uses various 2D assets and Unity components such as Sprite Renderer, Box Collider 2D, Rigid Body, Text Mesh Pro and other UI elements.
A key element was the Unity input system, which drives the entire core game mechanic and gameplay.
<br></br>
<h2> Final note </h2>

This project was created as part of a two-week challenge centered around game creation, where I mostly focused on (back-end) game development, which allowed me to explore a bit more about **Unity**, while refreshing my knowledge of **C# programming**.



