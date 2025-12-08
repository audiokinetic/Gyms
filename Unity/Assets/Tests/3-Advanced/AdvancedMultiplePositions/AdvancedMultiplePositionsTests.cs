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

using System.Collections;
using AK.Wwise;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AdvancedMultiplePositionsTests : GymTests
    {
        const string SceneName = "AdvancedMultiplePositions";
        [UnityTest]
        public IEnumerator AdvancedMultiplePositions_Tests()
        {
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();

            LoadBank(bank);

            GameObject testObject = GameObject.Find("Cylinder");
            var button = testObject.GetComponent<AdvancedMultiplePositions_Interact>();
            // Left "speaker"
            GameObject ambientLeft = button.ambientLeft;
            // Right "speaker"
            GameObject ambientRight = button.ambientRight;
            // GameParameter which corresponds to the bus volume on the AkChannelEmitter bus
            RTPC busVolumeRTPC = button.gameParameter;
            float busVolumeRTPCVal = 0.0f;
            GameObject playerObject = GameObject.Find("FirstPersonCharacter");

            // Set positions and start play
            button.Interact();

            // Go to the left speaker, this one is silent
            playerObject.transform.position = ambientLeft.gameObject.transform.position;

            // Wait a few frames
            yield return new WaitForSeconds(0.2f);

            busVolumeRTPCVal = busVolumeRTPC.GetGlobalValue();
            Assert.AreEqual(busVolumeRTPCVal, -96.0f, 0.0001);

            // Go to the right speaker, this one is emitting a tone
            playerObject.transform.position = ambientRight.gameObject.transform.position;

            // Wait a few frames
            yield return new WaitForSeconds(0.2f);
            float previousVal = busVolumeRTPCVal;

            busVolumeRTPCVal = busVolumeRTPC.GetGlobalValue();
            // As long as it's louder than before (silence), we really don't care what the value is
            Assert.Greater(busVolumeRTPCVal, previousVal);

            // Stop playing the sound
            button.Interact();

            yield return FinishTest(SceneName);
        }
    }
}
