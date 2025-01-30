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

### Playing Sounds on Collision based on the Surface (Footsteps)

- _Folder: `2-Basic/BasicFootstepsDynamic`_
- Connecting a Wwise Event to a trigger, such as a collision with a Raycast on a bone anchor and getting material to set a Switch value.

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

- _Folder: `2-Basic/BasicSetGameParameter/BasicSetGameParameterOnObject`_
- Modifying a Game Parameter to change the resulting sound of a posted Wwise Event on a game object.

### Using Game Parameters to Modify global parameters (RTPC)
- _Folder: `2-Basic/BasicSetGameParameter/BasicSetGlobalGameParameter`_
- Modifying a global Game Parameter.

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

### AudioLink Video | _Unreal_

- _Folder: `3-Advanced/AdvancedAudioLinkVideo`_
- Sending Unreal MediaPlayer audio to Wwise via AudioLink.

### Capturing Profiler File

- _Folder: `3-Advanced/AdvancedCaptureProfiler`_
- Start and stop capturing a Profiler session.

### Event Operations

- _Folder: `3-Advanced/AdvancedEvents`_
- List of operations based on event object posting.

#### Posting Events and Waiting for Completion

- _Folder: `3-Advanced/AdvancedEvents/AdvancedEventsPostAndWait`_
- Posting a Wwise Event and waiting for its completion.

#### Posting Events with a Cooldown Period

- _Folder: `3-Advanced/AdvancedEvents/AdvancedEventsCooldown`_
- Posting a Wwise Event with a Cooldown Period that prevents the Event from being posted again during that time.

### Using External Sources

- _Folder: `3-Advanced/AdvancedExternalSources`_
- Posting a Wwise Event that uses media defined through the External Sources system.

### Manually Loading Event Media and SoundBanks | _Unreal_

- _Folder: `3-Advanced/AdvancedLoadData`_
- Manually loading data for a specific Wwise Event.

### Using the Sequencer to Animate Wwise Objects

- _Folder: `3-Advanced/AdvancedSequencer`_
- Using the sequencing system to interact with Wwise Events and Objects

- _Folder: `3-Advanced/AdvancedSequencer/AdvancedSequencerPlain`_
- Using the sequencing system to post Wwise Events

- _Folder: `3-Advanced/AdvancedSequencer/AdvancedSequencerSpatialized`_
- Using the sequencing system to drive Wwise Events and Game Parameters

### Using Event Callbacks to Retrieve Markers (Subtitles)

- _Folder: `3-Advanced/AdvancedSubtitle`_
- Using Markers to synchronize Wwise media and game-specific operations.

### Using the Timeline to Animate Wwise Objects | _Unity_

- _Folder: `3-Advanced/AdvancedTimeline`_
- Using the Timeline system to drive Wwise Events, Game Parameters, and Switches.

### Using GetSourcePlayPosition to synchronize a Video with a Wwise Event

- _Folder: `3-Advanced/AdvancedVideoAudioSync`_
- Posting a Wwise Event and keeping the video synchronized using GetSourcePlayPosition.

## Spatial Audio

- _Folder: `5-Spatial`_
- List of examples using the different Wwise Spatial Audio integrated features for demoing and testing purposes. Use these features to spatialize sound in your virtual environments.

### Geometry

- _Folder: `5-Spatial/SpatialGeometry`_
- List of gyms showing how sound is affected by Geometry.

#### Geometry Shapes

- _Folder: `5-Spatial/SpatialGeometry/SpatialGeometryShape`_
- List of gyms showing how Geometry works with different shapes.

##### All Brush presets | _Unreal_

- _Folder: `5-Spatial/SpatialGeometry/SpatialGeometryShape/SpatialGeometryShapeBrush`_
- Shows how Geometry works with all Brush presets.

##### All Collision presets | _Unreal_

- _Folder: `5-Spatial/SpatialGeometry/SpatialGeometryShape/SpatialGeometryShapeCollision`_
- Shows how Geometry works with all Collision presets.

##### All Mesh presets

- _Folder: `5-Spatial/SpatialGeometry/SpatialGeometryShape/SpatialGeometryShapeMesh`_
- Shows how Geometry works with all Mesh primitives and with custom shapes.

### Level loading

