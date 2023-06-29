## General idea:
- user is playing a car (me handle) that is overtaking other cars 

- Other cars = obstacles 
    - it handle 
---
## we need...
### it handle:
- spawns a different locations 
- moves linear towards the user
- "disappears" after a certain line & respawns at beginning
- count how many times it moved past that line [speech: success/ count loudly]
    - measurement for winning the level -> set goal, [speech: success]

### me handle:
- walls to indicate the road
- can be moved freely
### me handle / it handle:
- detect collision (= same x/y coordiantes)
    - restart level [speech: failure, restart]
### explanataion:
- slowly drive it handle towards user, explaining "this is a car blabla"
- wiggling me handle "this is you, avoid the car"
