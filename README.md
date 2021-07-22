# FNA-SimpleGameEngine
A simple game engine made with FNA while following the [Michael Hicks FNA tutorial](https://www.youtube.com/playlist?list=PL3wErD7ZIp_DtsTKoouVCxu81UQkI9VZL).

Currently runs tested only on latest Visual Studio 2019 on Windows, will try to port it to VSCode using this template https://github.com/prime31/FNA-VSCode-Template which includes VSCode integration (build tasks, more) and the Nez Engine (https://github.com/prime31/Nez).


# How to start
Clone this repo **recursively**
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
Native libreries for FNA to work.
- fnalibs: https://github.com/FNA-XNA/FNA/wiki/1:-Download-and-Update-FNA
- That previous link is the primary source, but I added fnalibs as a submodule from https://github.com/deccer/FNA-libs, which is a repo version of fnalibs that updates every hour to check for updates on the original fnalibs.

