# Creating Your First Gym

## Template Selection
- Empty: An empty level. This template is not recommended for first-time users.
- Simple Button: A level with a simple button. The button can be interacted with and triggers an action based on the setup. Refer to [Simple Button example](#simple-button-example) for a basic tutorial on how to set up a button to trigger an AkEvent.
- Toggle Button: A level with a toggle button. The main difference between this and the simple button level is that the button toggles between on and off. Refer to [Toogle Button example](#toogle-button-example) for a basic tutorial on how to set up a button to Start and Pause an AkEvent.
- Open Level: Two levels connected by an arch. Refer to [Open Level example](#open-level-example) for a basic tutorial on how to set up a sound to play upon switching level.

### Simple Button {#simple-button-example}

#### Using the Gym Creator

1. Find the Gym creator in the project browser.
2. Open the scene.
3. Enter Play Mode.
![Gym Creator In Browser](../../../Documentation/Images/GymCreatorProjectBrowser.png)

4. Select the SimpleButton template from the menu.
5. Name your Gym and click **Create Gym**.
6. Exit Play Mode.
![Gym Creator](../../../Documentation/Images/SimpleButtonTutorial/GymCreator.png)

7. Locate your Gym in the project browser and open the scene.

#### Adding Ak Components {#adding-ak-components}
1. Expand the **button** GameObject in the hierarchy and select the **cylinder** GameObject in order to open it in the Inspector.
![Hierarchy](../../../Documentation/Images/Hierarchy.png)
![Cylinder](../../../Documentation/Images/Cylinder.png)
Notice the Button Proximity Interaction and Button 3D Script. The Button Proximity Interaction script handles the user input ("e" by default) and notifies the Button 3D script of the interaction. The Button 3D Script determines what to do with that notification. By default, when a Gym is created, the Button 3D Script doesn't respond to interactions. The following steps explain how to configure the button to play the PlaySubtitle AkEvent when the player interacts with it.

2. Add an AkEvent Component to the Cylinder GameObject.
![AkEvent](../../../Documentation/Images/SimpleButtonTutorial/AkEvent.png)
An AkGameObj Component is created at the same time.
3. Select the Post_Subtitle_Event.
![SubtitleEvent](../../../Documentation/Images/SimpleButtonTutorial/SubtitleEventSelection.png)
4. Set the Trigger On parameter to **Nothing**.
![AkEventTriggerOnNothing](../../../Documentation/Images/SimpleButtonTutorial/AkEventTriggerOnNothing.png)

5. Add an AkBank Component to the Cylinder Game Object.
![AkBank](../../../Documentation/Images/SimpleButtonTutorial/AkBank.png)

6. Select the bank that contains the Post_Subtitle_Event. By default, for the Gyms project, the event is contained in the Default SoundBank.
![DefaultSoundBank](../../../Documentation/Images/SimpleButtonTutorial/DefaultSoundBankSelection.png)

The AkEvent and the AkBank are now successfully set up.

#### Triggering the Event on Button Press

1. Drag the Cylinder GameObject into the Button 3D (Script) Component.
![Button3DScriptGameObject](../../../Documentation/Images/SimpleButtonTutorial/Button3DScriptGameObject.png)

2. Select the menu set to **No Function** and replace it with AkEvent > HandleEvent.
![SettingHandleEvent](../../../Documentation/Images/SimpleButtonTutorial/SettingHandleEvent.png)

3. Ensure that your Inspector looks like this:
![FinalInspector](../../../Documentation/Images/SimpleButtonTutorial/FinalInspector.png)

4. Enter Play Mode and interact with the button. The Subtitle Event plays.

### Toggle Button {#toogle-button-example}

This tutorial involves some basic scripting.

1. Open the Gym creator scene.

2. Select the Toggle Button template and create your Gym.
![Gym Creator](../../../Documentation/Images/ToggleButtonTutorial/GymCreator.png)

3. Locate your Gym in the project browser and open the scene.

#### Scripting

1. To create the script, right-click an empty space in the project browser and select <b>C# Script</b>.
![ScriptCreation](../../../Documentation/Images/ToggleButtonTutorial/ScriptCreation.png)

2. Give the script a name. In the example in this tutorial, it is called **OnAndOffScript**

3. Open the script in your editor.
4. Delete the start and update functions.
![DeleteScriptContent](../../../Documentation/Images/ToggleButtonTutorial/DeleteScriptContent.png)

5. Replace the **MonoBehavior** with **OnOffManager**.
![MonoBehaviour](../../../Documentation/Images/ToggleButtonTutorial/MonoBehaviour.png)
![OnOffManager](../../../Documentation/Images/ToggleButtonTutorial/OnOffManager.png)

6. The OnOffManager is a custom class that is part of the Gyms. It contains two functions that need to be overridden: **OnAction** and **OffAction**. Add them to your script:
![OnOffAction](../../../Documentation/Images/ToggleButtonTutorial/OnOffAction.png)

7. A reference to the AkEvent is also required to play and stop it.
![AkEvent](../../../Documentation/Images/ToggleButtonTutorial/AkEvent.png)

8. Finally, you must define the **OnAction** and **OffAction** functions.
![OnOffActionDefinition](../../../Documentation/Images/ToggleButtonTutorial/OnOffActionDefinition.png)
The OnAction calls the _event **HandleEvent** function which posts the event and plays it.
The OffAction calls the **ExecuteActionOnEvent** function with **AkActionOnEventType.AkActionOnEventType_Stop**, which stops the sound.

The script is now complete, and ready to test in the Editor.

#### Editor

1. Add the AkComponents as described in the [Simple Button Tutorial](#adding-ak-components).

2. Add the OnAndOffScript to the Cylinder GameObject.
![OnOffScript](../../../Documentation/Images/ToggleButtonTutorial/OnOffScript.png)
3. Drag the AkEvent to the appropriate field in the OnAndOffScript.
![OnOffEvent](../../../Documentation/Images/ToggleButtonTutorial/OnOffEvent.png)
4. Drag the OnAndOffScript to the Button On Off (Script) field.
![OnOffOnClick](../../../Documentation/Images/ToggleButtonTutorial/OnOffOnClick.png)
5. Select the OnOffScript **Interact** function from the menu.
![OnOffFunction](../../../Documentation/Images/ToggleButtonTutorial/OnOffFunction.png)

Enter Play Mode and interact with the button to start and pause the Subtitle Event.

### Open Level {#open-level-example}

1. Open the Gym creator scene.
2. Select the Open Level Gym and create your Gym.
![Gym Creator](../../../Documentation/Images/OpenLevelTutorial/GymCreator.png)

3. The Open Level Gym contains two levels. In the hierarchy, select the TriggerBox GameObject under the Arch GameObject.
![TriggerBoxGameObject](../../../Documentation/Images/OpenLevelTutorial/TriggerBoxGameObject.png)

4. In the Inspector, there is a script called Stress Open Level_Trigger. The Level To Load parameter is a string of the name of the level to load when the player passes through the arch. When the gym is created, the value is set to the other level that was automatically created.
![TriggerBoxInspector](../../../Documentation/Images/OpenLevelTutorial/TriggerBoxInspector.png)

5. To play a sound when the second level (OpenLevelGym_2 in this tutorial) is open, you must edit the second level to load an AkAmbient at Start. To do so, open the second level.
![OpenLevel2ndLevel](../../../Documentation/Images/OpenLevelTutorial/OpenLevel2ndLevel.png)

6. After the level is loaded in the Editor, right-click in the hierarchy and select Create Empty. Name the new GameObject **AkAmbient**.
![CreateEmpty](../../../Documentation/Images/OpenLevelTutorial/CreateEmpty.png)

7. Select the AkAmbient GameObject in the hierarchy and add [the AkComponent required to play a sound](#adding-ak-components).

8. To ensure that the sound plays after the level starts, set the AkAmbient component's **Trigger On** parameter to **Start**.

9. Make sure that in the Inspector, the AkAmbient component is configured like this:
![AkAmbientFinal](../../../Documentation/Images/OpenLevelTutorial/AkAmbientFinal.png)

10. Open the first level (OpenLevelGym in this tutorial).

11. Enter Play Mode, then enter the portal in the first level to teleport to the second level. The Subtitle Event begins to play.

## Prefabs

All three GameObjects used in the tutorials (ToggleButton, OnOffButton, Arch) are available in the **Assets/Common/Prefabs** folder. Combine various GameObjects to create your own gyms.
![Prefabs](../../../Documentation/Images/Prefabs.png)