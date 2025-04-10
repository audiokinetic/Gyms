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
    public class BasicLocalizedVoiceTests : GymTests
    {
        const string SceneName = "BasicLocalizedVoice";
        [UnityTest]
        public IEnumerator BasicLocalizedVoice_Tests()
        {
            AkUnitySoundEngine.SetCurrentLanguage("en_US");
            yield return new WaitForEndOfFrame();
            yield return StartTest(SceneName);
            BasicLocalizedVoice localizedVoice = GameObject.Find("Cylinder").GetComponent<BasicLocalizedVoice>();
            AK.Wwise.Event localizedEvent = localizedVoice.LocalizedEvent;
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            ExpectedLogErrorAtLeastOnce("Wwise Addressable Bank Manager: Post_Localized_Voice could not be loaded");
#endif
            yield return localizedVoice.SetLanguage("en_US");
            yield return new WaitForSeconds(0.2f);


#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
	        //With addressables, due to the auto bank not being loaded, the post won't happen for an invalid language thus not incrementing the PlayingId
            //This means that only the SetLanguage to "fr" will increment the PlayingId, making the expected localizedEvent.PlayingId + 1;
            uint expected = localizedEvent.PlayingId + 1;
#else
	        uint expected = localizedEvent.PlayingId + 2;
#endif

            //Set unsupported language
#if !AK_WWISE_ADDRESSABLES
            ExpectedLogError("PrepareEvent for Post_Localized_Voice failed with result: AK_IDNotFound");
#if UNITY_EDITOR
            //There are 5 expected log errors that can appear in a random order
            //Could not post event
            //File not found in path(s)
            //Event ID not found
            //Bank Load Failed
            //Unload bank failed
            ExpectedLogError("Wwise", 5);
#endif
#endif
            yield return localizedVoice.SetLanguage("");
            yield return new WaitForSeconds(0.2f);
            uint actual = localizedEvent.PlayingId;
            Assert.AreEqual(0, actual);
            string language = AkUnitySoundEngine.GetCurrentLanguage();
            Assert.AreEqual("", language);
            LogOutput("Set unsupported language: ", true);
            LogAssert.ignoreFailingMessages = false;
             
            // Set language
            yield return localizedVoice.SetLanguage("fr_FR");

            yield return new WaitForSeconds(0.2f);
            actual = localizedEvent.PlayingId;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual("fr_FR", AkSoundEngine.GetCurrentLanguage());
            LogOutput("Set language: ", true);
            AkSoundEngine.SetCurrentLanguage("en_US");
            yield return FinishTest(SceneName);
        }
    }
}