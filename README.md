# Movement Tutorial

This is a quick Unity tutorial to get a GameObject to move, given player input.

## Prerequisites

Before approaching this tutorial, you will need a current version of Unity and a code editor (such as Microsoft Visual Studio Community) installed and ready to use.

This tutorial was created with Unity 2022.3 LTS and Microsoft Visual Studio Community 2022 versions. It should work with earlier or later versions. But you should check the release notes for other versions as the Editor controls or Scripting API functions may have changed.

If you need help installing Unity you can find many online tutorials such as:
https://learn.unity.com/tutorial/install-the-unity-hub-and-editor

You will also need to know how to create an empty project, add primitive objects to your scene, create blank scripts, and run projects from within the editor. If you need help with this, there is a short video demonstrating how to do all of these things here: 

https://www.youtube.com/watch?v=eQpWPfP1T6g


## Objectives

In this tutorial, we will write a script that takes player input in the form of key presses or gamepad joystick movements and use it to move a Unity `GameObject` at an adjustable speed that is framerate independent.

https://github.com/user-attachments/assets/0f9e451a-8233-4a7b-81a0-881b697ba2be

## Getting started

To begin with, create a new Unity project, add a cube object, and then create a new script called `Player`. Make sure your new script is added to the cube object.

It's a good idea to run the project at this stage to make sure there are no errors or problems to deal with before starting to code.

We can break our objective into two separate tasks and then join the results. One is to get input from the player in the form of either key presses or gamepad inputs. The other is to use those inputs to change the position of the cube within the Unity scene. We can tackle these in either order.

In this tutorial I am going to start with the movement of the cube.

## Moving a GameObject

If you look at the Inspector Window while you have your cube selected in the Unity Editor, it will show all the current properties of the cube. At the top of this list is the `Transform` component.

