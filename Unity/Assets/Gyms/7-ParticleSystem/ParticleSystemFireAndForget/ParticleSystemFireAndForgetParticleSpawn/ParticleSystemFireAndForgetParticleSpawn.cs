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
using UnityEngine.Pool;

[ExecuteInEditMode]
[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemFireAndForgetParticleSpawn : MonoBehaviour
{
	[Header("Wwise Settings")]
	[Tooltip("The Wwise Event to post when a particle is spawned.")]
	public AK.Wwise.Event wwiseEvent;

	[Header("Pool Settings")]
	[Tooltip("Size of the pool. Should be roughly the max number of particles you expect to play sound at once.")]
	public int PoolSize = 20;
	[Tooltip("The maximum size the pool can grow to.")]
	public int MaxPoolSize = 100;

	[Header("Particle Settings")]
	[Tooltip("The maximum number of particles to handle. Ensure this matches or exceeds your system's max particles. Otherwise, audio dropouts may be experienced for particles out of the array's bound.")]
	public int MaxParticles = 1000;
	[Tooltip("If checked, logic only runs during gameplay.")]
	public bool OnlyActiveDuringGameplay = false;

	private ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particles;
	private HashSet<uint> trackedParticleSeeds;
	private ObjectPool<GameObject> pool;
	private int objectCounter = 0;

	private void OnEnable()
	{
		Initialize();
	}

	private void Initialize()
	{
		if (particleSystem == null)
			particleSystem = GetComponent<ParticleSystem>();

		// Initialize/Reset the array
		if (particles == null || particles.Length != MaxParticles)
			particles = new ParticleSystem.Particle[MaxParticles];

		if (trackedParticleSeeds == null)
			trackedParticleSeeds = new HashSet<uint>();

		// Initialize the Pool if it doesn't exist
		if (pool == null)
		{
			pool = new ObjectPool<GameObject>(
				createFunc: CreatePooledItem,
				actionOnGet: OnTakeFromPool,
				actionOnRelease: OnReturnedToPool,
				actionOnDestroy: OnDestroyPoolObject,
				collectionCheck: true,
				defaultCapacity: PoolSize,
				maxSize: MaxPoolSize
			);

			// Pre-warm the pool
			// Note: In Edit Mode, we only pre-warm if the hierarchy is empty to avoid duplicates
			if (transform.childCount < PoolSize)
			{
				List<GameObject> temp = new List<GameObject>();
				for (int i = 0; i < PoolSize; i++) temp.Add(pool.Get());
				foreach (var obj in temp) pool.Release(obj);
			}
		}
	}

	private GameObject CreatePooledItem()
	{
		GameObject obj = new GameObject($"WwisePoolObj_{objectCounter++}");
		obj.transform.SetParent(this.transform);
		obj.AddComponent<AkGameObj>();
		obj.SetActive(false);
		return obj;
	}

	private void OnTakeFromPool(GameObject obj) => obj.SetActive(true);
	private void OnReturnedToPool(GameObject obj) => obj.SetActive(false);
	private void OnDestroyPoolObject(GameObject obj) => DestroyImmediate(obj); // Use DestroyImmediate for Edit Mode safety

	void Update()
	{
		// Check if we should even be running
		if (!Application.isPlaying && OnlyActiveDuringGameplay) return;

		// Safety Check: Ensure everything is initialized (Essential for Edit Mode)
		if (particles == null || pool == null || particleSystem == null)
		{
			Initialize();
		}

		if (wwiseEvent == null || !wwiseEvent.IsValid()) return;

		int numParticles = particleSystem.GetParticles(particles);

		for (int i = 0; i < numParticles; i++)
		{
			uint seed = particles[i].randomSeed;
			if (!trackedParticleSeeds.Contains(seed))
			{
				SpawnFromPool(particles[i].position, particles[i].rotation3D);
				trackedParticleSeeds.Add(seed);
			}
		}

		// Cleanup dead seeds
		trackedParticleSeeds.RemoveWhere(seed =>
		{
			for (int i = 0; i < numParticles; i++)
			{
				if (particles[i].randomSeed == seed) return false;
			}
			return true;
		});
	}

	private void SpawnFromPool(Vector3 position, Vector3 rotation)
	{
		GameObject obj = pool.Get();
		obj.transform.position = position;
		obj.transform.rotation = Quaternion.Euler(rotation);

		// Post the event with the cookie
		wwiseEvent.Post(obj, (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, obj);
	}

	private void EndOfEventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
	{
		if (in_type == AkCallbackType.AK_EndOfEvent)
		{
			GameObject obj = in_cookie as GameObject;
			if (obj != null && pool != null)
			{
				pool.Release(obj);
			}
		}
	}
}