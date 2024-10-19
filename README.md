# Movement Tutorial

This is a quick Unity tutorial to get a GameObject to move, given player input.

## Prerequisites

Before approaching this tutorial, you will need a current version of Unity and a code editor (such as Microsoft Visual Studio Community) installed and ready to use.

This tutorial was created with Unity 2022.3 LTS and Microsoft Visual Studio Community 2022 versions. It should work with earlier or later versions although you should check the release notes for other versions for changes to Editor controls and the Scripting API.

If you need help installing Unity you can many online tutorials such as:
https://learn.unity.com/tutorial/install-the-unity-hub-and-editor

You will also need to know how to create an empty project, add primitive objects to your scene, create blank scripts, and run projects from within the editor. If you need help with this, there is a short video demonstrating how to do all of these things here: 

<video src='https://www.youtube.com/watch?v=eQpWPfP1T6g'></video>


## Objectives

In this tutorial, we will write a script that takes player input in the form of key presses or gamepad joystick movements and use it to move a Unity `GameObject` at an adjustable speed that is framerate independent.

https://github.com/user-attachments/assets/0f9e451a-8233-4a7b-81a0-881b697ba2be

## Getting started

To begin with, create a new Unity project, add a cube object, and then create a new script called `Player`. Make sure your new script is added to the cube object.

It's a good idea to run the project at this stage to make sure there are no errors or problems to deal with before starting to code.

We can break our objective into to two separate tasks and then join the results. One is to get input from the player in the form of either key presses or gamepad inputs. The other is to use those inputs to change the position of the cube within the Unity scene. We can tackle these in either order.

In this tutorial I am going to start with the movement of the cube.

## Moving a GameObject

If you look at the Inspector Window while you have your cube selected in the Unity Editor, it will show all the current properties of the cube. At the top of this list is the `Transform` component.

![the transform component highlighted in red](https://github.com/user-attachments/assets/ffdfb178-94f7-4b82-a33d-1d5ec4aee6c4)

The transform component is responsible for maintaining the position of the cube within the scene. If you modify the position co-ordinates in the inspector, the position of the cube will change relative to the camera.

https://github.com/user-attachments/assets/b3ca875e-13b6-4bb5-8c0f-3fe4296f39c2

