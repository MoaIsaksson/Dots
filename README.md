I have made a space shooter that is made with DOTS and I have tried to avoid having a lot of information in update functions to avoid putting a strain on the games performance but with this small game it really doesnt matter.
So, sometimes i have more information in the update function. I could have moved out almost all of the information into stand alone functions and then called that function in update.

My enemy wave system is set up so it spawns enemies and then they have a lifetime and gets destroyed after a while (It is changeable) then i have set up a timer so the game waits for a couple of seconds then it spawns a new wave of enemies. 
before i added the timer i simply destroyed all enemies when the first wave had spawned and then spawned a new wave, but i feel that the timer and life time added more dynamic to the enemies.

Use: W,S,A,D to move around and SPACE bar to shoot at the enemy. I chose to not change the input purely out of comfort, I could easily change the shoot to left mouse button.
