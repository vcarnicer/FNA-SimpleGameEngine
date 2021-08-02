# FNA-SimpleGameEngine
A simple game engine made with [FNA](https://github.com/FNA-XNA/FNA) while following the [Michael Hicks FNA tutorial](https://www.youtube.com/playlist?list=PL3wErD7ZIp_DtsTKoouVCxu81UQkI9VZL).

It could also be helpful to download the [Michael Hicks Toolbox](https://www.dropbox.com/s/byhp6e4eq40htfc/Michael%20Hicks%20Toolbox.zip?dl=0), a collection of scripts, 
explanations and all the original assets that Michael Hicks made for the tutorial and are used to make the engine (unimplemented). The engine on this repo already implements everything from the Toolbox.

Currently runs tested only on latest Visual Studio 2019 on Windows, I will try to port it to VSCode using [this template](https://github.com/prime31/FNA-VSCode-Template) which includes VSCode integration (build tasks and more) and the [Nez Engine](https://github.com/prime31/Nez).
**EDIT**: If you want to make a game and not a Monogame/FNA engine, definitely check out the Nez Engine, which is packed with features and tested.


# How to start
Clone this repo **recursively**, like this:
```
git clone --recursive git://github.com/vcarnicer/FNA-SimpleGameEngine.git
```

It is very important that you do clone recursively, as dependencies are added as submodules (linked to their original project).

## Dependencies
#### You don't need to download/clone these, as this project has these dependencies as submodules. Just clone recursively to get them all.

To update the submodules (FNA and fnalibs), this git command should do (please open an issue if they don't):

```
git submodule update --init --recursive
```

The dependencies installed are:
#### FNA
- FNA: https://github.com/FNA-XNA/FNA
- (FNA Wiki) https://github.com/FNA-XNA/FNA/wiki

#### fnalibs
Native libraries for FNA to work.
- fnalibs: https://github.com/FNA-XNA/FNA/wiki/1:-Download-and-Update-FNA
- That previous link is the primary source, but I added fnalibs as a submodule from [this repo](https://github.com/deccer/FNA-libs), which is a repo version of fnalibs that updates every hour to check for updates on the original fnalibs.

## How to add new Objects
>Extracted from "How to add new Objects.txt" from the [Michael Hicks Toolbox](https://www.dropbox.com/s/byhp6e4eq40htfc/Michael%20Hicks%20Toolbox.zip?dl=0), used for the [Michael Hicks FNA tutorial](https://www.youtube.com/playlist?list=PL3wErD7ZIp_DtsTKoouVCxu81UQkI9VZL).

This guide will show you how to add a new GameObject to your engine (like a new type of enemy or powerup).
1. Make a new class for your GameObject, copy over the `using` statements and make the class `public`.

2. Make the class derive from the appropriate parent class. This could be `GameObject`, `AnimatedObject` or `Character`.

3. Make sure the .png file you're trying to load is in the Content folder, and the "Copy to Output" option in the Properties
window is set as "Copy if newer". This way the file is copied next to the .exe when the game is compiled.

4. Override the Load function to look something like this:
```csharp
public override void Load(ContentManager content)
 {
    //Load our image/sprite sheet:
    image = TextureLoader.Load("spritesheet", content);

    //Load any animation stuff if this object animates:
    LoadAnimation("ShyBoy.anm", content);
    ChangeAnimation(Animations.IdleLeft); //Set our default animation.

    //Load stuff from our parent class:
    base.Load(content);

    //Customize the size of our bounding box for collisions:
    boundingBoxOffset.X = 0;
    boundingBoxOffset.Y = 0;
    boundingBoxWidth = animationSet.width; //or use image.Width if it's not animated
    boundingBoxHeight = animationSet.height; //or use image.Height if it's not animated
 }
```
5. Right click Editor.cs and click View Code. At the top in the ObjectType enum, type the name of the new object so we can drag
and drop it from the Editor. Make sure you add all new objects BEFORE NumOfObjects, that one needs to be the last item in the list.

6. Make sure the objectsNamespace variable is set correctly. There's a comment next to the variable that shows you how to do it.

7. Return to the new class you made. Override the Update function so you can program the logic needed for your object. Here’s an
example of how it would look:
```csharp
public override void Update(List<GameObject> objects, Map map)
  {
    base.Update(objects, map);
  }
```

8. (Optional) If your object is animated... you can override the UpdateAnimations function to write the animation logic. Example:
```csharp
protected override void UpdateAnimations()
   {
      if (currentAnimation == null)	
	      return; //Animation isn't loaded, so return.

      base.UpdateAnimations();

     //TODO: ADD YOUR ANIMATION LOGIC HERE AT THE BOTTOM!
   }
```

9. Make sure you add the following code above the objects list in LevelData.cs:
```csharp
[XmlElement("NAMEOFYOURNEWCLASS", Type = typeof(NAMEOFYOURNEWCLASS))]
```

10. Make sure your new class has an EMPTY constructor. If XML doesn't find an empty constructor it will crash while saving levels!
