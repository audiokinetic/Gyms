/*******************************************************************************
The content of this file includes portions of the AUDIOKINETIC Wwise Technology
released in source code form as part of the SDK installer package.

Commercial License Usage

Licensees holding valid commercial licenses to the AUDIOKINETIC Wwise Technology
may use this file in accordance with the end user license agreement provided 
with the software or, alternatively, in accordance with the terms contained in a
written agreement between you and Audiokinetic Inc.

Apache License Usage

Alternatively, this file may be used under the Apache License, Version 2.0 (the 
"Apache License"); you may not use this file except in compliance with the 
Apache License. You may obtain a copy of the Apache License at 
http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software distributed
under the Apache License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES
OR CONDITIONS OF ANY KIND, either express or implied. See the Apache License for
the specific language governing permissions and limitations under the License.
*******************************************************************************/

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemPersistent : MonoBehaviour
{
	[Tooltip("The Wwise Event to post when a particle is spawned.")]
	public AK.Wwise.Event wwiseEvent;

	[Tooltip("The Wwise RTPC to drive with the particle's normalized age (0.0 to 1.0).")]
	public AK.Wwise.RTPC gameParameter;

	[Tooltip("The maximum number of particles to handle. Ensure this matches or exceeds your system's max particles.")]
	public int MaxParticles = 1000;

	[Tooltip("If checked, the Wwise Event will be stopped when the GameObject attached to the particle is destroyed.")]
	public bool StopEventOnComponentDestroyed = true;

	[Tooltip("If checked, the particle sound system will only run its logic when the game is playing (i.e., not during Scene View particle simulation).")]
	public bool OnlyActiveDuringGameplay = false;

	private ParticleSystem pSystem;
	private ParticleSystem.Particle[] particles;

	// A struct to hold the data needed to track a single particle's sound
	private struct WwiseParticleData
	{
		public GameObject particleGameObj;
		public uint playingId;
	}

	// Dictionary to link the unique particle seed (ID) to its Wwise data
	private Dictionary<uint, WwiseParticleData> activeParticleSounds;

	void Awake()
	{
		pSystem = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[MaxParticles];
		activeParticleSounds = new Dictionary<uint, WwiseParticleData>(MaxParticles);

		if (wwiseEvent == null)
		{
			Debug.LogError("Wwise Event is not assigned in the WwiseParticleSoundController on " + gameObject.name);
		}
	}

	void Update()
	{
		if(!Application.isPlaying && OnlyActiveDuringGameplay)
		{
			return;
		}

		if (wwiseEvent == null || gameParameter == null) return;

		int numParticles = pSystem.GetParticles(particles);

		// Identify and process current particles (Active/New)
		HashSet<uint> currentActiveSeeds = new HashSet<uint>();

		for (int i = 0; i < numParticles; i++)
		{
			ParticleSystem.Particle p = particles[i];
			uint seed = p.randomSeed; // Use randomSeed as the unique particle ID

			float normalizedAge = 1.0f - (p.remainingLifetime / p.startLifetime);
			float rtpcValue = normalizedAge; // Scale to 0-100 range (common for Wwise)

			// Add to the current set for dead particle detection later
			currentActiveSeeds.Add(seed);

			if (activeParticleSounds.ContainsKey(seed))
			{
				// Particle is ACTIVE: Update position and rotation and RTPC
				WwiseParticleData data = activeParticleSounds[seed];

				data.particleGameObj.transform.position = p.position;
				data.particleGameObj.transform.rotation = Quaternion.Euler(p.rotation3D);

				gameParameter.SetValue(data.particleGameObj, rtpcValue);
			}
			else
			{
				GameObject particleObj = new GameObject("WwiseParticleObj_" + seed);
				particleObj.transform.SetParent(this.transform); // Keep hierarchy clean
				particleObj.transform.position = p.position;
				particleObj.transform.rotation = Quaternion.Euler(p.rotation3D);

				AkGameObj akObj = particleObj.AddComponent<AkGameObj>();

				uint pID = wwiseEvent.Post(particleObj);

				// Store the tracking data
				WwiseParticleData newData = new WwiseParticleData
				{
					particleGameObj = particleObj,
					playingId = pID
				};
				activeParticleSounds.Add(seed, newData);
			}
		}

		// Identify and process Dead Particle
		List<uint> seedsToRemove = new List<uint>();
		foreach (var pair in activeParticleSounds)
		{
			if (!currentActiveSeeds.Contains(pair.Key))
			{
				// Check the boolean to determine the cleanup method
				if (StopEventOnComponentDestroyed)
				{
					// Scenario 1: IMMEDIATE STOP & CLEANUP (Used in both Editor and Runtime)
					AkUnitySoundEngine.StopPlayingID(pair.Value.playingId);

					if (pair.Value.particleGameObj != null)
					{
						// *** Use DestroyImmediate in Editor, Destroy in Runtime ***
						if (Application.isPlaying)
						{
							Destroy(pair.Value.particleGameObj);
						}
						else
						{
							DestroyImmediate(pair.Value.particleGameObj);
						}
					}
				}
				else
				{
					// Scenario 2: DELAYED CLEANUP (If boolean is UNCHECKED/false)
					if (pair.Value.particleGameObj != null)
					{
						// *** Delayed Destroy is ONLY allowed in Runtime ***
						if (Application.isPlaying)
						{
							Destroy(pair.Value.particleGameObj, 5.0f);
						}
						else
						{
							// TO DO: Fix the behavior described here
							// CRITICAL: Delayed destruction is not possible in Edit Mode.
							// If you want the sound to finish in the Editor, you MUST NOT
							// destroy the object here. However, this means the object
							// will leak in the Editor until you manually stop the simulation
							// or delete the main component (which triggers OnDestroy).
							// For now, we leave it to leak if unchecked in Editor simulation, 
							// prioritizing the "sound plays to completion" feature in Play Mode.
						}
					}
				}

				seedsToRemove.Add(pair.Key);
			}
		}

		// Remove the dead particles from the tracking dictionary
		foreach (uint seed in seedsToRemove)
		{
			activeParticleSounds.Remove(seed);
		}
	}

	void OnDestroy()
	{
		if (activeParticleSounds == null) return;

		if (StopEventOnComponentDestroyed)
		{
			bool isSoundEngineBufferValid = AkUnitySoundEngine.IsInitialized();

			// Clean up any remaining sounds and GameObjects if the controller is destroyed
			foreach (var pair in activeParticleSounds)
			{
				AkUnitySoundEngine.StopPlayingID(pair.Value.playingId);

				if (pair.Value.particleGameObj != null)
				{
					// *** Apply conditional destruction here too ***
					if (Application.isPlaying)
					{
						Destroy(pair.Value.particleGameObj);
					}
					else
					{
						DestroyImmediate(pair.Value.particleGameObj);
					}
				}
			}
			activeParticleSounds.Clear();
		}
	}
}

