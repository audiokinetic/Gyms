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
    public class StressSwitchesMultipleSoundsTests : GymTests
    {
        const string SceneName = "StressSwitchesMultipleSounds";

        string[] multiSoundMedia = new string[] {"ShortMedia_0", "ShortMedia_1" , "ShortMedia_2" };

        [UnityTest]
        public IEnumerator StressSwitchesMultipleSounds_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            AkState akState = gameObject.GetComponent<AkState>();

            uint expected = PostSilence();


            // Load and Post ManySounds Switch with the Default value(This uses State Value). ShortMedia_6
            bank.data.Load();
            ExpectedLogError("ShortMedia_6 is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_6"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Load and Set AkState to High and Post the event, expect medias to be loaded. Expect medias ShortMedia_0, ShortMedia_1, ShortMedia_2 to play.
            //TODO AkState is currently already loaded.
            akState.data.SetValue();
            ExpectedLogError("media is expected to play", 3);
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilesPlaying));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Unload the AkState-High and Post the event, expect medias to be unloaded. ShouldNotPlay
            //TODO Unload AkState-High, currently no way to unload it.
            ExpectedLogError("was not loaded for this source");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShouldNotPlay"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            yield return new UnityEngine.WaitForSeconds(0.2f);
            yield return FinishTest(SceneName);
        }
        void CheckFilesPlaying(object in_cookie, AkCallbackType in_type, object in_info)
        {
            if (in_type != AkCallbackType.AK_Marker)
                return;
            bool found = false;
            foreach(string media in multiSoundMedia){
                if (((AkMarkerCallbackInfo)in_info).strLabel == (string)in_cookie)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found, string.Format("{0} was playing, but wasn't expected.", ((AkMarkerCallbackInfo)in_info).strLabel));
            UnityEngine.Debug.LogError("media is expected to play (This is an expected error");
        }
    }
}