- _Folder: `5-Spatial/SpatialLevelLoading`_
- A list of gyms to test the loading and unloading behaviour of Spatial Audio levels.

#### Loading a Spatial Audio sub-level in a Spatial Audio level

- _Folder: `5-Spatial/SpatialLevelLoading/SpatialLevelLoadingSubLevel`_
- Shows what happens when a Spatial Audio sub-level is loaded in a Spatial Audio level.

#### Loading from a Spatial Audio level to a non-Spatial Audio level

- _Folder: `5-Spatial/SpatialLevelLoading/SpatialLevelLoadingToNonSpatial`_
- Shows what happens when the game transitions from a level containing Spatial Audio elements to a level with no Spatial Audio elements.

#### Loading from a non-Spatial Audio level to a Spatial Audio level

- _Folder: `5-Spatial/SpatialLevelLoading/SpatialLevelLoadingToSpatial`_
- Shows what happens when the game transitions from a level with no Spatial Audio elements to a level containing Spatial Audio elements.

### Obstruction and Occlusion | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc`_
- Examples showing how to use Obstruction and Occlusion in a map using Spatial Audio.

#### Emitter Obstruction | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc/SpatialObsOccEmitterObstruction`_
- Example showing Obstruction being applied when an obstacle is placed between an emitter and the listener. For Obstruction to be applied, the map needs to contain Spatial Audio Rooms.

##### Emitter Occlusion | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc/SpatialObsOccEmitterOcclusion`_
- Example showing Occlusion being applied when an obstacle is placed between an emitter and the listener. For Occlusion to be applied, the map cannot contain any Spatial Audio Rooms.

#### Emitter Obstruction through Portals | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc/SpatialObsOccPortalObstruction/`_
- Examples showing Obstruction being applied when an obstacle is placed between an emitter and the listener while they are in different Spatial Audio Rooms.

##### Obstacle between Emitter and Portal | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc/SpatialObsOccPortalObstruction/SpatialObsOccPortalObstructionEmitter`_
- Example showing Obstruction being applied when an obstacle is placed between an emitter and a Portal.

##### Obstacle between Listener and Portal | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc/SpatialObsOccPortalObstruction/SpatialObsOccPortalObstructionListener`_
- Example showing Obstruction being applied when an obstacle is placed between a Portal and the listener.

##### Obstacle between two Portals | _Unreal_

- _Folder: `5-Spatial/SpatialObsOcc/SpatialObsOccPortalObstruction/SpatialObsOccPortalObstructionPortal`_
- Example showing Obstruction being applied when an obstacle is placed between two Portals.

### Outdoors Room

- _Folder: `5-Spatial/SpatialOutdoorsRoom`_
- Shows how to set and update the parameters of the automatically created Outdoors Room.

### Radial Emitter

- _Folder: `5-Spatial/SpatialRadialEmitter`_
- A list of gyms to test Radial Emitters.

#### Radial Emitter and Multi-Position

- _Folder: `5-Spatial/SpatialRadialEmitter/SpatialRadialEmitterMultiPosition`_
- Shows how to set up a Multi-Positioned Radial Emitter.

#### Single Radial Emitter

- _Folder: `5-Spatial/SpatialRadialEmitter/SpatialRadialEmitterSimple`_
- Shows how to set up a single Radial emitter.

## Niagara | _Unreal_
- _Folder: `7-Niagara`_
- Using the Niagara Particle system with the Wwise Niagara plugin.


### Niagara GPU Emitter
- _Folder: `7-Niagara/NiagaraGPUEmitter`_
- Example showing how to export particle data to Blueprints . This example runs **without** using the Wwise Niagara plugin.

### Niagara Fire and Forget
- _Folder: `7-Niagara/NiagaraFireAndForget`_
- Example of an posting a fire-and-forget (one-shot) Wwise Event from a Niagara emitter.

### Niagara Persistent
- _Folder: `7-Niagara/NiagaraPersistent`_
- Example of an posting a persistent Wwise Event from a Niagara emitter, and modifying Game Parameters
  associated with it.

## Testing

- _Folder: `9-Testing`_
- List of tests designed to evaluate the performance and robustness of the sound engine.

### Stress

- _Folder: `9-Testing/Stress`_
- List of tests designed to push the sound engine to its limits. Some of these tests may result in source or voice starvation, which is expected.
