# Wwise Gym List

This is a curated list of all the [Wwise Gyms](../../README.md), as defined in [Wwise Gyms for Unity](../../Unity/README.md) and [Wwise Gyms for Unreal](../../Unreal/README.md).

## Essential Features

- _Folder: `1-Essential`_
- List of essential operations used to interact with the Wwise SoundEngine.

### Adding Ambient Events to Scenes

- _Folder: `1-Essential/EssentialAmbient`_
- Posting a Wwise Event from an action that plays a sound globally.

### Posting Events to Wwise

- _Folder: `1-Essential/EssentialPostEvent`_
- Posting a Wwise Event from an action that plays a sound, bound to an object.

### Using Switch Containers to Modify Events

- _Folder: `1-Essential/EssentialSwitch`_
- Presetting a Game Object's Switches for future Wwise Event posting.

## Basic Features

- _Folder: `2-Basic`_
- List of basic operations used to interact with the Wwise SoundEngine.

### Adding Sounds to Animations (AnimNotify)

- _Folder: `2-Basic/BasicAnimNotify`_
- Posting a Wwise Event to a specific part of an animation.

### Executing Actions on Playing Events

- _Folder: `2-Basic/BasicExecuteAction`_
- List of gyms showing how to execute an action on a posted Wwise Event.

#### Break

- _Folder: `2-Basic/BasicExecuteAction/BasicExecuteActionBreak`_
- Breaking a looping Wwise Event at the next loop.

#### Pause and Resume

- _Folder: `2-Basic/BasicExecuteAction/BasicExecuteActionPauseResume`_
- Pausing and Resuming a playing Wwise Event.

#### Stop

- _Folder: `2-Basic/BasicExecuteAction/BasicExecuteActionStop`_
- Stopping a playing Wwise Event immediately.

### Playing Sounds on Collision (Footsteps)

- _Folder: `2-Basic/BasicFootsteps`_
- Connecting a Wwise Event to a trigger, such as a collision, on a bone anchor.

### Localizing Voices

- _Folder: `2-Basic/BasicLocalizedVoice`_
- Selecting a voice language for the entire application.

### Assigning Multiple Positions to a Single AkComponent

- _Folder: `2-Basic/BasicMultiplePositions`_
- Posting a single Wwise Event on multiple objects at once.

### Posting Events at location

- _Folder: `2-Basic/BasicPostLocation`_
- Posting Events at given location.

### Using Game Parameters to Modify Events (RTPC)

- _Folder: `2-Basic/BasicSetGameParameter`_
- Modifying a Game Parameter to change the resulting sound of a posted Wwise Event.

## Advanced Features

- _Folder: `3-Advanced`_
- List of complex operations, used to achieve specific goals in certain scenarios or with certain technologies.

### Ambient Operations

- _Folder: `3-Advanced/AdvancedAmbient`_
- List of operations based on ambient objects, posting events globally in a level.

#### Following an Actor

- _Folder: `3-Advanced/AdvancedAmbient/AdvancedAmbientFollow`_
- Making a level's Ambient sound follow an actor's animated rig.

#### Starting All Level Ambients

- _Folder: `3-Advanced/AdvancedAmbient/AdvancedAmbientStartAll`_
- Posting Wwise Events for every currently defined Ambient sound on a map.

### Event Operations

- _Folder: `3-Advanced/AdvancedEvents`_
- List of operations based on event object posting.

#### Posting Events and Waiting for Completion

- _Folder: `3-Advanced/AdvancedEvents/AdvancedEventsPostAndWait`_
- Posting a Wwise Event and waiting for its completion.

### Using External Sources

- _Folder: `3-Advanced/AdvancedExternalSources`_
- Posting a Wwise Event that uses media defined through the External Sources system.

### Manually Loading Event Media and SoundBanks | _Unreal_

- _Folder: `3-Advanced/AdvancedLoadData`_
- Manually loading data for a specific Wwise Event.

### Using the Sequencer to Animate Wwise Objects

- _Folder: `3-Advanced/AdvancedSequencer`_
- Using the sequencing system to drive Wwise Events, Game Parameters, and Switches.

### Using Event Callbacks to Retrieve Markers (Subtitles)

- _Folder: `3-Advanced/AdvancedSubtitle`_
- Using Markers to synchronize Wwise media and game-specific operations.

### Using the Timeline to Animate Wwise Objects | _Unity_

- _Folder: `3-Advanced/AdvancedTimeline`_
- Using the Timeline system to drive Wwise Events, Game Parameters, and Switches.

## Spatial Audio

- _Folder: `5-Spatial`_
- List of examples pertaining to properly spatializing a map's soundscape.

### Defining Rooms

- _Folder: `5-Spatial/SpatialAudio`_
- Example showing how to bound and define a properly spatialized room.

### Defining Portals

- _Folder: `5-Spatial/SpatialPortal`_
- Example showing how to define a portal between two spatial environments.

### Setting Reverb Zones | _Unreal_

- _Folder: `5-Spatial/SpatialReverb`_
- Example showing how to set up a reverberation zone.

## Testing

- _Folder: `9-Testing`_
- List of tests designed to evaluate the performance and robustness of the sound engine.

### Stress

- _Folder: `9-Testing/Stress`_
- List of tests designed to push the sound engine to its limits. Some of these tests may result in source or voice starvation, which is expected.
