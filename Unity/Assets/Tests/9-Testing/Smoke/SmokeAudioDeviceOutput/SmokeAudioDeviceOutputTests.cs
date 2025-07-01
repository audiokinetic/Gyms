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
using UnityEngine.TestTools;

namespace Tests
{
    public class SmokeAudioDeviceOutputTests : GymTests
    {
        private const string AudioDeviceShareSet = "Audio_Device_System";
        private const string SceneName = "SmokeAudioDeviceOutput";
        
        [UnityTest]
        public IEnumerator SmokeAudioDeviceOutput_Tests()
        {
            yield return StartTest(SceneName);
            var bank = gameObject.GetComponent<AkBank>();

            LoadBank(bank);
            var initialPlayingID = AkSoundEngine.PostEvent("Silence", gameObject);
            
            // Find an additional audio device
            var additionalDevice = AddOutputHelpers.GetNonDefaultActiveDevice("System");
            if (additionalDevice == null)
            {
                yield break;
            }
            
            Assert.AreNotEqual(additionalDevice.idDevice, AkSoundEngine.AK_INVALID_DEVICE_ID);

            // AddOutput and make sure it works
            ulong deviceId;
            AKRESULT result;
            {
                var outSettingsToAdd = new AkOutputSettings();
                outSettingsToAdd.idDevice = additionalDevice.idDevice;
                outSettingsToAdd.audioDeviceShareset = AkSoundEngine.GetIDFromString(AudioDeviceShareSet);
                result = AkSoundEngine.AddOutput(outSettingsToAdd, out deviceId);

                Assert.AreEqual(result, AKRESULT.AK_Success);
                Assert.AreNotEqual(deviceId, AkSoundEngine.AK_INVALID_DEVICE_ID);
            }
            
            // Make sure posting additional event works
            {
                var pID = gameObject.GetComponent<AkEvent>().data.Post(gameObject);
                Assert.AreEqual(pID, initialPlayingID+1);
            }
            
            // Remove the output
            {
                result = AkSoundEngine.RemoveOutput(deviceId);
                Assert.AreEqual(result, AKRESULT.AK_Success);
            }
            
            yield return FinishTest(SceneName);
            bank.data.Unload();
        }
    }
}
