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

using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class ParticleSystemFireAndForgetCollision : MonoBehaviour
{
	public ParticleSystem sourceParticleSystem;
	public AK.Wwise.Event collisionWwiseEvent;

	[Tooltip("Max sounds to trigger per frame to prevent audio clutter.")]
	[SerializeField] private int maxSoundsPerFrame = 5;

	private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	// Unity's native Object Pool
	private ObjectPool<GameObject> pool;

	private void Awake()
	{
		// Initialize the pool
		pool = new ObjectPool<GameObject>(
			createFunc: () => {
				GameObject go = new GameObject("Pool_ParticleAudioSource");
				go.AddComponent<AkGameObj>();
				return go;
			},
			actionOnGet: (go) => go.SetActive(true),
			actionOnRelease: (go) => go.SetActive(false),
			actionOnDestroy: (go) => Destroy(go),
			collectionCheck: false,
			defaultCapacity: 10,
			maxSize: 20
		);
	}

	private void OnParticleCollision(GameObject other)
	{
		if (other == sourceParticleSystem.gameObject)
		{
			int numCollisionEvents = sourceParticleSystem.GetCollisionEvents(gameObject, collisionEvents);
			int instancesToPlay = Mathf.Min(numCollisionEvents, maxSoundsPerFrame);

			for (int i = 0; i < instancesToPlay; i++)
			{
				// Grab a unique object from the pool
				GameObject tempSource = pool.Get();

				// Set the position accurately
				tempSource.transform.position = collisionEvents[i].intersection;

				// Post the event on the unique object
				collisionWwiseEvent.Post(tempSource);

				// Return to pool after a short delay so the sound can initialize
				StartCoroutine(ReleaseAfterDelay(tempSource, 0.1f));
			}
		}
	}

	private System.Collections.IEnumerator ReleaseAfterDelay(GameObject obj, float delay)
	{
		yield return new WaitForSeconds(delay);
		pool.Release(obj);
	}
}