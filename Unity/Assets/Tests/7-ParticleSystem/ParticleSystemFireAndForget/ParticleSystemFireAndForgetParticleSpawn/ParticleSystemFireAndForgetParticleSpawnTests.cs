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

using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ParticleSystemFireAndForgetParticleSpawnTests : GymTests
    {
        const string SceneName = "ParticleSystemFireAndForgetParticleSpawn";
        [UnityTest]
        public IEnumerator ParticleSystemFireAndForget_Tests()
        {
            yield return StartTest(SceneName);

			GameObject particleSystemObject = GameObject.Find("Particle System");
			ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
			ParticleSystemFireAndForgetParticleSpawn scriptReference = particleSystem.GetComponent<ParticleSystemFireAndForgetParticleSpawn>();

#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
#if UNITY_WEBGL
            yield return scriptReference.wwiseEvent.WwiseObjectReference.CompleteLoadBank();
#else
			yield return new WaitUntil(() => scriptReference.wwiseEvent.WwiseObjectReference.CompleteLoadBank().IsCompleted);
#endif
#endif

			uint playingID1 = AkUnitySoundEngine.PostEvent("Fire_Shotgun", particleSystemObject);

			particleSystem.Play();

			yield return new WaitForSeconds(2.0f);

			particleSystem.Stop();
			yield return null;

			uint playingID2 = AkUnitySoundEngine.PostEvent("Fire_Shotgun", particleSystemObject);

			Assert.Greater(playingID2, playingID1);
			LogOutput("PlayingID before ParticleSystem is " + playingID1 + ", playingID after is " + playingID2 + " - ", true);

            yield return FinishTest(SceneName);
        }
    }
}