![the transform component highlighted in red](https://github.com/user-attachments/assets/ffdfb178-94f7-4b82-a33d-1d5ec4aee6c4)

The transform component is responsible for maintaining the position of the cube within the scene. If you modify the position co-ordinates in the inspector, the position of the cube will change relative to the camera.

https://github.com/user-attachments/assets/b3ca875e-13b6-4bb5-8c0f-3fe4296f39c2

To make the position update from our script all we need to do is modify the `Transform` component of the game object.

## Scripting movement

If you double click on the `Player` script, the Visual Studio editor should open with the code in the main editor window. The initial code should look like this:

```cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```

To modify the transform of the object this script is attached to, all we need do is refer to it by its variable name `transform`. This is one of the variables inherited by our player class from the Unity supplied `MonoBehaviour` class. The line defining the `Player` class:

```cs
public class Player : MonoBehaviour
```

- `public` means that this class can be accessed from other scripts
- `class` means that what follows is a class definition
- `Player` is the name we supplied for the class
- `:` means this class inherits functions and variables from another class
- `MonoBehaviour` is the Unity supplied class we are basing our new class on

You can look up what [`MonoBehaviour`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/MonoBehaviour.html) supplies in the Unity documentation. And if you scroll through that documentation (there is quite a lot) you will eventually get to the [`transform`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Component-transform.html) property. This property is automatically initialized by Unity and allows you to reference the `Transform` component of the `GameObject` this script is on.

If you follow the links in the documentation, you will see that [`Transform`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Transform.html) itself is another Unity supplied class. And it has a number of useful functions that we can use to modify the transform of an object.

To move the object in the scene, the simplest function to use is [`Translate`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Transform.Translate.html). And we can use that by typing an instruction into the `Update()` function of our `Player` script:

```cs
    // Update is called once per frame
    void Update()
    {
        transform.Translate
    }
```

> [!CAUTION]
> The capitalization of the words is important here. So `transform` must begin with a lower case `t` and `Translate` must begin with an upper case `T`. Using alternative capitalization or spelling will result in syntax errors.

> [!NOTE]
> This follows the convention in Unity code that variables start with lower case letters whereas classes and functions start with upper case letters. This is not a requirement of the language, but it's a good idea to stick to this convention when writing your own Unity scripts.

As well as telling Unity that we want to translate the position of this object, we also have to tell it how much to translate in `x`, `y`, and `z`. Fortunately, Unity provides some handy predefined constant values we can use to test this out. For example [`Vector3.right`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Vector3-right.html) is shorthand for a 3 dimensional vector value where `x` is 1 and `y` and `z` are 0. This means that `Vector3.right` should move us to the right by one Unity unit.

> [!CAUTION]
> We also need to specify what we mean by "to the right". By default (that is unless you specify otherwise) all movements are considered to be relative to the object's own orientation. So if an object is upright and pointing along the global `x` axis, instructing unity to move `Vector3.right` will actually change the `z` position.

To pass this information to the `Translate` function we need to enter the value inside brackets. These are called the arguments or parameters of the function.

```cs
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right)
    }
```

Finally, to make this a correct program instruction as far as the c# language is concerned, we have to end the line with a semi-colon character:

```cs
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right);
    }
```

To test this out, save the edits we have made to the `Player` script and switch back to the Unity window.

If everything is working correctly, you should see the cube shoot off to the right. It is going a bit fast, but we'll deal with that in the next step.

## Compensating for frame rate

___Why does the cube go so fast?___

The clue is in the comment that Unity helpfully provides when it generates a new script:

```cs
// Update is called once per frame
```

In video games, we generate the illusion of movement by redrawing the scene many times per second and making small adjustments to the position of the objects. To see how fast our game is currently running, click on the Stats button at the top of the Game window.

![the stats button highlighted in red](https://github.com/user-attachments/assets/97d0aa01-ca71-4c67-b53c-717a624fa3ee)

In this capture from my PC, the game graphics are updating at more than 1,000 FPS! Since we have told Unity to move our cube one Unity unit each frame, that means for me it is moving at something like 1,000 Unity units per second.

Different machines will run at a faster or slower pace depending on the speed of the CPU and type of Graphics card installed. If we want our movement to be consistent no matter what device we run our game on, we will have to compensate for this difference in update rate.

Fortunately, Unity provides us with a variable value that allows us to do that. That value is called [`Time.deltaTime`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Time-deltaTime.html). The documentation defines this value as "The interval in seconds from the last frame to the current one."

___How does this help?___

If we divide our one second by this interval we will get the total number of updates we'd expect in that time. And if we wanted to move a fixed distance in one second then we could divide that distance by the number of updates.

In code we could write that as:

```cs
Vector3.right / (1 / Time.deltaTime)
```

> [!TIP]
> Divide instructions are usually slower to execute than multiply instructions. Usually it is better to rewrite divides as multiplies if possible.

But because Unity provides `deltaTime` as a fraction of a second we can use the [reciprocal method](https://www.bbc.co.uk/bitesize/articles/z78vgdm) to get the following equivalent expression:
```cs
Vector3.right * Time.deltaTime
```

Switch back to Visual Studio and modify the `Update` instruction:

```cs
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }
```

Save the changes and run the project in Unity. You should now see the cube move at a consistent pace to the right. If you watch the `x` coordinate in the Inspector window, you should see it increasing at a rate of 1 every second.

## Adjusting for speed

Now we have our object moving at a consistent speed, we can add a variable to make the speed adjustable. For example, if we wanted to move at 2 Unity units per second instead of 1, we could set a `speed` variable to be `2`.

To add a variable to a Unity script and have it appear in the Unity inspector window, add a variable definition somewhere within the class. The convention here is to add these at the top of the class definition, before the functions.

```cs
public class Player : MonoBehaviour
{
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    ...
```

In this definition:

- `public` means this variable can be referenced by other scripts
- `float` is the type of the variable (a floating point number)
- `speed` is the name of the variable
- `= 10` is the default value
- `;` is the end of this variable declaration

If you save at this stage and check the Inspector window in Unity, you should see the new variable `speed` listed under the `Player` component and it should be set to your supplied default value.

![the speed variable as it appears in the Unity inspector window](https://github.com/user-attachments/assets/6ad6d2cc-f688-4d11-b5c6-1de7b2b9851f)

> [!NOTE]
> If you modify this value in the Unity inspector window, your script will use that value and not the number you wrote in the script. The value in the inspector window overrides the value in the script. And if you put your script on more than one object in Unity, each will have it's own `speed` value.

To use this variable in your script, switch back to Visual Studio. The value we want to modify is `Vector3.right`. This value represents one Unity unit in `x`. If we want it to represent more (or less distance) we can simply multiply it.

Change the `Update` instruction to:

```cs
transform.Translate(Vector3.right * speed * Time.deltaTime);
```

Now save and switch back to Unity and play. The cube should now move at a rate defined by the speed variable in the `Player` component. You can check this by modifying the value in the inspector.

> [!NOTE]
> If you put in a negative value for speed, you can get the object to travel in the opposite direction. And if you put in zero speed, the object will stop. We will use this feature later to implement input controls.

If you look closely in the Visual Studio editor window you should see three grey dots under the `Vector3.right` value. This is a Visual Studio hint. What it tells us is that this expression can be re-written to improve performance.

![the Visual Studio editor hint indicator](https://github.com/user-attachments/assets/8e6a1369-9ae6-42e3-bf63-0a36d37555c3)

The reason is that c# will perform these calculations in the order given from left to right. Since we are starting with a 3D vector (x y z) each coordinate will be calculated by multiplying by speed and then delta time for a total of 6 multiplies. However, if we multiply by the vector last, the speed and delta time are multiplied first and the result is then multiplied by each of x, y, and z for a total of 4 multiplies. That's a 30% reduction in calculations just by re-ordering the code! In single cases like this, you will not notice the reduction. But in code that loops many times the savings can be significant.

We can either re-order the multiplication or add brackets to force the calculation into the order we want. Either way, this will remove the hint from the Visual Studio editor.

```cs
transform.Translate(speed * Time.deltaTime * Vector3.right);
```

## Getting player input

There are currently two systems for getting player input within Unity. These are known as `Input Manager (Old)` and `Input System Package (New)`. The old system is still the default option when you create a new Unity project, so we will use that in this tutorial. However, it would not be hard to swap out that system for the new one and make it work in this tutorial.

To get input from the `Input Manager` we use the [`Input`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Input.html) class. A look through the documentation there should show the [`GetAxis`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Input.GetAxis.html) function which will return a value from a named axis control mapped either to a GamePad controller or to the keyboard.

To see what named axes are available for input in the `Input Manager`, you can look through the `Input Manager` section in the `Project Settings` in the Unity editor. You can open the `Project Settings` window from the `Edit` menu on Windows.

> [!NOTE]
> This option may be under a different menu on other platforms than Windows.

![the input manager section of the project settings window listing all of the mapped axes](https://github.com/user-attachments/assets/a4750213-278e-42a8-ad1f-45fe0385203d)

In this list you can see that the axes are defined twice. The first is the keyboard mapping and the second is the mapping for the GamePad.

Unlike the `Translate` function we used earlier, the `GetAxis` function returns a value. That value represents the current position of the virtual input axis in the range from -1 to 1. Since we want to use that value, the neatest way to handle this is to define a local variable to hold it.

Defining a local variable is similar to the way we defined the `speed` variable earlier. However, this time we don't have the option of `public` access, since this variable is temporary and will only exist within the scope of the `Update` function.

```cs
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }
```

In this new line:
- `float` is the type of the local variable
- `input` is the name of the local variable
- `=` means we are supplying an initial value
- `Input` is the name of the `Input Manager` class
- `GetAxis` is the name of the function we are calling to get the result
- `Horizontal` is the name of the virtual axis we want to read

> [!NOTE]
> The quotes and capitalization of `"Horizontal"` are important here. The quotes mean that we are supplying the name of an item of data within the `Input Manager` and not a c# script name. The spelling and capitalization therefore must exactly match the entry defined in the `Input Manager` window above. Because this lookup is done only when you run the program, you will not get any warnings if the name does not match until you press the run button.

Save the script and run the program in Unity. It won't do anything yet, but if you get an error here you may have spelled the input axis name incorrectly. For example, if I write `GetAxis("Horzontal")` then I will get the following error message every frame:

![an error showing an attempt to access an axis not defined in the input manager settings](https://github.com/user-attachments/assets/036305f6-ade1-4962-9fee-9b3905d47fec)

Assuming you don't get any errors here we can continue.

## Testing the input

Before we hook the input into our movement expression, let's write a quick test to see if the input values are coming out correctly.

There are many ways to test variable values within a Unity script. One of the easiest is to use the instruction [`Debug.Log`](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Debug.Log.html). Whatever you supply as an argument to `Debug.Log` it will output as a message in the Console window in Unity.

If we add a `Debug.Log` line to our script:

```cs
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Debug.Log(input);
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }
```

Save and run in Unity. We should see a stream of text messages in the Console window showing the value of the virtual `Horizontal` input axis.

When you are not touching the keyboard or a connected gamepad, a `0` should appear in the console.

Holding down the left arrow key, the `A` key, or pushing a connected gamepad stick to the left, should produce a `-1` output in the console.

Holding down the right arrow key, the `D` key, or pushing a connected gamepad stick to the right, should produce a `1` in the output.

If you get this we can delete the `Debug.Log` line and instead connect the input to our movement code.

## Applying the input to the movement

We saw in the earlier step, that giving the object a negative speed made it move in the opposite direction. And we saw in the previous test that pressing left turns the `input` value to `-1`. So all we need to do to change our movement from right to left is to multiply it by the `input` variable.

| input | speed | input * speed |
| ----: | ----: | ------------: |
|     1 |    10 |            10 |
|     0 |    10 |             0 |
|    -1 |    10 |           -10 |

To make this change in code, all we need to do is modify the argument for `Translate`:

```cs
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(input * speed * Time.deltaTime * Vector3.right);
    }
```

Save and switch back to Unity to test. You should now get the behaviour we saw earlier:

https://github.com/user-attachments/assets/0f9e451a-8233-4a7b-81a0-881b697ba2be

## Tidy up

It's always a good idea to tidy up a project after you get it working. And even though we don't have much code, there are still a couple of hints from the editor.

As noted earlier, there are some more grey dots under the `Start` function. Here, the hint is "The Unity message 'Start' is empty." This is letting us know that if we have an empty function that is not directly referenced by other code, we can just delete it.

Also, if you look at the top of the script, you'll notice that the top two lines are slightly fainter than the following one. And if you click on those lines, you'll see a light bulb icon appear to the left.

![the light bulb icon indicating a Visual Studio editor suggestion](https://github.com/user-attachments/assets/101c3a88-cbdd-4058-8f34-57572d03edd2)

And if you click on that icon, the hint suggests you remove unnecessary usings. That is telling you that Visual Studio has detected that the top two lines are not required and can be deleted. Is it right? The easy way to find out is to delete those lines and see if it still works.

Finally, the comment `// Update is called once per frame` might be useful to an absolute beginner, but now you've got this far it is already old news. For me, this just lengthens the script for no real benefit, so I would delete it too. That leaves the final script looking like this:

```cs
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(input * speed * Time.deltaTime * Vector3.right);
    }
}
```

Nice!