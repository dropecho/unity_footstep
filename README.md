An "automatic" footstep detector for humanoid animators.

This can be setup to automatically detect footsteps, or you can setup animation events and trigger the footstep detector through those, allowing you
to use just the surface detection part.

An example audio setup is included (the foot step audio component).

Surface types can be detected in a variety of ways, by tag, material, terrain texture index, or by attaching a FootStepSurface component.

[Example Video](https://www.youtube.com/watch?v=BFf3PyW09tQ)

## Install
To use this, install it through the package manager.

You can click on the code button in the top right, copy that or just copy this 
```git@github.com:dropecho/unity_footstep.git```

Open unity, and open the package manager, in the dropdown select install from git, and paste that in.

![image](https://user-images.githubusercontent.com/316782/133017967-0cfd5087-bf10-4df3-87fe-cd46549edba8.png)

![image](https://user-images.githubusercontent.com/316782/133018219-fe062677-fe74-483b-a416-a82d81b8fc9d.png)

It should then show up in the list of installed packages  
![image](https://user-images.githubusercontent.com/316782/142772781-1e59901a-db67-4765-ba4d-c913f4a7925f.png)

## Quickstart

For the most basic setup, do this  
![image](https://user-images.githubusercontent.com/316782/146982575-54f79832-41af-4026-bdda-bcf1788be5c9.png)  
You should be able to start playmode and have it begin working.

Look at usage below for more details.

## Usage

Attach a foot step detector to the desired game object and assign the animator.  
![image](https://user-images.githubusercontent.com/316782/146982675-095cc9e7-0693-455a-9be6-d0fbdc157e39.png)


Create a surface type (under dropecho/character/surfacetype), and setup the desired data.  
![image](https://user-images.githubusercontent.com/316782/146982748-27177d25-ea26-4a53-98ea-a5ca9babfc75.png)

Assign the surface type to the foot step detector.  
![image](https://user-images.githubusercontent.com/316782/146983018-685d61d1-e0f2-430f-96fb-5425cc3ed305.png)


To play audio, attach a foot step audio component, and assign a surface type and audio clip, it will 
play the clip based on either the surface type index (i.e. surface 0 is clip 0), or the default clip if no surface is detected.  
![image](https://user-images.githubusercontent.com/316782/146982826-7c73b388-e1fc-4bfa-a973-fef58b3d2b27.png)

