An "automatic" footstep detector for humanoid animators.

This can be setup to automatically detect footsteps, or you can setup animation events and trigger the footstep detector through those, allowing you
to use just the surface detection part.

An example audio setup is included (the foot step audio component).

Surface types can be detected in a variety of ways, by tag, material, terrain texture index, or by attaching a FootStepSurface component.

## Quickstart

Look at usage below.

## Install
To use this, install it through the package manager.

You can click on the code button in the top right, copy that or just copy this 
```git@github.com:dropecho/unity_footstep.git```

Open unity, and open the package manager, in the dropdown select install from git, and paste that in.

![image](https://user-images.githubusercontent.com/316782/133017967-0cfd5087-bf10-4df3-87fe-cd46549edba8.png)

![image](https://user-images.githubusercontent.com/316782/133018219-fe062677-fe74-483b-a416-a82d81b8fc9d.png)

It should then show up in the list of installed packages  
![image](https://user-images.githubusercontent.com/316782/142772781-1e59901a-db67-4765-ba4d-c913f4a7925f.png)


## Usage

Attach a foot step detector to the desired game object and assign the animator.

Create a surface type (under dropecho/character/surfacetype), and setup the desired data.
Assign the surface type to the foot step detector.

To play audio, attach a foot step audio component, and assign a surface type and audio clip, it will 
play the clip based on either the surface type index (i.e. surface 0 is clip 0), or the default clip if no surface is detected.
