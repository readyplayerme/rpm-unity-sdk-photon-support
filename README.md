# Ready Player Me Unity SDK - Photon Support

Photon Unity Networking 2 support for Ready Player Me avatars.

Photon requires characters to be preconfigured prefabs to be instantiated when a player joins a room, and RPM avatars are used by loading them at runtime. This package helps solve this problem by providing an avatar prefab and a component that handles avatar loading by transferring the avatar mesh and material to a character prefab.

This package provides an `RPM_Photon_Character` prefab to be instantiated, a `NetworkPlayer` component to handle the avatar loading and an `Avatar Control` sample scene to test the avatar in a multiplayer environment.

## Requirements
- [Photon Unity Networking 2](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922) (PUN2)
- Photon Settings configured with App ID and Region
- [Ready Player Me Core](https://github.com/readyplayerme/rpm-unity-sdk-core)

## Installation
- Copy the git URL below.
  ```
  https://github.com/readyplayerme/rpm-unity-sdk-photon-support.git
  ```

- Open the `Window > Package Manager` menu, click on the Plus (+) button and select `Add package from git URL`.

  <img width="500" alt="image" src="https://github.com/readyplayerme/rpm-unity-sdk-photon-support/assets/3163281/54874f4e-8ff9-4011-aca1-0826ac0538a7">

- Paste the URL in and then click on the `Add` button.

  <img width="500" alt="image" src="https://github.com/readyplayerme/rpm-unity-sdk-photon-support/assets/3163281/e0601a53-e233-4c2a-b602-5dedfbb2a560">

- After the package is installed you should see it under the Ready Player Me section.

![Screenshot 2023-10-12 083913](https://github.com/readyplayerme/rpm-unity-sdk-photon-support/assets/7085672/5a87492f-9491-4403-ba70-fc3ac2702336)

## Testing the Sample Project
- Select Ready Player Me Photon Support in the Package Manager and import the Avatar Control sample.
- Open the Avatar Control scene.
  
  <img width="500" alt="image" src="https://github.com/readyplayerme/rpm-unity-sdk-photon-support/assets/3163281/ae87e2a4-4c5e-41f8-ab0f-ef90a3aaa5ff">


- Run the scene, paste your avatar URL and click on the load button.
- Build the scene and run it on another device to observe multiple avatars in the same scene.
  
  <img width="500" alt="image" src="https://github.com/readyplayerme/rpm-unity-sdk-photon-support/assets/3163281/c18f2c83-195b-434b-bfe2-3cbf943c1535">
  
## Custom Use
From the Ready Player Me menu you can copy an instance of Ready Player Me Character Prefab into the `Ready Player Me/Resources/RPM_Photon_Character.prefab` file and modify it for your use case.

The prefab has attached NetworkPlayer, Animator, PhotonView, PhotonAnimatorView and PhotonTransformView components. Feel free to modify, add or remove components and change the animation controller you are using.

The NetworkPlayer component requires an `AvatarConfig` and currently only works with Atlased avatars (with a config setting of `TextureAtlas != none`). You can create a new AvatarConfig with your multiplayer characters and use it with your NetworkPlayer component.

In your code, where you instantiate your multiplayer character prefab, you can load the `RPM_Photon_Character.prefab` and call the LoadAvatar method with your avatar URL. This will run a Remote Procedure Call (RPC) that will notify all other players to load the avatar on their instance of the player.

``` cs
GameObject character = PhotonNetwork.Instantiate("RPM_Character", Vector3.zero, Quaternion.identity);
character.GetComponent<NetworkPlayer>().LoadAvatar(inputField.text);
```

## Known Issues
- **Avatar Partially Loading:** If you did not use a Texture Atlas size selected in your Avatar Config, you will receive an avatar in multiple meshes and you might observe only eyes of the avatar being loaded and that you are getting an `IndexOutOfRangeException: Index was outside the bounds of the array.` error message. Multiple mesh avatars are not yet supported in the package so please use a config with an atlas size selected.
- **Shared Access Violation Error:** When you test your build locally, all the game instances will try to write to the same avatar file simultaneously due to first-time download and cause a file read error. Even though remote players won't experience this issue it is troubling while testing your app. You might test with same avatars and after they are cached this issue should not occur.
